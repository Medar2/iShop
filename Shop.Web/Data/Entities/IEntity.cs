using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Data.Entities
{
    public class IEntity
    {
        int Id { get; set; }
        public bool WasDelete { get; set; }
    }
}
