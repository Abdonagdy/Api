using DataModels;

namespace MizeApi.ViewModels
{
    public class SurveyRegisterViewModel
    {
        //	Mobile	City	Age	Comment	SumRate	BRANCH_NAME	EmpName	PLATE_NO	SEQ	MC_SEQ	SmsSentDate
        public string SurveyNo { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Age { get; set; }
        public string Comment { get; set; }
        public int SumRate { get; set; }
        public string BranchName { get; set; }
        public string EmpName { get; set; }
        public string PlateNo { get; set; }
        public int Seq { get; set; }
        public int McSeq { get; set; }
        public string SmsSentDate { get; set; }
        public string BranchEmail { get; set; }
        public string RefNo { get; set; }
        public bool Processed { get; set; }
        public string Car { get; set; }
    }
}
