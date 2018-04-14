using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using XamarinForms_PrismExample.Constants;

namespace XamarinForms_PrismExample.Services
{
    class ApiService : IApiService
    {
        readonly HttpClient _httpClient = new HttpClient();

        public ApiService()
        {

        }

        /// <summary>
        /// Get asynchronously specific resources from API Rest defined in ApiConstants.API_HOST
        /// </summary>
        /// <typeparam name="T">Object Type to deserialize</typeparam>
        /// <param name="apiUri">Relative Api Rest Uri to specific resource</param>
        /// <param name="queryArgs">Parameters array to query relative apiUri</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string apiUri, string[] queryArgs)
        {
            Uri uri = FabricateUrl(apiUri, queryArgs);
            return await GetAsync<T>(uri);
        }

        private async Task<T> GetAsync<T>(Uri WebServiceUrl)
        {
            try
            {
                CheckConnection();
                Debug.WriteLine($">>> Get request {WebServiceUrl} ");
                var response = await _httpClient.GetAsync(WebServiceUrl);
                Debug.WriteLine($"<<< Get response {WebServiceUrl} ");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<T>(content);
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Check Connectivity using Xam.Plugin.Connectivity
        /// </summary>
        private void CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new Exception("Connection error, check the connection");
            }
        }

        /// <summary>
        /// Transforma la ruta relativa a la que queremos acceder a una absoluta
        /// Esto en realidad no es genérico ya que se están usando parámetros propios de la api de TheMovieDb como 'api_key'..
        /// Ver cómo hacerlo genérico, ya que de esta clase heredará cada EntidadService...
        /// </summary>
        /// <param name="apiUri">ruta relativa a recursos de la api</param>
        /// <param name="queryArgs">parametros para la query</param>
        /// <returns></returns>
        private Uri FabricateUrl(string apiUri, string[] queryArgs)
        {
            string apiUriWithParams = string.Format(apiUri, queryArgs); // Aquí se sustituyen los parámetros en el string apiUri
            return new Uri($"{ApiConstants.API_PROTOCOL}://{ApiConstants.API_HOST}/{apiUriWithParams}&api_key={ApiConstants.ApiKey}", UriKind.Absolute);
        }
    }
}
