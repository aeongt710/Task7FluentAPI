using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task7FluentAPI.Models.VMs
{
    public class ItemIndexVM
    {
        public IList<Item> Items { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UnitSelectList { get; set; }
        [Display(Name ="Filter By")]
        public int UnitId { get; set; }
        public string Name { get; set; }
    }
}
