using System.ComponentModel;

namespace RGDTest
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum VariantType
    {
        [Description("Вариант 1")]
        Variant1 = 1,
        [Description("Вариант 2")]
        Variant2 = 2,
        [Description("Вариант 3")]
        Variant3 = 3,
        [Description("Вариант N")]
        VariantN = 4,
        [Description("Вариант 5")]
        Variant5 = 5
    }
}
