
# SendIO

SendIO geçici süreli dosya depolama ve paylaşma uygulamasıdır.

## Özellikler
- Çoklu dosya yüklemesini destekler.
- Yüklemiş olduğunuz dosyaların bağlantı adresini göndererek paylaşabilirsiniz.
- Klasik dosya saklama yöntemleri yerine “Minio File Object Storage” sunucusunu kullanarak dosyalarınızı saklar.
- Yüklemiş olduğunuz dosyalar otomatik olarak 7 gün sonra silinir.

## Gereksinimler

- .Net 7.0
- Minio object storage (Dosyaların tutulacağı alan)
- Mssql veritabanı

  
## Ekran Görüntüleri

![Uygulama Ekran Görüntüsü](https://github.com/canglr/SendIO/blob/9516b879103f4fca67e972ec244fcffdb180a667/other/image/01.png)

Demo: [https://sendio.gulernet.net/](https://sendio.gulernet.net/)
  
## Bilgisayarınızda Çalıştırın

Projeyi klonlayın

```bash
  git clone https://github.com/canglr/SendIO.git
```

Proje dizinine gidin

```bash
  cd SendIO
```
Visual studio kullanarak projeyi düzenleyebilir ve çalıştırabilirsiniz. Çalıştırmadan önce bağlantı ayarlarının düzenlenmesi gerekmektedir.


  
## Config Dosyaları

```bash
  src/Presentation/SendIO.WebApi/appsettings.json - appsettings.Development.json 
```
Yukarıdaki dosyaların içerisinde veritabanı ve minio sunucu bilgileri tutulmaktadır.
Uygulamanın çalışabilmesi için geçerli bilgiler ile düzenlenmelidir.

```bash
  src/Presentation/SendIO.WebUI/wwwroot/appsettings.json - appsettings.Development.json 
```
Yukarıdaki dosyada arayüz ile ilgili ayarlar yer almaktadır. Logodaki yazının değiştirilmesi ve demo modu aktif etme gibi işlemler bu dosyadan yapılmaktadır. En önemlisi ise WebApi bağlantı bilgisi burada tutulmaktadır. Kullanılmadan önce kendi bilgileriniz ile değiştirilmelidir.
## Veritabanı Oluşturma

```bash
  src/Infrastucture/SendIO.Persistence
```
Veritabanı oluşturmak için ilk önce yukarıdaki dizine terminal ile gitmemiz gerekiyor.

```bash
  dotnet ef database update
```
Eğer config dosyasındaki bilgiler doğru girildiyse yukarıdaki komut ile veritabanı otomatik olarak oluşturulacak.
## Dağıtım

Dağıtım olarak docker tercih ediyorum. Sırasıyla aşağıdaki dağıtımları yapacağız.

- Mssql
- Minio
- WebApi
- WebUI

İşlemleri yapabilmeniz için bilgisayarınızda veya sunucunuzda docker kurulu olmalıdır.

### Mssql kurulumu için:

```bash
  sudo docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```
<YourStrong@Passw0rd> alanını kendi şifreniz ile değiştiriniz. Kullanıcı adı otomatik olarak sa olacak.
Yukarıdaki kodu terminalde çalıştırınız.

### Minio kurulumu için:

```bash
version: "3.7"
services:
  minio:
    container_name: minio
    image: minio/minio
    ports:
      - "9000:9000"
      - "9001:9001"
    command: server /data --console-address ":9001"
    volumes:
      - ./data:/data
    environment:
      MINIO_BROWSER_REDIRECT_URL: http://console.file.gulernet.net:9001
      MINIO_SERVER_URL: https://file.gulernet.net
      MINIO_ROOT_USER: admin
      MINIO_ROOT_PASSWORD: Test12345
```
Yukarıdaki kodu kendinize göre düzenleyiniz. Daha sonra adını docker-compose.yml olarak kaydediniz.

```bash
docker compose up
```

Dosyanın bulunduğu dizine terminal ile gidip yukarıdaki komutu çalıştırınız.

Şuana kadar yapmış olduğumuz kurulumlar projeden bağımsızdı şimdi proje ile ilgili kısıma gelmiş bulunmaktayız.

### WebApi kurulumu için:

Visual studio ile projeyi açıyoruz ve WebApi projesini klasöre publish ediyoruz.

```bash
FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY ./sendio/api .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "SendIO.WebApi.dll"]
```

COPY ./sendio/api . 

satırını publish ettiğiniz klasör yolu ile değiştiriniz.

Yukarıdaki kodu Dockerfile olarak kaydediniz.

```bash
docker build -t webapiimaj .
```
Yukarıdaki kod ile projenin docker imajını oluşturuyoruz.

```bash
docker run -p 8080:5000 --name webapiimaj webapi
```

Yukarıdaki kod ile oluşturmuz olduğumuz imajı docker üzerinde başlatıyoruz.
8080 dış portu kendinize göre düzenleyebilirsiniz.

### WebUI kurulumu için:

Visual studio ile projeyi açıyoruz ve WebUI projesini klasöre publish ediyoruz.

```bash
FROM nginx:alpine
WORKDIR /user/share/nginx/html
COPY ./sendio/ui/wwwroot .
COPY ./sendio/conf/nginx.conf /etc/nginx/nginx.conf
```

COPY ./sendio/ui/wwwroot . 

satırını publish ettiğiniz klasör yolu ile değiştiriniz.

Ek olarak nginx conf dosya yolu içinde geçerli

COPY ./sendio/conf/nginx.conf /etc/nginx/nginx.conf

Bu kısmı kendinize göre değiştiriniz "./sendio/conf/nginx.conf"

Yukarıdaki kodu Dockerfile olarak kaydediniz.


nginx.conf
```bash
events { }
http {
   include mime.types;
   types {
      application/wasm;
    }
  server {
     listen 5000;
     index index.html;
     location / {
        root /user/share/nginx/html;
        try_files $uri $uri/ /index.html =404;
     }
  }
}
```

```bash
docker build -t webuiimaj .
```
Yukarıdaki kod ile projenin docker imajını oluşturuyoruz.

```bash
docker run -p 80:5000 --name webuiimaj webui
```

Dağıtım İşlemleri tamamlandı.

Arayüz adresi : http://localhost

Api adresi : http://localhost:8080

Adresleri örnek olarak yazdım. Kendi yapmış olduğunuz ayarlara göre değişiklik gösterir.

