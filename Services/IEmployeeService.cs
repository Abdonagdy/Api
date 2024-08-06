using DataModels;
using DataModels.OraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MizeApi.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> EmployeeSearch(string search);
        Employee GetEmployeeByNo(int empno);
    }
}
