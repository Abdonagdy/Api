using DataModels;
using DataModels.OraDB;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MizeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly OraDbDB _dbContext;

        public EmployeeService(OraDbDB dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Employee>> EmployeeSearch(string search)
        {
            return await _dbContext.Employees.Where(r => r.FullEmpName.Contains(search)).ToListAsync();
        }

        public Employee GetEmployeeByNo(int empno)
        {
            return _dbContext.Employees.Where(r => r.SEQ == empno).FirstOrDefault();
        }
    }
}
