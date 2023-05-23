namespace Videoclub.AccesoDatos.Utilidades;

public class RestResponse<T> where T : class
{
    public T Data { get; set; }
    public string Error { get; set; }
    public bool Success { get; set; }
}