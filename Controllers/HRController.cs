using DataModels.OraDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MizeApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MizeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HRController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public HRController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("Employees")]
        public Task<List<Employee>> EmployeesList(string search)
        {
            return _employeeService.EmployeeSearch(search);
        }
    }
}