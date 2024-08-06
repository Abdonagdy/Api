using System.Collections.Generic;
using System.Threading.Tasks;

namespace MizeApi.Helper
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message,List<string> cc);
    }
}
