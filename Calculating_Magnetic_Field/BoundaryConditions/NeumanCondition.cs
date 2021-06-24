using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculating_Magnetic_Field.BoundaryConditions
{
    public class NeumanCondition : IBoundaryCondition
    {
        public double[] CalculateFreeMember()
        {
            throw new NotImplementedException();
        }

        public double[,] CalculateMatrixOfCoefficients()
        {
            throw new NotImplementedException();
        }
    }
}
