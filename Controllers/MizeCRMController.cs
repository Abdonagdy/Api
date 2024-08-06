using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MizeApi.Services;
using MizeApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MizeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MizeCRMController : ControllerBase
    {
        private readonly IMzscService _mzscService;

        public MizeCRMController(IMzscService mzscService)
        {
            _mzscService = mzscService;
        }

        [HttpGet("Contacts")]
        public Task<List<VwContact>> GetContacts(string search)
        {
            return _mzscService.GetContacts(search);
        }
        [HttpGet("ContactsIndex")]
        public Task<List<ContactMCard>> GetContactsIndex(string search,int branch = 0)
        {
            return _mzscService.GetContactsMaster(search, branch);
        }
        [HttpGet("CustomerMc")]
        public Task<List<ContactMCard>> GetCustomerMaintenanceCards(string search)
        {
            return _mzscService.GetContactMCards(search);
        }
        [HttpGet("Complaints")]
        public Task<List<Complaint>> GetComplaints(string search = null)
        {
            return _mzscService.GetComplaints(search);
        }
        [HttpPost("AddComplaint")]
        public async Task<ComplaintHistory> AddCustomerComplaint([FromBody] ComplaintHistory model)
        {
            return await _mzscService.AddNewComplaint(model);
        }
        [HttpGet("ComplaintsHistory")]
        public Task<List<ComplaintHistory>> GetComplaintsHistory(string search = null)
        {
            var result = _mzscService.GetComplaintsHitory(search);
            return result;
        }
        [HttpGet("SurveyRegister")]
        public Task<List<SurveyRegisterViewModel>> GetSurveyRegister(string fromDate = null)
        {
            var result = _mzscService.GetSurveyRegister(fromDate);
            return result;
        }
    }
}