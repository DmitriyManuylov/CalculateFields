using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources
{
    public interface IResidualIntensitySource : ISource
    {
        int N { get; }
        double ResidualIntensity { get; set; }

        SimpleDirections Direction { get; set; }
    }
}
