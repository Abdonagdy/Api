namespace MizeApi.ViewModels
{
    public class TechReportViewModel
    {
        public int TECH_SEQ { get; set; }
        public int EMP_NO { get; set; }
        public string NAME { get; set; }
        public decimal MAIN_VAL_TARGET { get; set; }
        public decimal MAINT_VALUE { get; set; }
        public decimal TECH_TAR_PERC { get; set; }
        public int Manager { get; set; }
        public int BranchId { get; set; }
        public decimal CARS_COUNT_TARGET { get; set; }
        public decimal CARS_COUNT { get; set; }


    }
}
