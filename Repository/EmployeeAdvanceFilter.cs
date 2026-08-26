using EmployeeApi.Repository.Interface;
using ExpressionFilterApi.DTOs;
using ExpressionFilterApi.Data;
using ExpressionFilterApi.DTOs.Helper;
using ExpressionFilterApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpressionFilterApi.Repository;

public class EmployeeAdavnceFilter : IEmployeeAdavnceFilter
{
    private readonly AppDbContext _dbcontext;
    private readonly ApplyFilters _filterBuilder;


    public EmployeeAdavnceFilter(AppDbContext dbcontext,  ApplyFilters filterBuilder)
    {
        _dbcontext = dbcontext;
        _filterBuilder = filterBuilder;
    }

    public async Task<IEnumerable<ResponseDto>> EmployeeGetByAdvanceFilter(List<FilterRequest> filters)
    {
        IQueryable<Employee> query = _dbcontext.Employees
            .Where(e => e.IsActive);

        // Apply advanced filters
        query = _filterBuilder.AdvanceFilters(query,filters);

        // Convert Employee -> ResponseDto and execute query
        var result = await query
            .Select(e => new ResponseDto
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Address = e.Address,
                Contact = e.Contact,
                Department = e.Department,
                IsActive = e.IsActive,
                IsDeleted = e.IsDeleted
            })
            .ToListAsync();

        return result;
    }
    }