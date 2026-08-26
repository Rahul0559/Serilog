using EmployeeApi.DTOs;

namespace EmployeeApi.Interfaces;

public interface IEmployeeDepartmentGet
{
    Task<IEnumerable<EmployeeDepartmentResponseDto>> GetEmployeesWithDepartmentAsync();
    Task<IEnumerable<EmployeeDepartmentResponseDto>> GetEmployeesWithDepartmentAsyncById(int id);
}