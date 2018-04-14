using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;

namespace XamarinForms_PrismExample.Services
{
    public interface IGeoLocationService
    {
        bool GeolocatorIsSupported();
        Task<Position> GetCurrentPositionAsync();
    }
}