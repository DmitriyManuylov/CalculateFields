using Calculating_Magnetic_Field.Models;
using System;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public class PSL_Factory_Method : IPotencialFactoryMethod
    {

        public IPotencial CreatePotencial(DimensionsOfPotencial potencialType)
        {
            IPotencial result = null;
            switch (potencialType)
            {
                case DimensionsOfPotencial.Scalar:
                    {
                        result = new ScalarPSL();
                        break;
                    }
                case DimensionsOfPotencial.Vector:
                    {
                        result = new VectorPSL();
                        break;
                    }
            }
            return result;
        }
    }
}
