using ExpressionFilterApi.DTOs;
using ExpressionFilterApi.DTOs.Helper;
namespace EmployeeApi.Repository.Interface;
public interface IEmployeeAdavnceFilter
{
    public Task<IEnumerable<ResponseDto>> EmployeeGetByAdvanceFilter(List<FilterRequest> filters);
}