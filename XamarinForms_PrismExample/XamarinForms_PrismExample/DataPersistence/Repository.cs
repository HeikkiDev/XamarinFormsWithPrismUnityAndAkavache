using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Models;
using System.Reactive.Linq;   // IMPORTANT - this makes await work!
using Akavache;

namespace XamarinForms_PrismExample.DataPersistence
{
    /// <summary>
    /// 
    /// </summary>
    public class Repository : IRepository
    {
        private IRemoteDatabase _remoteDatabase; // Falta implementar todo menos el GetItemAsync y el GetItemsAsync en RemoteDatabase...
        private IBlobCache _cache;

        public Repository(IRemoteDatabase remoteDatabase)
        {
            _remoteDatabase = remoteDatabase;

            _cache = BlobCache.LocalMachine;
        }

        public IObservable<TCollection> GetItems<T, TCollection>(string apiUri, string[] queryArgs) where T : EntityBase, new() where TCollection : EntityCollectionBase<T>, new()
        {
            /*
                 * Opción 1. Se comprueba si hay info cacheada. Si no la hay se hace la request, si la hay no se hace la request y se da la cacheada.
                 * Definimos un tiempo máximo (offset) para que cada cierto tiempo (5 min) se haga la request aunque esté la info cacheada
                 
                var cachedMovies = cache.GetAndFetchLatest(
                    "movies",
                    async () => await _remoteDatabase.GetItemsAsync<TCollection>(apiUri, queryArgs),
                    offset =>
                    {
                        TimeSpan elapsed = DateTimeOffset.Now - offset;
                        return elapsed > new TimeSpan(hours: 0, minutes: 5, seconds: 0);
                    });

                var movies = await cachedMovies.FirstOrDefaultAsync();
                return movies;
                */


            /*
             * Opción 2. Mediante cachedMovies.Suscribe se manda inmediatamente la info cacheada y al mismo tiempo se hace la request para la nueva info. Cuando llega la info se envía de nuevo a la suscripción.
             * Durante bloques de 5 minutos si hay info cacheada sólo la lee de ahí. Es decir, cuando pasan 5 min desde la última request a la API, se vuelve a hacer la request.
             */
            var cachedMovies = _cache.GetAndFetchLatest(
                typeof(TCollection).Name,
                async () => await _remoteDatabase.GetItemsAsync<TCollection>(apiUri, queryArgs),
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return elapsed > new TimeSpan(hours: 0, minutes: 5, seconds: 0);
                });

            return cachedMovies;
        }

        public IObservable<T> GetById<T>(string apiUri, long id, string[] queryArgs) where T : EntityBase, new()
        {
            /*
             * Mediante cachedMovie.Suscribe se manda inmediatamente la info cacheada y al mismo tiempo se hace la request para la nueva info. Cuando llega la info se envía de nuevo a la suscripción.
             * Durante bloques de 5 minutos si hay info cacheada sólo la lee de ahí. Es decir, cuando pasan 5 min desde la última request a la API, se vuelve a hacer la request.
             */
            var cachedMovie = _cache.GetAndFetchLatest(
                typeof(T).Name + "_" + id.ToString(),
                async () => await _remoteDatabase.GetItemAsync<T>(apiUri, id, queryArgs),
                offset =>
                {
                    TimeSpan elapsed = DateTimeOffset.Now - offset;
                    return elapsed > new TimeSpan(hours: 0, minutes: 5, seconds: 0);
                });
            
            return cachedMovie;
        }

        public async Task<bool> Create<T>(T entity) where T : EntityBase, new()
        {
            try
            {
                // Aquí se haría la llamada con await a la API Rest con _remoteDatabase para CREAR el elemento en el servidor

                // Si ha ido bien se hace lo mismo en caché
                await _cache.InsertObject<T>(
                    typeof(T).Name + "_" + entity.id.ToString(),
                    entity,
                    DateTimeOffset.Now.AddHours(1));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Delete<T>(T entity) where T : EntityBase, new()
        {
            try
            {
                // Aquí se haría la llamada con await a la API Rest con _remoteDatabase para BORRAR el elemento en el servidor

                // Si ha ido bien se hace lo mismo en caché
                await _cache.InvalidateObject<T>(typeof(T).Name + "_" + entity.id.ToString());
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> Update<T>(T entity) where T : EntityBase, new()
        {
            try
            {
                // Aquí se haría la llamada con await a la API Rest con _remoteDatabase para ACTUALIZAR el elemento en el servidor

                // Si ha ido bien se hace lo mismo en caché
                await _cache.InvalidateObject<T>(typeof(T).Name + "_" + entity.id.ToString()); // Delete from cache --> Quizá esto no hace falta... Tengo que probarlo aún sólo con Insert
                await _cache.InsertObject<T>(typeof(T).Name + "_" + entity.id.ToString(), entity, DateTimeOffset.Now.AddHours(1)); // Insert new item in cache
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
