using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.Sources
{
    public interface IPointSource : ISource
    {
        PointD Location { get; set; }

        double SourcePower { get; set; }

        double FieldProperty { get; set; }
    }
}
