namespace _6502Emulator.AddressingModes;

using DataBus;
using Models;
using Registers;

public class ZPX : IAddressingMode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public ZPX(IRegisters registers, ICPUBus cPUBus) 
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    /// <summary>
    /// zero-page mode for indexed addressing. However, this is generally only available with the X-register.
    /// (The only exception to this is LDX, which has an indexed zero-page mode utilizing the Y-register.)
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        var address = (ushort)(_cPUBus.Read(_registers.PC.Value) + _registers.X.Value);
        _registers.PC.Value++;

        return new Address((ushort)(address & 0x00FF),2);
    }
}