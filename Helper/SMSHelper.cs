using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MizeApi.Helper
{
    public class SMSHelper
    {
        public async Task<bool> SendSmsMessage(string mobile, string msgbody)
        {
            try
            {
                if (mobile.ObjToStringIfIsNullOrEmpty(string.Empty) != string.Empty)
                {
                    string url = $"http://mshastra.com/sendurlcomma.aspx?user=20099206&pwd=Mize@388&senderid=20099206&CountryCode=+966&mobileno={mobile}&msgtext={msgbody}&smstype=0";
                    HttpClient http = new HttpClient();
                    http.BaseAddress = new Uri(url);
                    await http.GetAsync(http.BaseAddress);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
