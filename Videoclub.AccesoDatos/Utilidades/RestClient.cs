using System.Text;
using System.Text.Json;
using Videoclub.AccesoDatos.Utilidades;

internal static class RestClient
{
    private static readonly HttpClient Client = new HttpClient();

    static RestClient()
    {
        Client.BaseAddress = new Uri("https://cai-api.azurewebsites.net/api/v1/");
    }
    
    internal static async Task<RestResponse<T>> GetAsync<T>(string url) where T: class
    {
        RestResponse<T> restResponse = new RestResponse<T>
        {
            Data = default,
            Success = false
        };
        try
        {
            string response = await Client.GetStringAsync(url);
            if (!string.IsNullOrWhiteSpace(response))
            {
                restResponse.Data = JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
                restResponse.Success = true;
            }
        }
        catch (Exception ex)
        {
            restResponse.Success = false;
            restResponse.Data = default;
            restResponse.Error = ex.Message;
        }

        return restResponse;
    }
    
    internal static async Task<RestResponse<T>> PostAsync<T>(string url, T data) where T : class
    {
        RestResponse<T> restResponse = new RestResponse<T>
        {
            Data = default,
            Success = false
        };
        try
        {
            string jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PostAsync(url, content);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseContent))
            {
                restResponse.Data = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                restResponse.Success = true;
            }
        }
        catch (Exception ex)
        {
            restResponse.Success = false;
            restResponse.Data = default;
            restResponse.Error = ex.Message;
        }

        return restResponse;
    }

    internal static async Task<RestResponse<T>> PutAsync<T>(string url, T data) where T : class
    {
        RestResponse<T> restResponse = new RestResponse<T>
        {
            Data = default,
            Success = false
        };
        try
        {
            string jsonData = JsonSerializer.Serialize(data);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Client.PutAsync(url, content);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseContent))
            {
                restResponse.Data = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                restResponse.Success = true;
            }
        }
        catch (Exception ex)
        {
            restResponse.Success = false;
            restResponse.Data = default;
            restResponse.Error = ex.Message;
        }

        return restResponse;
    }

    internal static async Task<RestResponse<T>> DeleteAsync<T>(string url) where T : class
    {
        RestResponse<T> restResponse = new RestResponse<T>
        {
            Data = default,
            Success = false
        };
        try
        {
            HttpResponseMessage response = await Client.DeleteAsync(url);
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && !string.IsNullOrWhiteSpace(responseContent))
            {
                restResponse.Data = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                restResponse.Success = true;
            }
        }
        catch (Exception ex)
        {
            restResponse.Success = false;
            restResponse.Data = default;
            restResponse.Error = ex.Message;
        }

        return restResponse;
    }
}
