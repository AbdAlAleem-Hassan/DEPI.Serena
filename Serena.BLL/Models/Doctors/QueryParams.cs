namespace Serena.BLL.Models.Doctors
{
    public class QueryParams
    {
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public int? YearsOfExperience { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }

        public string? City { get; set; }
    }
}