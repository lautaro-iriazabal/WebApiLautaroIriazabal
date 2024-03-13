using System.Text.Json;
using System.Text;

namespace WebApiLautaroIriazabal.Provider
{
    public abstract class ProveedorAbstracto
    {
        // Atributos privados de la clase
        private readonly HttpClient httpClient;
        protected readonly string host;

        // Constructor de la clase
        protected ProveedorAbstracto()
        {
            // Inicialización del cliente HTTP y del host
            httpClient = new HttpClient();
            this.host = @"https://localhost:5001/api";
        }

        // Método para obtener una cadena de un recurso
        protected virtual string GetString(string recurso)
        {
            // Construcción de la URL
            string url = $"{this.host}/{recurso}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                // Envío de la solicitud y obtención de la respuesta
                HttpResponseMessage? response = httpClient.Send(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    // Lanzamiento de una excepción si la respuesta no es exitosa
                    throw new Exception($"La respuesta no fue exitosa, se obtuvo un estado {response.StatusCode}");
                }

                // Lectura de la respuesta y devolución del resultado
                Task<string>? resultado = response.Content.ReadAsStringAsync();
                resultado.Wait();
                return resultado.Result;
            }
            catch (Exception ex)
            {
                // Lanzamiento de una excepción si ocurre un error
                throw new Exception("Error al procesar la solicitud get de string", ex);
            }
        }

        // Método para obtener un objeto JSON de un recurso
        protected virtual T GetJson<T>(string recurso) where T : class
        {
            // Construcción de la URL
            string url = $"{this.host}/{recurso}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, url);

            try
            {
                // Envío de la solicitud y obtención de la respuesta
                HttpResponseMessage? response = httpClient.Send(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    // Lanzamiento de una excepción si la respuesta no es exitosa
                    throw new Exception($"La respuesta no fue exitosa, se obtuvo un estado {response.StatusCode}");
                }

                // Lectura de la respuesta y devolución del resultado
                Task<T?> resultado = response.Content.ReadFromJsonAsync<T>();
                resultado.Wait();
                return resultado.Result!;
            }
            catch (Exception ex)
            {
                // Lanzamiento de una excepción si ocurre un error
                throw new Exception("Error al procesar la solicitud get de json", ex);
            }
        }

        // Método para enviar una solicitud POST o PUT y obtener una respuesta
        private ApiResponse PostAndPutJson<T>(string recurso, T data, HttpMethod metodo) where T : class
        {
            // Construcción de la URL
            string url = $"{this.host}/{recurso}";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(metodo, url);

            // Serialización de los datos y construcción del contenido de la solicitud
            string dataSerializada = JsonSerializer.Serialize(data);
            httpRequestMessage.Content = new StringContent(dataSerializada, Encoding.UTF8, "application/json");

            try
            {
                // Envío de la solicitud y obtención de la respuesta
                HttpResponseMessage? response = httpClient.Send(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    // Lanzamiento de una excepción si la respuesta no es exitosa
                    throw new Exception($"La respuesta no fue exitosa, se obtuvo un estado {response.StatusCode}");
                }

                // Lectura de la respuesta y devolución del resultado
                Task<ApiResponse?> resultado = response.Content.ReadFromJsonAsync<ApiResponse>();
                resultado.Wait();
                return resultado.Result!;
            }
            catch (Exception ex)
            {
                // Lanzamiento de una excepción si ocurre un error
                throw new Exception($"Error al procesar la solicitud {metodo} del json", ex);
            }
        }

        // Método para enviar una solicitud PUT y obtener una respuesta
        protected virtual ApiResponse PutJson<T>(string recurso, T data) where T : class
        {
            return this.PostAndPutJson(recurso, data, HttpMethod.Put);
        }

        // Método para enviar una solicitud POST y obtener una respuesta
        protected virtual ApiResponse PostJson<T>(string recurso, T data) where T : class
        {
            return this.PostAndPutJson(recurso, data, HttpMethod.Post);
        }

        // Método para enviar una solicitud DELETE y obtener una respuesta
        protected virtual ApiResponse Delete(string recurso)
        {
            // Construcción de la URL
            string url = $"{this.host}/{recurso}";
            HttpMethod httpMethod = HttpMethod.Delete;
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, url);
            try
            {
                // Envío de la solicitud y obtención de la respuesta
                HttpResponseMessage? response = httpClient.Send(httpRequestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    // Lanzamiento de una excepción si la respuesta no es exitosa
                    throw new Exception($"La respuesta no fue exitosa, se obtuvo un estado {response.StatusCode}");
                }

                // Lectura de la respuesta y devolución del resultado
                Task<ApiResponse?> resultado = response.Content.ReadFromJsonAsync<ApiResponse>();
                resultado.Wait();
                return resultado.Result!;
            }
            catch (Exception ex)
            {
                // Lanzamiento de una excepción si ocurre un error
                throw new Exception("Error al procesar la solicitud DELETE", ex);
            }
        }
    }

}
