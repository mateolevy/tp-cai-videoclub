using System.Net;
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
            var res = await Client.GetAsync(url);
            var response = await res.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(response) || res.StatusCode != HttpStatusCode.OK)
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
            var content = MapValues(data);
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
    
    private static FormUrlEncodedContent MapValues<T>(T type) where T: class
    {
        var properties = type.GetType().GetProperties();
        var values = new Dictionary<string, string>();
        foreach (var property in properties)
        {
            values.Add(property.Name, property.GetValue(type).ToString());
        }

        return new FormUrlEncodedContent(values);
    }
} 
