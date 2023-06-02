namespace Videoclub.Negocio.Excepciones
{
    internal class ObjetoNull : Exception
    {
        public ObjetoNull(string tipoObjeto):base($"El objeto: {tipoObjeto} ingresado es inválido.") { }
    }
}
