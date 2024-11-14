
namespace EmployeeManagement.Models.Common
{
    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
        public List<Error> Errors { get; set; }
    }
}
