using System;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json") // Specify the appsettings.json file
                    .Build();

                string connectionString = configuration.GetConnectionString("AdventureDB");

                using (SqlConnection connection = new SqlConnection(connectionString))
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
