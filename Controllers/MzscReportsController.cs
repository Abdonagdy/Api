using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MizeApi.Helper;
using MizeApi.Services;
using MizeApi.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace MizeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MzscReportsController : ControllerBase
    {
        private readonly IMzscService _mzscService;

        public MzscReportsController(IMzscService mzscService)
        {
            _mzscService = mzscService;
        }
        [HttpGet("[action]")]
        public async Task<List<TechReportViewModel>> TechReport(int managerId,int yearNo,int monthNo,int branch,int user)
        {
            return _mzscService.GetTechIncomeReport(managerId, yearNo, monthNo, branch,user);
        }
        [HttpGet("[action]")]
        public async Task<List<DataModels.OraDbMsc.GeBranch>> Branches(int user)
        {
            return _mzscService.BranchesList(user).Where(r => r.IsActive.ObjToStringIfIsNullOrEmpty("N") == "Y").ToList();
        }
        [HttpGet("[action]")]
        public async Task<List<DataModels.OraDbMsc.GeDepartment>> Departments(int underDepartment)
        {
            return _mzscService.DepartmentsList(underDepartment).Where(r => r.IsActive.ObjToStringIfIsNullOrEmpty("N") == "Y").ToList();
        }

        [HttpGet("[action]")]
        public async Task<List<DataModels.MZReports.MscSalesAndTarget>> SalesAndTarget(int branch,int year,int month)
        {
            return await _mzscService.GetSalesAndTargetReport(branch,year,month);
        }
    }
}
