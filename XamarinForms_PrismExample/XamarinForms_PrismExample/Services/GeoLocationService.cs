using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinForms_PrismExample.Services
{
    /// <summary>
    /// Cross-platform GeoLocation Service using GeoLocator Xamarin Plugin
    /// </summary>
    public class GeoLocationService : IGeoLocationService
    {
        // Constructor
        public GeoLocationService()
        {

        }

        /// <summary>
        /// Check if GeoLocation is supported at current platform
        /// </summary>
        /// <returns><c>true</c> si la geolocalización está disponible; <c>false</c> en caso contrario.</returns>
        public bool GeolocatorIsSupported()
        {
            return CrossGeolocator.IsSupported;
        }

        /// <summary>
        /// Obtiene la posición actual mendiante geolocalización
        /// </summary>
        /// <returns>Posición geográfica actual</returns>
        public async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentPositionAsync()
        {
            return await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(10));
        }
    }
}
