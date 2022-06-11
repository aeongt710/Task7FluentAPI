using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task7FluentAPI.Models
{
    public class Unit
    {
        public int Id { get; set; }
        [Required]
        public string UnitName { get; set; }
        public List<ItemUnit> ItemUnit { get; set; }
    }
}
