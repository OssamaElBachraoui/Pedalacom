using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace Pedalacom.Servizi.Log
{
    public class Log
    {
        private string NameClass { get; set; }
        private string ErrorMessage { get; set; }
        private string ExType { get; set; }
        private string ErrorCode { get; set; }
        private DateTime Date { get; set; }

        public Log(string NameClass, string ErrorMessage, string ExType, string ErrorCode, DateTime date)
        {
            this.NameClass = NameClass;
            this.ErrorMessage = ErrorMessage;
            this.ExType = ExType;
            this.ErrorCode = ErrorCode;
            Date = date;
        }

        public void WriteLog()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionStrings"]))
                {
                    connection.Open();
                    string query = "insert into Errori values(@NomeClasse, @ErroreMessaggio, @TipologiaEccezione, @CodiceErrore, @Data)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("NomeClasse", NameClass);
                        command.Parameters.AddWithValue("ErroreMessaggio", ErrorMessage);
                        command.Parameters.AddWithValue("TipologiaEccezione", ExType);
                        command.Parameters.AddWithValue("CodiceErrore", ErrorCode);
                        command.Parameters.AddWithValue("Data", Date);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
