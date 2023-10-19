namespace _6502Emulator.AddressingModes;

using _6502Emulator.DataBus;
using _6502Emulator.Models;
using _6502Emulator.Registers;

public class ABS : IAddressingMode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public ABS(IRegisters registers, ICPUBus cPUBus)
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }

    /// <summary>
    /// Absolute addressing provides the 16-bit address of a memory location, the 
    /// contents of which is used as the operand to the instruction. The address is provided
    /// in two bytes immediately after the instruction (making these 3-byte instructions) in low-byte, 
    /// high-byte order (LLHH) or little-endian
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        ushort loAddress = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;
        ushort hiAddress = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;

        return new Address((ushort)((hiAddress << 8) | loAddress), 2);
    }
}
