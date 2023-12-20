namespace Pedalacom.Servizi.Log
{
    public class Log
    {
        //metti il tuo path commentato assoluto come il mio
        string path = "C:\\Users\\stefa\\Desktop\\Accademy Betacom\\Pedalacom\\Servizi\\Log\\log.txt";//Path stefan
        private string NameClass { get; set; }
        private string ErrorMessage { get; set; }

        private string ExType { get; set; }
        private string ErrorCode { get; set; }
        private DateTime date { get; set; }

        public Log(string NameClass, string ErrorMessage, string ExType, string ErrorCode, DateTime date)
        {

            this.NameClass = NameClass;
            this.ErrorMessage = ErrorMessage;
            this.ExType = ExType;
            this.ErrorCode = ErrorCode;
            this.date = date;


        }

        public void WriteLog()
        {

            string err = "Nome Classe: " + NameClass + ", Errore Messaggio: " + ErrorMessage + ", Tipologia di Eccezione: " + ExType + ", Codice di errore: " + ErrorCode + ", Data: " + date.ToLongDateString() + "\n";
            try
            {

                File.AppendAllText(path, err);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);

            }




        }

        public void WriteLog(string msg)
        {
            try
            {

                File.AppendAllText(path, msg);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);

            }
        }


    }
}
