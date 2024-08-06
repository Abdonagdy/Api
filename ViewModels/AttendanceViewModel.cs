using DataModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MizeApi.ViewModels
{
    public class AttendanceViewModel
    {
        public decimal EmpNo { get; set; } // decimal(18, 0)
        public string EmpName { get; set; } // nvarchar(403)
        public string SecDesc { get; set; } // nvarchar(50)
        public char? AreaCode { get; set; } // nvarchar(1)
        [Column("days_no")]                                            
        public int DaysNo { get; set; } // int
        [Column("LakeIn_Minute")]
        public string LakeInMinute { get; set; } // nvarchar(61)
        [Column("abs_days")]
        public int AbsDays { get; set; } // int
        [Column("LakeOut_Minute")]
        public string LakeOutMinute { get; set; } // nvarchar(61)
        public int? CntOneFinger { get; set; }
        public int DepCode { get; set; }
        public List<AttendanceDetail> EmpAttDetails { get; set; }
    }
}
