using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekon.Application.DTO.Enums;

namespace Tekon.Application.DTO
{
    public record ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductStatusDto StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
