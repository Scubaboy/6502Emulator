namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Instructoins.OptCodes;
using Registers;
using Utils;

public class INY : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Increment Y Register
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public INY(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    public byte Execute(ushort address)
    {
        _registers.Y.Value++;

        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z, _registers.Y.Value == 0x0000);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N, (_registers.Y.Value & 0x0080) == 0x0080);
        
        return 0;
    }
}