using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Keyless]
    public partial class Territory
    {
        [Column(TypeName = "nvarchar")]
        public string TerritoryId { get; set; } = null!;
        [Column(TypeName = "nchar")]
        [Required]
        public string TerritoryDescription { get; set; } = null!;
        [Column(TypeName = "int")]
        [Required]
        public int RegionId { get; set; }
    }
}
