using DataModels;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MizeApi.Helper;
using MizeApi.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MizeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly MizeCRMDB _mizeCrmContext;

        public SurveyController(MizeCRMDB mizeCrmContext)
        {
            _mizeCrmContext = mizeCrmContext;
        }
        [HttpGet]
        public async Task<SurveyViewModel> GetSurvey(string surno)
        {
            var master = _mizeCrmContext.SurveyMasters.Where(f => f.SurveyNo == surno).FirstOrDefault();
            if (master != null)
            {
                var sviewModel = new SurveyViewModel
                {
                    SurveyNo = surno,
                    Age = master.Age,
                    City = master.City,
                    CreationUtc = master.CreationUtc,
                    CustomerIp = master.CustomerIp,
                    CustomerName = master.CustomerName,
                    Email = master.Email,
                    Gender = master.Gender,
                    Id = master.Id,
                    IsDone = master.IsDone,
                    Mobile = master.Mobile,
                    Car = master.Car,
                    Processed = master.Processed,
                    UpdateUtc = master.UpdateUtc,
                    SurveyDetails = (from d in _mizeCrmContext.SurveyDetails.Where(v => v.SurveyNo == surno)
                                     select new SurveyDetailViewModel
                                     {
                                         SurveyNo = surno,
                                         Id = d.Id,
                                         Answer = d.Answer,
                                         OptionId = d.OptionId,
                                         QuestionDesc = d.QuestionDesc,
                                         QuestionId = d.QuestionId,
                                     }
                                 ).ToList()
                };
                foreach (var item in sviewModel.SurveyDetails)
                {
                    item.QuestionAnswerOptions = _mizeCrmContext.SurveyQuestionOptions.Where(r => r.QuestionId == item.QuestionId).ToList();
                }

                return sviewModel;
            }
            else
            {
                return new SurveyViewModel
                {
                    SurveyNo = surno,
                    Id = 0,
                    IsDone = true,
                };
            }
        }
        [HttpGet("[action]")]
        public async Task<SurveyViewModel> GetSurveyData(string surno)
        {
            var master = _mizeCrmContext.SurveyMasters.Where(f => f.SurveyNo == surno).FirstOrDefault();
            if(master != null)
            {
                var sviewModel = new SurveyViewModel
                {
                    SurveyNo = surno,
                    Age = master.Age,
                    City = master.City,
                    CreationUtc = master.CreationUtc,
                    CustomerIp = master.CustomerIp,
                    CustomerName = master.CustomerName,
                    Email = master.Email,
                    Gender = master.Gender,
                    Id = master.Id,
                    IsDone = master.IsDone,
                    Mobile = master.Mobile,
                    Car = master.Car,
                    Processed= master.Processed,
                    UpdateUtc = master.UpdateUtc,
                    SurveyDetails = (from d in _mizeCrmContext.SurveyDetails.Where(v => v.SurveyNo == surno)
                                     join v in _mizeCrmContext.SurveyQuestionOptions on new { X1 = d.QuestionId, X2 = d.OptionId } equals new { X1 = v.QuestionId, X2 = v.Id }
                                     select new SurveyDetailViewModel
                                     {
                                         SurveyNo = surno,
                                         Id = d.Id,
                                         Answer = v.OptionValue,
                                         OptionId = d.OptionId,
                                         QuestionDesc = d.QuestionDesc,
                                         QuestionId= d.QuestionId,
                                         Rate = v.OptionRate.ObjToInt(0)
                                     }
                                 ).ToList()
                };
                return sviewModel;
            }
            else
            {
                return new SurveyViewModel
                {
                    SurveyNo = surno,
                    Id = 0,
                    IsDone= true,
                };
            }
        }

        [HttpPost("answer")]
        public async Task FillSurvey(SurveyViewModel surveyData)
        {
            if(surveyData != null)
            {
               var master = _mizeCrmContext.SurveyMasters.Where(t=>t.SurveyNo == surveyData.SurveyNo).UpdateAsync(r=>new SurveyMaster
                {
                    SurveyNo = surveyData.SurveyNo,
                    City= surveyData.City,
                    CustomerIp= surveyData.CustomerIp,
                    IsDone= true,
                    Mobile= surveyData.Mobile,
                    UpdateUtc=DateTime.Now,
                    CustomerName= surveyData.CustomerName,
                    Age=surveyData.Age,
                    Email= surveyData.Email,
                    Gender= surveyData.Gender,
                    Comment= surveyData.Comment,                    
                    
                }).GetAwaiter().GetResult();

                foreach (var line in surveyData.SurveyDetails)
                {
                    _mizeCrmContext.SurveyDetails.Where(r => r.Id == line.Id).UpdateAsync(y => new SurveyDetail
                    {
                        SurveyNo = line.SurveyNo,
                        Answer = line.Answer,
                        OptionId= line.OptionId,
                        
                    }).GetAwaiter().GetResult();
                }
            }
        }

        [HttpPost("Process")]
        public async Task ProcessSurvey(string refno)
        {
            await _mizeCrmContext.SurveyMasters.Where(s => s.RefNo == refno).UpdateAsync(y => new SurveyMaster
            {
                Processed = true,
            });
        }
    }
}
