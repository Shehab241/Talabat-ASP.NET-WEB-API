namespace Talabat.APIS.Errors
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statuscode,string?message=null)
        {
            StatusCode = statuscode;
			Message = message ?? GetDefaultMessageForStatusCode(statuscode);

		}

		private string? GetDefaultMessageForStatusCode(int statuscode)
		{
			return statuscode switch
			{
				400 => "Bad Request",
				401 => "UnAuthrized",
				404 => "Resoure was not found",
				500 => "Errors are tha path to the dark .Errors lead to anger Anger leads to hate , hate to career change",
				_ => null
			};
		}
	}
}
