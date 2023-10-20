namespace _6502Emulator.AddressingModes;

using DataBus;
using Models;
using Registers;

public class IZY
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public IZY(IRegisters registers, ICPUBus cPUBus) 
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    /// <summary>
    /// Post-indexed indirect addressing is only available in combination with the Y-register. As indicated by the
    /// indexing term ",Y" being appended to the outside of the parenthesis indicating the indirect lookup, here,
    /// a pointer is first read (from the given zero-page address) and resolved and only then the contents of the
    /// Y-register is added to this to give the effective address.
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        ushort baseAdr = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;

        var lo = (ushort)_cPUBus.Read((ushort)(baseAdr & 0x00FF));
        var hi = (ushort)_cPUBus.Read((ushort)((baseAdr + 1) & 0x00FF));
        var adr = (ushort)(((hi << 8) | lo) + _registers.Y.Value);
        var cycles = 2;
        
        if ((adr & 0xFF00) != (ushort)(hi << 8))
        {
            cycles = 3;
        }
        
        return new Address(adr,cycles);
    }
}