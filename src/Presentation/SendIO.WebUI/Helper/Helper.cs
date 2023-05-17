namespace SendIO.WebUI.Helper
{
    public class Helper
    {
        public static string ToSizeString(long l)
        {
            long KB = 1024;
            long MB = KB * 1024;
            long GB = MB * 1024;
            long TB = GB * 1024;
            double size = l;
            if (l >= TB)
            {
                size = Math.Round((double)l / TB, 2);
                return $"{size} TB";
            }
            else if (l >= GB)
            {
                size = Math.Round((double)l / GB, 2);
                return $"{size} GB";
            }
            else if (l >= MB)
            {
                size = Math.Round((double)l / MB, 2);
                return $"{size} MB";
            }
            else if (l >= KB)
            {
                size = Math.Round((double)l / KB, 2);
                return $"{size} KB";
            }
            else
            {
                return $"{size} Bytes";
            }
        }
    }
}
