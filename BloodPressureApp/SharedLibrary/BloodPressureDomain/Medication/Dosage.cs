namespace SharedLibrary.BloodPressureDomain.Medication;

public enum Unit
{
    Mg, // Milligrams
    G, // Grams
    Ml, // Milliliters
    Units, // e.g., Insulin units
    Mcg // Micrograms
    // Other units?
}

public record Dosage
{
    private Dosage(double value, Unit unit)
    {
        Value = value;
        Unit = unit;
    }

    public double Value { get; init; }

    public Unit Unit { get; init; }

    public static Dosage? Create(double value, Unit unit)
    {
        // What is a reasonable dosage for validation purposes?
        if (value <= 0)
        {
            return null;
        }

        return new Dosage(value, unit);
    }

    public override string ToString() => $"{Value} {Unit.ToString().ToLower()}";
}
