namespace CodeZero;

public static partial class AppConst
{
    /// <summary>
    /// Collection of MimeType Constants for using to avoid Typos
    /// If needed MimeTypes missing feel free to add
    /// </summary>
    public static partial class MimeTypes
    {
        public const string JSON = "application/json";
        public const string PDF = "application/pdf";
        public const string ZIP = "application/zip";
        public const string ForceDownload = "application/force-download";
        public const string Problem = "application/problem+json";
        public const string Image_JPEG = "image/jpeg";
        public const string Image_PNG = "image/png";
        public const string Image_SVG = "image/svg+xml";
        public const string Audio = "audio/mpeg";
        public const string Video = "video/mp4";
        public const string Multipart_Mixed = "multipart/mixed";
        public const string Multipart_FormData = "multipart/form-data";
        public const string Text = "text/plain";
        public const string Text_UTF8 = "text/plain; charset=utf-8";
        public const string Text_CSS = "text/css";
        public const string Text_CSV = "text/csv";
        public const string Text_HTML = "text/html";
        /// <summary>
        /// Generic binary data, for unknown binary file.
        /// </summary>
        public const string OctetStream = "application/octet-stream";
    }
}