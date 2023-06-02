namespace Videoclub.Negocio.Excepciones
{
    internal class ObjetoNoExiste : Exception
    {
        public ObjetoNoExiste(string tipoObjeto, string tipoIdentificador, int nroIdentificador) : base($"El {tipoObjeto} con {tipoIdentificador}: {nroIdentificador} no existe.") { }   
    }
}
