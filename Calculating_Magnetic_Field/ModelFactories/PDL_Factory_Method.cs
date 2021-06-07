using Calculating_Magnetic_Field.Models;
using System;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public class PDL_Factory_Method : IPotencialFactoryMethod
    {
        public IPotencial CreatePotencial(TypeOfPotencial potencialType)
        {
            IPotencial result = null;
            switch (potencialType)
            {
                case TypeOfPotencial.Scalar:
                    {
                        result = new ScalarPDL();
                        break;
                    }
                case TypeOfPotencial.Vector:
                    {
                        result = new VectorPDL();
                        break;
                    }
            }
            return result; 
        }
    }
}
