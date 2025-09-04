namespace ITRProject.API.PL.Errors
{
    public class ApiValidationResponse:ApiErrorResponse
    {
        public List<string>? errors { get; set; } = new List<string>();
        public ApiValidationResponse(int StatusCode, string? Message = null, List<string>? Errors = null) : base(StatusCode, Message)
        {
            foreach (var error in Errors)
            {
                errors.Add(error);
            }

        }
    }
}
