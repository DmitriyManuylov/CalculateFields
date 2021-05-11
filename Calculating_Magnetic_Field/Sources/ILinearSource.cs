using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources
{
    public interface ILinearSource: ISource
    {
        Bound_Rib Rib { get; set; }

        double Density { get; set; }

        int N { get; set; }
    }
}
