namespace _6502Emulator.AddressingModes;

using _6502Emulator.DataBus;
using _6502Emulator.Models;
using _6502Emulator.Registers;

public class ABX : IAddressingMode
{
    private IRegisters _registers;
    private ICPUBus _cPUBus;

    public ABX(IRegisters registers, ICPUBus cPUBus)
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    /// <summary>
    /// Absolute,X, where the X-register is added to a given base address. 
    /// As the base address is a 16-bit value, these are generally 3-byte
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        var cycles = 3;
        ushort loAddress = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;
        ushort hiAddress = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;

        ushort address = (ushort)(((hiAddress << 8) | loAddress) + _registers.X.Value);

        if ((address & 0xFF00) != hiAddress << 8)
        {
            cycles++;
        }

        return new Address(address, cycles);
    }
}
