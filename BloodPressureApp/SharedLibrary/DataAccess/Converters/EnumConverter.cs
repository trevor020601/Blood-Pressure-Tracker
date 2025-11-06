using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SharedLibrary.DataAccess.Converters;

// TODO: Maybe move under under an EFCore extensions folder? Could be useful for reuse across projects
internal sealed class EnumConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : Enum
{
    public EnumConverter() : base(
        enumValue => enumValue.ToString(),
        stringValue => (TEnum)Enum.Parse(typeof(TEnum), stringValue)
    )
    { }
}
