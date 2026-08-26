using ExpressionFilterApi.DTOs;
using ExpressionFilterApi.Helper;
namespace ExpressionFilterApi.Repository.Interface;
public interface IEmployeeGet
{
    Task<IEnumerable<ResponseDto>> GetEmployee(PagingDto pagingDto);
}