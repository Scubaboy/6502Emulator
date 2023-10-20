namespace _6502Emulator.AddressingModes;

using DataBus;
using Models;
using Registers;

public class IZX : IAddressingMode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public IZX(IRegisters registers, ICPUBus cPUBus) 
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    /// <summary>
    /// Pre-indexed indirect address mode is only available in combination with the X-register. It works much like the
    /// "zero-page,X" mode, but, after the X-register has been added to the base address, instead of directly accessing
    /// this, an additional lookup is performed, reading the contents of resulting address and the next one
    /// (in LLHH little-endian order), in order to determine the effective address
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        ushort baseAdr = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;

        var lo = _cPUBus.Read((ushort)((baseAdr + _registers.X.Value) & 0x00FF));
        var hi = _cPUBus.Read((ushort)((baseAdr + _registers.X.Value + 1) & 0x00FF));
        
        return new Address((ushort)((hi << 8 ) | lo),2);
    }
}