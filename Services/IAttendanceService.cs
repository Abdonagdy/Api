using DataModels;
using MizeApi.ViewModels;
using System.Collections.Generic;

namespace MizeApi.Services
{
    public interface IAttendanceService
    {
        List<AttendanceDetail> EmployeeDetailedAttReportLast30(int empno, string fromDate, string toDate, int sectionId = 0);
        List<AttendanceSummary> EmployeeSummaryAttReportLast30(int empNo, string fromDate, string toDate, int sectionId = 0);
        List<AttendanceViewModel> EmployeeSummaryAttReport(int empNo, string fromDate, string toDate, int sectionId = 0);
    }
}
