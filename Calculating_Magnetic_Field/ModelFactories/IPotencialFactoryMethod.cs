using Calculating_Magnetic_Field.Models;

namespace Calculating_Magnetic_Field.ModelFactories
{
    public interface IPotencialFactoryMethod
    {
        IPotencial CreatePotencial(DimensionsOfPotencial potencialType);
    }
}
