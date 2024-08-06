using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MizeApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using MizeApi.Helper;
using MizeApi.Services;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MizeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly MizeCRMDB _dbContext;
        private readonly IMzscService _mzscService;
        private readonly IEmailSender _emailSender;
        private readonly IBookingService _bookingService;
        private readonly IConfiguration _configuration;

        public AppointmentController(MizeCRMDB dbContext,
            IMzscService mzscService,
            IEmailSender emailSender,
            IBookingService bookingService,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mzscService = mzscService;
            _emailSender = emailSender;
            _bookingService = bookingService;
            _configuration = configuration;
        }
        [HttpGet("check-date")]
        public async Task<List<Appointment>> CheckDate(int branchId, string date)
        {
            var resList = _dbContext.Appointments.Where(v => v.BranchId == branchId && v.Date.ToString() == date).Where(v => v.Reserved == false)
                ;
            if (Convert.ToDateTime(date).Date.Year == DateTime.Now.Year && Convert.ToDateTime(date).Date.Month == DateTime.Now.Month && Convert.ToDateTime(date).Date.Day == DateTime.Now.Day)
                return resList.Where(c => Convert.ToDateTime(c.Time).Hour > DateTime.Now.Hour).OrderBy(b => b.Time).ToList();
            else return resList.ToList();
        }

        [HttpPost("reserve")]
        public async Task<ActionResult<Appointment>> ReserveAppointment(string reservation, int branch, string mobile, string name)
        {
            var appo = _dbContext.Appointments.Where(r => r.ReservationNo == reservation).FirstOrDefault();
            if (appo == null)
            {
                return NotFound();
            }
            //reservation has been founded
            //We will check If same customer try to reserve another appointment in 4 days range
            var customerHasAnotherAppointmentsInThisRange = _dbContext.Appointments.Where(d => d.Date <= appo.Date.AddDays(2) && d.Date >= appo.Date.AddDays(-2)).Where(b=> b.Mobile == mobile).FirstOrDefault();
            if (customerHasAnotherAppointmentsInThisRange != null)
                return Forbid();
            else
            {
                await _dbContext.Appointments.Where(v => v.ReservationNo == reservation).UpdateAsync(c => new Appointment
                {
                    CustomerName = name,
                    BranchId = branch,
                    Reserved = true,
                    Mobile = mobile,
                });
                await _dbContext.CommitTransactionAsync();
                DataModels.OraDbMsc.GeBranch br = _mzscService.GetBranch(branch);
                Appointment bookInfo = await _bookingService.GetAppointmentAsync(appo.Id);
                await new SMSHelper().SendSmsMessage(mobile, $"تم حجز موعد صيانة رقم {appo.Id.ToString()} , {br.BranchName} \n تاريخ : {bookInfo.Date.ToString("yyyy-MM-dd")} \n الوقت : {bookInfo.Time}");

                await _emailSender.SendEmailAsync(br.AddressE, "موعد صيانة", $"تم حجز موعد صيانة للعميل {name} \n رقم جوال  {mobile} , \n  تاريخ  {bookInfo.Date.ToString("yyyy-MM-dd")} \n الوقت : {bookInfo.Time}", new List<string> {
            _configuration.GetValue<string>("CustomerServiceEmail")
            });

                Task.Delay(5000);

                return appo;
            }
            
        }
    }
}
