using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Entities.ShopEntities.Concrete.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool NewOrNot { get; set; }

        public string Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual Genders Gender { get; set; }
        public bool Unisex { get; set; }
        public Decimal Price { get; set; }
        public int Stock { get; set; }
        public bool InStock { get; set; }

        public virtual BodyParts BodyPart { get; set; }
    }
}
