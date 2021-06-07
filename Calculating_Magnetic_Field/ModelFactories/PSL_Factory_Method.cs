using Calculating_Magnetic_Field.Models;
using System;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public class PSL_Factory_Method : IPotencialFactoryMethod
    {

        public IPotencial CreatePotencial(TypeOfPotencial potencialType)
        {
            IPotencial result = null;
            switch (potencialType)
            {
                case TypeOfPotencial.Scalar:
                    {
                        result = new ScalarPSL();
                        break;
                    }
                case TypeOfPotencial.Vector:
                    {
                        result = new VectorPSL();
                        break;
                    }
            }
            return result;
        }
    }
}
