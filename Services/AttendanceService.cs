using DataModels;
using LinqToDB.Data;
using MizeApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MizeApi.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly MizePortalDB _contextMizePortal;

        public AttendanceService(MizePortalDB context)
        {
            _contextMizePortal = context;
        }
        public List<AttendanceSummary> EmployeeSummaryAttReportLast30(int empNo, string fromDate, string toDate, int sectionId = 0)
        {
            var query = _contextMizePortal.QueryProc<AttendanceSummary>("EmployeeSummaryAttReportLast30_1",
                   new DataParameter("empno", empNo),
                   new DataParameter("sectionId", sectionId),
                   new DataParameter("fromDate", fromDate),
                   new DataParameter("toDate", toDate)
               );
            return query.ToList();
        }
        public List<AttendanceViewModel> EmployeeSummaryAttReport(int empNo, string fromDate, string toDate, int sectionId = 0)
        {
            var query = _contextMizePortal.QueryProc<AttendanceViewModel>("EmployeeSummaryAttReportLast30_1",
                   new DataParameter("empno", empNo),
                   new DataParameter("sectionId", sectionId),
                   new DataParameter("fromDate", fromDate),
                   new DataParameter("toDate", toDate)
               );
            var dtl = new List<AttendanceDetail>();
            var summ = query.ToList();
            foreach (var item in summ)
            {
                dtl = new List<AttendanceDetail>();
                dtl = _contextMizePortal.QueryProc<AttendanceDetail>("EmployeeDetailedAttReportLast30_1",
                   new DataParameter("empno", item.EmpNo),
                   new DataParameter("sectionId", sectionId),
                   new DataParameter("fromDate", fromDate),
                   new DataParameter("toDate", toDate)
               ).ToList();
                item.EmpAttDetails = dtl;
            }
            return summ;
        }
        public List<AttendanceDetail> EmployeeDetailedAttReportLast30(int empNo, string fromDate, string toDate, int sectionId = 0)
        {
            var query = _contextMizePortal.QueryProc<AttendanceDetail>("EmployeeDetailedAttReportLast30_1",
                   new DataParameter("empno", empNo),
                   new DataParameter("sectionId", sectionId),
                   new DataParameter("fromDate", fromDate),
                   new DataParameter("toDate", toDate)
               );

            return query.ToList();
        }
    }
}
