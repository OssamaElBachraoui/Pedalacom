namespace Pedalacom.Servizi.Eccezioni
{
    public class NotFoundException:Exception
    {
        public string message { get; set; }
        public NotFoundException(){ }

        public NotFoundException(string message):base(message) {

            this.message = message;
        }
    }
}
