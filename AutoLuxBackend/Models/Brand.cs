using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoLuxBackend.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Cars> Cars { get; set; } = new List<Cars>();
    }
}
