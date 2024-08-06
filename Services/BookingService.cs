using DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace MizeApi.Services
{
    public class BookingService : IBookingService
    {
        private readonly MizeCRMDB _dbCRMContext;

        public BookingService(MizeCRMDB dbCRMContext)
        {
            _dbCRMContext = dbCRMContext;
        }

        public Task<Appointment> GetAppointmentAsync(int id)
        {
            return Task.FromResult<Appointment>(_dbCRMContext.Appointments.FirstOrDefault(v=>v.Id == id));
        }
    }
}
