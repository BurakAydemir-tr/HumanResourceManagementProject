using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Sevices.AuthorizeService
{
    public class Menu
    {
        public string Name { get; set; }
        public List<ControllerAction> Actions { get; set; } = new();
    }
}
