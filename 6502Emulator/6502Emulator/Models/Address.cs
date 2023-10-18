namespace _6502Emulator.Models;

public record Address
{
    public ushort Value { get; init; }

    public int Cycles { get; init; }

    public Address(ushort value, int cycles) => (Value, Cycles) = (value, cycles);
}
