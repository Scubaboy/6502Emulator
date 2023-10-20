namespace _6502Emulator.AddressingModes;

using DataBus;
using Models;
using Registers;

public class IND : IAddressingMode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public IND(IRegisters registers, ICPUBus cPUBus)
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }

    /// <summary>
    /// This mode looks up a given address and uses the contents of this address and the next one
    /// (in LLHH little-endian order) as the effective address. This is a pointers!
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        ushort loPtr = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;
        ushort hiPtr = _cPUBus.Read(_registers.PC.Value);
        _registers.PC.Value++;

        var ptr = (ushort)((hiPtr << 8) | loPtr);

        if (loPtr == 0x00FF)
        {
            // Hardware 6502 page bug this was fixed in 65C02 https://news.ycombinator.com/item?id=26913521
            return new Address(
                (ushort)(_cPUBus.Read((ushort)(ptr & 0xFF00)) << 8 | _cPUBus.Read((ushort)(ptr + 0))),
                3);
        }

        return new Address(
            (ushort)(_cPUBus.Read((ushort)(ptr + 1)) << 8 | _cPUBus.Read((ushort)(ptr + 0))),
            3);
    }
}