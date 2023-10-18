namespace _6502Emulator.Instructoins.OptCodes;

using _6502Emulator.DataBus;
using _6502Emulator.Registers;

public class SBC : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public SBC (IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    public byte Execute(ushort address)
    {
        var data = this._cPUBus.Read (address);

        return 1;
    }
}
