public class HttpException : Exception {
    public int StatusCode;

    public HttpException(string message, int statusCode) : base(message) {
        this.StatusCode = statusCode;
    }
}