using DataModels;
using System;
using System.Collections.Generic;

namespace MizeApi.ViewModels
{
    public class SurveyViewModel
    {
        public int Id { get; set; } // int
        public string SurveyNo { get; set; } // varchar(350)
        public string CustomerIp { get; set; } // varchar(255)
        public DateTime? CreationUtc { get; set; } // datetime
        public DateTime? UpdateUtc { get; set; } // datetime
        public bool? IsDone { get; set; } // char(1)
        public string Mobile { get; set; } // varchar(50)
        public string CustomerName { get; set; } // nvarchar(150)
        public string City { get; set; } // nvarchar(50)
        public int? Age { get; set; } // int
        public string Email { get; set; } // varchar(50)
        public string Gender { get; set; }
        public string Comment { get; set; }
        public List<SurveyDetailViewModel> SurveyDetails { get; set; }
        public string Car { get; set; }
        public bool Processed { get; set; }
    }
}
