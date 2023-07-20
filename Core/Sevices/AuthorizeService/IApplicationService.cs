using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Sevices.AuthorizeService
{
    public interface IApplicationService
    {
        List<Menu> GetAuthorizeDefinitionEndPoints(Type type);
    }
}
