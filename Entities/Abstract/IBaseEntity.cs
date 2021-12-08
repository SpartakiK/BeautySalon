using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Abstract
{
    interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreationDate { get; set; }
    }
}
