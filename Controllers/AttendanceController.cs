using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MizeApi.Services;
using MizeApi.ViewModels;
using System.Collections.Generic;

namespace MizeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }
        [HttpGet("[action]")]
        public List<AttendanceSummary> EmployeeSummary(int empno,int section,string fromDate,string toDate)
        {
            return _attendanceService.EmployeeSummaryAttReportLast30(empno,fromDate,toDate, section);
        }
        [HttpGet("[action]")]
        public List<AttendanceDetail> EmployeeDetailed(int empno,int section, string fromDate, string toDate)
        {
            return _attendanceService.EmployeeDetailedAttReportLast30(empno, fromDate, toDate, section);
        }

        [HttpGet("[action]")]
        public List<AttendanceViewModel> EmployeeAttReport(int empno, int section, string fromDate, string toDate)
        {
            return _attendanceService.EmployeeSummaryAttReport(empno, fromDate, toDate, section);
        }
    }
}
