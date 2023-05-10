using System;
using System.Net;
using System.Security.AccessControl;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.Exceptions;
using SendIO.Application.Services;

namespace SendIO.Infrastucture
{
    public class MinIOService : IMinIO
    {

        private string endPoint;
        string accessKey;
        string secretKey;
        string Bucketname;
        MinioClient _client;
        public MinIOService(IConfiguration configuration)
        {
            this.endPoint = configuration["MinIO:Endpoint"].ToString();
            this.accessKey = configuration["MinIO:Accesskey"].ToString();
            this.secretKey = configuration["MinIO:Secretkey"].ToString();
            this.Bucketname = configuration["MinIO:Bucketname"].ToString();
            this._client = new MinioClient()
                .WithEndpoint(this.endPoint)
                .WithCredentials(this.accessKey, this.secretKey)
                .WithSSL(false)
                .Build();

        }

        public async Task<string> Add(string folder,Stream file, string filename, string contenttype)
        {
            FileInfo fileinfo = new FileInfo(filename);
            string objectName = Guid.NewGuid().ToString() + fileinfo.Extension;
            try
            {
                var args = new PutObjectArgs()
                .WithBucket(this.Bucketname)
                    .WithObject(folder.Insert(folder.Length,"/")+objectName)
                    .WithObjectSize(file.Length)
                    .WithContentType(contenttype)
                    .WithStreamData(file);
                    
                await _client.PutObjectAsync(args).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Bucket]  Exception: {e}");
            }
            return objectName;
        }

        public async Task<string> ShareLink(string filename, int minute)
        {
            string url = "";
            try
            {
                var args = new PresignedGetObjectArgs()
                    .WithBucket(this.Bucketname)
                    .WithObject(filename)
                    .WithExpiry(minute * minute * 24);
                url = await _client.PresignedGetObjectAsync(args);
                
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
            }
            return url;
        }
    }
}

