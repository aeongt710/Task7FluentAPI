using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task7FluentAPI.Models.VMs
{
    public class ItemUnitVM
    {
        public int ItemId { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> UnitSelectList { get; set; }
        [Required]
        public int UnitId { get; set; }
    }
}
