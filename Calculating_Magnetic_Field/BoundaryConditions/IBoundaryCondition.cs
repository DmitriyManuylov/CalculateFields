using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.BoundaryConditions
{
    public interface IBoundaryCondition
    {
        double[,] CalculateMatrixOfCoefficients();
        double[] CalculateFreeMember();
    }
}
