using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pro.Models
{
    public class Acknowledgment
    {
        [Key]
        public int AckId { get; set; }
        public bool KeepAccountOpen { get; set; }
        public bool ReceiveInformation { get; set; }
        public bool AgreeToTerms { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
