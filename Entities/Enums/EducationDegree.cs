using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public enum EducationDegree
    {
        [Description("Lisans Derecesi")]
        BachelerDegree,
        [Description("Yüksek Lisans Derecesi")]
        MasterDegree,
        [Description("Doktora Derecesi")]
        PhdDegree   
    }
}
