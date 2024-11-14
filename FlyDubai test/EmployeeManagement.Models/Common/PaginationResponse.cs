

namespace EmployeeManagement.Models.Common
{
    public class PaginationResponse<T>
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public List<T> Items { get; set; }
        public Error Error { get; set; }
    }
}
