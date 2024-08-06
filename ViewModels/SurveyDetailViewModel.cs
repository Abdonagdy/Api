using DataModels;
using System.Collections.Generic;

namespace MizeApi.ViewModels
{
    public class SurveyDetailViewModel
    {
        public int Id { get; set; } // int
        public string SurveyNo { get; set; } // varchar(350)
        public string QuestionDesc { get; set; } // varchar(450)
        public string Answer { get; set; } // varchar(450)
        public int QuestionId { get; set; } // int
        public int OptionId { get; set; }
        public List<SurveyQuestionOption> QuestionAnswerOptions { get; set; }
        public int Rate { get; set; }
    }
}
