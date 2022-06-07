using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task7FluentAPI.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string UnitName { get; set; }
        public List<Item> Items { get; set; }
    }
}
