﻿using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models
{
    public class Model
    {
        [Key]
        public int ProductModelId { get; set; }

        public string Name { get; set; } = null!;

    }
}
