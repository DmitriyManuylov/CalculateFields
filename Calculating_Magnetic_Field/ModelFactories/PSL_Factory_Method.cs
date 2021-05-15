using Calculating_Magnetic_Field.Models;
using System;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public class PSL_Factory_Method : IPotencialFactoryMethod
    {

        public IPotencial CreatePotencial(PotencialTypes potencialType)
        {
            IPotencial result = null;
            switch (potencialType)
            {
                case PotencialTypes.Scalar:
                    {
                        result = new ScalarPSL();
                        break;
                    }
                case PotencialTypes.Vector:
                    {
                        result = new VectorPSL();
                        break;
                    }
            }
            return result;
        }
    }
}
