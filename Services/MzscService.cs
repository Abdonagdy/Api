using DataModels;
using DataModels.MZReports;
using DataModels.OraDB;
using LinqToDB;
using LinqToDB.Data;
using MizeApi.Helper;
using MizeApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MizeApi.Services
{
    public class MzscService : IMzscService
    {
        private readonly DataModels.OraDbMsc.OraDbMscDB _dbContextOraMscDb;
        private readonly MizePortalDB _dbPortal;
        private readonly MizeCRMDB _mizeCRMDB;
        private readonly MZReportsDB _mZReportsDB;

        public MzscService(DataModels.OraDbMsc.OraDbMscDB dbContext, MizePortalDB dbPortal, MizeCRMDB mizeCRMDB, MZReportsDB mZReportsDB)
        {
            _dbContextOraMscDb = dbContext;
            _dbPortal = dbPortal;
            _mizeCRMDB = mizeCRMDB;
            _mZReportsDB = mZReportsDB;
        }
        public List<TechReportViewModel> GetTechIncomeReport(int managerId, int yearNo, int monthNo, int branchId, int user)
        {
            var userInfo = _dbPortal.UserSCenters.Where(r => r.UserId == user && r.BranchId == branchId).FirstOrDefault();
            if (userInfo != null)
            {
                var res = _dbContextOraMscDb.QueryProc<TechReportViewModel>("GetTechIncomeReport", new DataParameter("managerId", managerId), new DataParameter("monthNo", monthNo), new DataParameter("yearNo", yearNo), new DataParameter("typeCode", 3043), new DataParameter("branchId", branchId), new DataParameter("wshcNo", userInfo.WshcSeq.ObjToInt(9999)), new DataParameter("wshcType", userInfo.WshcType), new DataParameter("techNo", userInfo.TechSeq), new DataParameter("techType", userInfo.TechType)).ToList();
                if (res != null)
                    return res;
                else
                    return new List<TechReportViewModel>();
            }
            else
                return null;
        }
        public List<TechReportViewModel> GetTechIncomeReportNew(int managerId, int monthNo, int branchId, int user)
        {
            var userInfo = _dbPortal.UserSCenters.Where(r => r.UserId == user && r.BranchId == branchId).FirstOrDefault();
            if (userInfo != null)
            {

            }
            var res = _dbContextOraMscDb.QueryProc<TechReportViewModel>("GetTechIncomeReport", new DataParameter("managerId", managerId), new DataParameter("monthNo", monthNo), new DataParameter("typeCode", 3043), new DataParameter("branchId", branchId), new DataParameter("wshcNo", userInfo.WshcSeq.ObjToInt(9999)), new DataParameter("wshcType", userInfo.WshcType), new DataParameter("techNo", userInfo.TechSeq), new DataParameter("techType", userInfo.TechType)).ToList();
            return res;
        }
        public List<DataModels.OraDbMsc.GeBranch> BranchesList(int userId)
        {
            var userscenters = _dbPortal.UserSCenters.ToList();

            var brlist = (from a in _dbContextOraMscDb.GeBranches
                          join b in userscenters
                          on a.BranchId equals b.BranchId
                          where b.UserId == userId
                          select a
                          ).ToList();
            if (userId == 0)
                brlist = _dbContextOraMscDb.GeBranches.ToList();

            return brlist.Where(b=>b.SubCode.ObjToDecimal(0) > 1).ToList();
        }
        public DataModels.OraDbMsc.GeBranch GetBranch(int branchid)
        {
            return _dbContextOraMscDb.GeBranches.FirstOrDefault(b => b.BranchId == branchid);
        }
        public List<DataModels.OraDbMsc.GeDepartment> DepartmentsList(int parentId)
        {
            var res = _dbContextOraMscDb.QueryProc<DataModels.OraDbMsc.GeDepartment>("GetDepartments", new DataParameter("DEP_CODE", parentId));
            return res.ToList();
        }
        public async Task<List<Signage>> GetSignageList()
        {
            return _dbPortal.Signages.Where(s => s.IsActive == true).OrderBy(r => r.DisplayOrder).ToList();
        }
        public async Task<List<VwContact>> GetContacts(string search)
        {
            if (search != null)
                return _mizeCRMDB.VwContacts.Where(r => r.CusMobile.StartsWith(search) || r.CusName.StartsWith(search)).ToList();
            else
                return _mizeCRMDB.VwContacts.ToList();
        }
        public async Task<List<ContactMCard>> GetContactsMaster(string search,int branch)
        {
            if (search == null)
                search = " ";
            return _mizeCRMDB.QueryProc<ContactMCard>("GetContactsMaster", new DataParameter("searchKey", search), new DataParameter("branchId", branch)).ToList();
        }
        public async Task<List<ContactMCard>> GetContactMCards(string mobileno)
        {
            if (mobileno == null)
                mobileno = " ";
            return _mizeCRMDB.QueryProc<ContactMCard>("GetContactMCards", new DataParameter("customerMobile", mobileno)).ToList();
        }
        public async Task<List<Complaint>> GetComplaints(string search = null)
        {
            if (search == null)
                return _mizeCRMDB.Complaints.ToList();
            else
                return _mizeCRMDB.Complaints.Where(r => r.ComplaintTile.Contains(search)).ToList();
        }
        public async Task<ComplaintHistory> AddNewComplaint(ComplaintHistory model)
        {
            var id = _mizeCRMDB.ComplaintHistories.InsertWithIdentityAsync(() => new ComplaintHistory
            {
                BranchId = model.BranchId,
                ComplaintId = model.ComplaintId,
                CreatedUtc = DateTime.UtcNow,
                EmpNo = model.EmpNo,
                FullDescription = model.FullDescription,
                MobileNo = model.MobileNo,
                Priority = model.Priority,
                Ser = model.Ser,
                UserId = model.UserId
            });
            var compResult = await _mizeCRMDB.ComplaintHistories.FirstOrDefaultAsync(r => r.Id == id.Result.ObjToInt(0));
            return compResult;
        }
        public async Task<List<ComplaintHistory>> GetComplaintsHitory(string search = null)
        {
            if (search == null)
                return _mizeCRMDB.ComplaintHistories.ToList();
            else
                return _mizeCRMDB.ComplaintHistories.Where(r => r.FullDescription.Contains(search)).ToList();
        }

        public async Task<List<DataModels.MZReports.MscSalesAndTarget>> GetSalesAndTargetReport(int branchId, int yearNo, int monthNo)
        {
            return _mZReportsDB.QueryProc<DataModels.MZReports.MscSalesAndTarget>("SalesAndTarget", new DataParameter("branchId", branchId), new DataParameter("yearNo", yearNo), new DataParameter("monthNo", monthNo)).ToList();
        }

        public async Task<List<SurveyRegisterViewModel>> GetSurveyRegister(string fromDate = null)
        {
            if (fromDate == null)
                return _mizeCRMDB.QueryProc<SurveyRegisterViewModel>("GetSurveyMasterRegister", new DataParameter("fromDate", DateTime.Now.AddYears(-1).Date), new DataParameter("toDate", DateTime.Now.Date)).ToList();
            else
                return _mizeCRMDB.QueryProc<SurveyRegisterViewModel>("GetSurveyMasterRegister", new DataParameter("fromDate", fromDate.ObjToDateTimeElseNow().Date), new DataParameter("toDate", DateTime.Now.Date)).ToList();
        }


    }
}
