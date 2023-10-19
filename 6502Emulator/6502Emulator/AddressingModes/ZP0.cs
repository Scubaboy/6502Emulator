namespace _6502Emulator.AddressingModes;

using _6502Emulator.DataBus;
using _6502Emulator.Models;
using _6502Emulator.Registers;

public class ZP0 : IAddressingMode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public ZP0(IRegisters registers, ICPUBus cPUBus) 
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    /// <summary>
    /// The zero-page address mode is similar to absolute address mode, but these 
    /// instructions use only a single byte for the operand, the low-byte, while 
    /// the high-byte is assumed to be zero by definition.
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        ushort address = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;

        return new Address((ushort)(address & 0x00FF),1);
    }
}
