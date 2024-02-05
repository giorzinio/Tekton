﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekon.Application.DTO
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
