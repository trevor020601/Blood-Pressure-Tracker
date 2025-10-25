using SharedLibrary.Primitives;

namespace SharedLibrary.BloodPressureDomain.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 320;

    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Email Create(string email)
    {
        // TODO: Add more constraints
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentNullException("Email is empty.");
        }

        if (email.Length > MaxLength)
        {
            throw new ArgumentException("Email is too long.");
        }

        return new Email(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
