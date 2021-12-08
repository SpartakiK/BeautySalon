using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Mappers.Abstract
{
    public interface IMapper<Entity, Model>
    {
        Entity ToEntity(Model values);
        Model ToModel(Entity values);
        IEnumerable<Entity> ToEntityList(IEnumerable<Model> modellist);
        IEnumerable<Model> ToModelList(IEnumerable<Entity> entitylist);
    }
}
