using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum Gender
    {
        [Description("Erkek")]
        Male,
        [Description("Kadın")]
        Female,
        [Description("Diğer")]
        Other
    }
}
