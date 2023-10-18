namespace _6502Emulator.AddressingModes;

using _6502Emulator.Models;
using _6502Emulator.Registers;

/// <summary>
/// Implied Addressing Mode.
/// </summary>
public class IMP : IAddressingMode
{
    private readonly IRegisters registers;

    public IMP (IRegisters registers)
    {
        this.registers = registers;
    }

    public Address Execute()
    {
        return new Address(this.registers.A.Value, 0);
    }
}
