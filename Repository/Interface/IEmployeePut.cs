using ExpressionFilterApi.DTOs.Helper;
using ExpressionFilterApi.DTOs;

namespace EmployeeApi.Repository.Interface;
public interface IEmployeePut
{
    public Task<IEnumerable<ResponseDto>> AdvanceFilter(FilterRequest filterRequest);
}