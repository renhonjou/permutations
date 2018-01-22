using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace permutationsWeb.Entities
{
    public class PermutationsResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        
        [Required]
        [MaxLength(8)]
        public String Request { get; set; }
        [Required]
        public String PermutationsJson { get; set; }
        [Required]
        public Double Seconds { get; set; }
    }
}
