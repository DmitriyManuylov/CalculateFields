using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources
{
    public interface IVolumeSource: ISource
    {
        int N { get; }

        int M { get; }

        double SourcePower { get; }
    }
}
