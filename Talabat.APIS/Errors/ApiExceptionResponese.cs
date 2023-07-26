namespace Talabat.APIS.Errors
{
	public class ApiExceptionResponese:ApiResponse
	{
        public string? Details { get; set; }

        public ApiExceptionResponese(int statusCode,string?message=null,string?details = null):base(statusCode, message)
        {
            Details = details;
        }
    }
}
