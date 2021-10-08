using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using View;
namespace Mappers
{
    public interface IMapper<ViewInstance, ModelInstance>
    {
        ViewInstance FromModelToView(ModelInstance model);

        ModelInstance FromViewToModel(ViewInstance view);
    }
}
