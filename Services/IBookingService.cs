using DataModels;
using System.Threading.Tasks;

namespace MizeApi.Services
{
    public interface IBookingService
    {
        Task<Appointment> GetAppointmentAsync(int id);
    }
}
