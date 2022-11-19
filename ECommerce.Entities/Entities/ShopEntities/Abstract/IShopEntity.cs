using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entities.Abstract
{
    public interface IShopEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
