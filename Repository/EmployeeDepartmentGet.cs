using ExpressionFilterApi.Data;
using EmployeeApi.DTOs;
using EmployeeApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace EmployeeApi.Repositories;

public class EmployeeDepartmentGet : IEmployeeDepartmentGet
{
    private readonly AppDbContext _dbContext;

    public EmployeeDepartmentGet(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<EmployeeDepartmentResponseDto>> GetEmployeesWithDepartmentAsync()
    {
    var result = await _dbContext.Departments
        .Join(
            _dbContext.Employees,
            department => department.DepartmentName,
            employee => employee.Department,
            (department, employee) => new EmployeeDepartmentResponseDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Address = employee.Address,
                Contact = employee.Contact,
                Department = employee.Department,

                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            })
        .ToListAsync();

        return result;
    }
     public async Task<IEnumerable<EmployeeDepartmentResponseDto>> GetEmployeesWithDepartmentAsyncById(int id)
    {
    var result = await _dbContext.Departments
        .Join(
            _dbContext.Employees
            .Where(e => e.EmployeeId== id),
            department => department.DepartmentName,
            employee => employee.Department,
            (department, employee) => new EmployeeDepartmentResponseDto
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Address = employee.Address,
                Contact = employee.Contact,
                Department = employee.Department,

                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName
            })
        .ToListAsync();
        Log.Logger.Information("{@result}",result);
        return result;
    }
}