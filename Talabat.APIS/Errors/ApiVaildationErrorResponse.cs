namespace Talabat.APIS.Errors
{
	public class ApiVaildationErrorResponse : ApiResponse
	{

        public IEnumerable<string> Errors { get; set; }
        public ApiVaildationErrorResponse() : base(404)
		{
			Errors = new List<string>();
		}
	}
}
