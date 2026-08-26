using ExpressionFilterApi.Repository.Interface;
using ExpressionFilterApi.Data;
using ExpressionFilterApi.DTOs;
using Microsoft.EntityFrameworkCore;
using ExpressionFilterApi.Helper;
public class EmployeeGet : IEmployeeGet
{
    private readonly AppDbContext _dbcontext;
    public EmployeeGet(AppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    public async Task<IEnumerable<ResponseDto>> GetEmployee(PagingDto pagingDto)
    {
       IQueryable<ResponseDto> result = _dbcontext.Employees
            .Where(e => e.IsActive)
            .Select(e => new ResponseDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Address = e.Address,
                Contact = e.Contact,
                Department = e.Department,
                IsDeleted = e.IsDeleted,
                IsActive = e.IsActive
            });
        if (!string.IsNullOrWhiteSpace(pagingDto.searchItem))
        {
            result = result.Where(e =>
                e.EmployeeName.Contains(pagingDto.searchItem) ||
                e.Address!.Contains(pagingDto.searchItem) ||
                e.Contact!.Contains(pagingDto.searchItem) ||
                e.Department!.Contains(pagingDto.searchItem)
            );
        }
        if (!string.IsNullOrWhiteSpace(pagingDto.sortColumn))
        {
            string column = pagingDto.sortColumn.ToLower();
            bool descending = pagingDto.sortDirection?.ToLower() == "desc";
            switch (column)
            {
                case "employeename":
                    result = descending
                        ? result.OrderByDescending(e => e.EmployeeName)
                        : result.OrderBy(e => e.EmployeeName);
                    break;
                case "address":
                    result = descending
                        ? result.OrderByDescending(e => e.Address)
                        : result.OrderBy(e => e.Address);
                    break;
                case "contact":
                    result = descending
                        ? result.OrderByDescending(e => e.Contact)
                        : result.OrderBy(e => e.Contact);
                    break;
                case "department":
                    result = descending
                        ? result.OrderByDescending(e => e.Department)
                        : result.OrderBy(e => e.Department);
                    break;
                case "employeeid":
                    result = descending
                        ? result.OrderByDescending(e => e.EmployeeId)
                        : result.OrderBy(e => e.EmployeeId);
                    break;

            }
        }
        result = result.Skip((pagingDto.pageNumber - 1) * pagingDto.pageSize)
                .Take(pagingDto.pageSize);

        var FinalResult = await result.ToArrayAsync();

        return FinalResult;
   }
}