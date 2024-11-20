using JewerlyGala.Application.Interfaces;
namespace JewerlyGala.Infrastructure.Services
{
    public class DateTimeService: IDateTime 
    {
        public DateTime Now => DateTime.Now;
    }
}
