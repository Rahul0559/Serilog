using ExpressionFilterApi.DTOs.Helper;
using ExpressionFilterApi.DTOs;
using EmployeeApi.Repository.Interface;
using ExpressionFilterApi.Data;
using Microsoft.EntityFrameworkCore;
namespace ExpressionFilterApi.Repository;
public class EmployeePut : IEmployeePut
{
    private readonly AppDbContext _dbcontext;

    public EmployeePut(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<IEnumerable<ResponseDto>> AdvanceFilter(
        FilterRequest filterRequest)
    {
        IEnumerable<ResponseDto> result = await _dbcontext.Employees
        .Select(e => new ResponseDto
        {
            EmployeeId = e.EmployeeId,
            EmployeeName = e.EmployeeName,
            Address = e.Address,
            Contact = e.Contact,
            Department = e.Department,
            IsActive = e.IsActive,
            IsDeleted = e.IsDeleted
        }).ToListAsync();
        
        return result;
    }
}