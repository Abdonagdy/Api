using System.Collections.Generic;

namespace MizeApi.ViewModels
{
    public class EmailViewModel
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public List<string> CCEMail { get; set; }
    }
}
