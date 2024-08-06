using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MizeApi.Helper;
using MizeApi.Services;
using MizeApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MizeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MizeController : ControllerBase
    {
        private readonly IMzscService _mzscService;
        private readonly IEmailSender _emailSender;

        public MizeController(IMzscService mzscService,IEmailSender emailSender)
        {
            _mzscService = mzscService;
            _emailSender = emailSender;
        }
        [HttpGet("[action]")]
        public async Task<List<Signage>> SignageList() 
        { 
            return await _mzscService.GetSignageList();
        }
        [HttpPost("Email")]
        public async Task SendEmail([FromBody] EmailViewModel model)
        {
            await _emailSender.SendEmailAsync(model.ToEmail, model.Subject, model.MessageBody,model.CCEMail);
        }
    }
}
