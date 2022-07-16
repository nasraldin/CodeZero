namespace CodeZero.Logging;

/// <summary>
/// CodeZero common errors
/// </summary>
public static partial class ErrorCode
{
    public static readonly ErrorId NotModified = new(304, "Not Modified");
    public static readonly ErrorId Redirect = new(307, "Redirect");
    public static readonly ErrorId BadRequest = new(400, "Bad Request");
    public static readonly ErrorId Unauthorized = new(401, "Unauthorized");
    public static readonly ErrorId PaymentRequired = new(402, "Payment Required");
    public static readonly ErrorId Forbidden = new(403, "Forbidden");
    public static readonly ErrorId NotFound = new(404, "Not Found");
    public static readonly ErrorId MethodNotAllowed = new(405, "Method Not Allowed");
    public static readonly ErrorId RequestTimeout = new(408, "Request Timeout");
    public static readonly ErrorId Conflict = new(409, "Conflict");
    public static readonly ErrorId PayloadTooLarge = new(413, "Payload Too Large");
    public static readonly ErrorId URITooLong = new(414, "URI Too Long");
    public static readonly ErrorId ExpectationFailed = new(417, "Expectation Failed");
    public static readonly ErrorId RequestExpired = new(419, "Request Expired");
    public static readonly ErrorId Locked = new(423, "Locked");
    public static readonly ErrorId TooManyRequests = new(429, "Too Many Requests");
    public static readonly ErrorId NoResponse = new(444, "No Response");
    public static readonly ErrorId Unavailable = new(451, "Unavailable");
    public static readonly ErrorId ClientClosedRequest = new(460, "Client Closed Request");
    public static readonly ErrorId InvalidToken = new(498, "Invalid Token");
    public static readonly ErrorId TokenRequired = new(499, "Token Required");
    public static readonly ErrorId InternalServerError = new(500, "Internal Server Error");
    public static readonly ErrorId NotImplemented = new(501, "Not Implemented");
    public static readonly ErrorId BadGateway = new(502, "Bad Gateway");
    public static readonly ErrorId ServiceUnavailable = new(503, "Service Unavailable");
    public static readonly ErrorId GatewayTimeout = new(504, "Gateway Timeout");
    public static readonly ErrorId NotSupported = new(505, "Not Supported");
    public static readonly ErrorId LoopDetected = new(508, "Loop Detected");
    public static readonly ErrorId UnknownError = new(520, "Unknown Error");
    public static readonly ErrorId ConnectionTimedOut = new(522, "Connection Timed Out");
}