using DataModels;
using DataModels.MZReports;
using DataModels.OraDbMsc;
using MizeApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MizeApi.Services
{
    public interface IMzscService
    {
		List<DataModels.OraDbMsc.GeBranch> BranchesList(int userId);
        List<DataModels.OraDbMsc.GeDepartment> DepartmentsList(int parentId);
        Task<List<Signage>> GetSignageList();
        List<TechReportViewModel> GetTechIncomeReport(int managerId, int yearNo, int monthNo, int branchId, int user);
        Task<List<VwContact>> GetContacts(string search);
        Task<List<ContactMCard>> GetContactMCards(string mobileno);
        Task<List<ContactMCard>> GetContactsMaster(string search,int branch);
        Task<ComplaintHistory> AddNewComplaint(ComplaintHistory model);
        Task<List<Complaint>> GetComplaints(string search = null);
        Task<List<ComplaintHistory>> GetComplaintsHitory(string search = null);
        Task<List<MscSalesAndTarget>> GetSalesAndTargetReport(int branchId, int yearNo, int monthNo);
        Task<List<SurveyRegisterViewModel>> GetSurveyRegister(string fromDate = null);
        GeBranch GetBranch(int branchid);
    }
}
