using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class Errori
    {
        [Key]
        public int IdErrore { get; set; }
        public string NomeClasse { get; set; }
        public string ErroreMessaggio { get; set; }
        public string TipologiaErrore { get; set; }

        public string CodiceErrore { get; set; }
        public DateTime DataErrore { get; set; }
    
}
}
