namespace _6502Emulator.AddressingModes;

using _6502Emulator.Models;
using _6502Emulator.Registers;

/// <summary>
/// Immediate Address - literal operand is given immediately after the instruction. 
/// The operand is always an 8-bit value and the total instruction length 
/// is always 2 bytes.
/// </summary>
public class IMM : IAddressingMode
{
    private readonly IRegisters _registers;

    public IMM(IRegisters registers)
    {
        _registers = registers;
    }

    public Address Execute() => new(_registers.PC.Value++, 0);
}
