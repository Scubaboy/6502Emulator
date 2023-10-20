namespace _6502Emulator.AddressingModes;

using DataBus;
using Models;
using Registers;

public class ZPY : IAddressingMode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public ZPY(IRegisters registers, ICPUBus cPUBus) 
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    /// <summary>
    /// zero-page mode for indexed addressing. However, this is generally only available with the Y-register.
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        ushort address = (ushort)(_cPUBus.Read(_registers.PC.Value) + _registers.Y.Value);
        _registers.PC.Value++;

        return new Address((ushort)(address & 0x00FF),2);
    }
}