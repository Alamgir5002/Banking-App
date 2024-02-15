using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace bank_app_backend.Models
{
    public enum TransactionTypes
    {
        CREDIT, TRANSFER // we dont like all caps names in general; its considered YELLING
    }

    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }

        public TransactionTypes TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Guid AccountId { get; set; }

        [JsonIgnore]
        public Account? Account { get; set; }
    }

    public class TransactionExtended : Transaction
    {
        public Guid? DepositorAccountId { get; set; }
    }
}
