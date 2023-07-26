namespace Talabat.Core.Specifications
{
	public class ProductSpecPrama
	{
		private const int MaxPages = 10;
		private int Pagesize=5;

		public int PageSize
		{
			get { return Pagesize; }
			set
			{ 
				Pagesize = value> MaxPages ? MaxPages : value;
			
			}
		}

		private string search;

		public string Search
        {
			get { return search; }
			set { search = value.ToLower(); }
		}

		public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }
    }
}
