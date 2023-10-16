namespace _6502Emulator.Instructoins.OptCodes;

using _6502Emulator.Clock;
using _6502Emulator.Registers;

public class SBC : IOptCode
{
    private readonly IRegisters _registers;
    private readonly IClock _clock;

    public SBC (IRegisters registers, IClock clock)
    {
        this._registers = registers;
        this._clock = clock;
    }

    public byte Execute(ushort address)
    {
        throw new NotImplementedException();
    }
}
