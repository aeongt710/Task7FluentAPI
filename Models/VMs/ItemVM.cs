using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Task7FluentAPI.Models.VMs
{
    public class ItemVM
    {
        public Item Item { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UnitSelectList { get; set; }
    }
}
