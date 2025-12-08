namespace Serena.BLL.Models.Hospitals
{
    public class QueryParamsForHospital
    {
        public string? Name { get; set; }
        public string? Department { get; set; }
        public decimal? MaxAverageCost { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        public string? Rank { get; set; }
        public string? SortBy { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

    }
}