namespace ITRProject.API.PL.Errors
{
    public class ApiErrorResponse
    {
        // Response دي الطريقه اللي بنهندل بيها عشان نوحد بيها شكل ال
        public int statusCode { get; set; }
        public string? message { get; set; }
        public ApiErrorResponse(int StatusCode, string? Message = null, List<string>? Errors = null)
        {
            statusCode = StatusCode;
            message = Message ?? GetDefaultMessageForStatusCode(statusCode); //GetDefaultMessageForStatusCode هنا بقوله لو مفيش رساله جاتلك اعمل جنيريت للميثود دي

        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch //New Feature => C# 7
            {
                400 => "a bad Request , You have made",
                401 => "Authorized , your not found",
                404 => "Resource was not found",
                500 => "Server Error",
                _ => null // default
            };
            return message;
        }
    }
}
