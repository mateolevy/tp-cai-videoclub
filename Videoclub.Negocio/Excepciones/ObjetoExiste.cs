namespace Videoclub.Negocio.Excepciones
{
    internal class ObjetoExiste : Exception
    {
        public ObjetoExiste(string tipoObjeto, string tipoIdentificador, int nroIdentificador) : base ($"El {tipoObjeto} con {tipoIdentificador}: {nroIdentificador} ya existe."){ }
    }
}
