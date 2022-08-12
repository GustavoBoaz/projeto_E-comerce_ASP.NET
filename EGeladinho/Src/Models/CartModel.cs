using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EGeladinho.Src.Models
{
    [Table("tb_carts")]
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("fk_user"), InverseProperty("MyCarts")]
        public User Buyer { get; set; }

        [ForeignKey("fk_product"), InverseProperty("Carts")]
        public Product Product { get; set; }

        public DateTime Date { get; set; }

        public Status StatusPayment { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        PENDING,
        CANCELLED,
        PAID
    }
}