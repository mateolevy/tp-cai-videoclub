namespace Videoclub.Negocio.Excepciones
{
    internal class TransactionError : Exception
    {
        public TransactionError(string error) : base($"Se produjo el siguiente error: {error}.") { }
    }
}
