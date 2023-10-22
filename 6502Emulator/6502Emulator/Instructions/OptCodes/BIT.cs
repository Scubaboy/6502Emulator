namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Registers;
using Utils;

public class BIT
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Bit Test.
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public BIT(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    public byte Execute(ushort address)
    {
        var data = _cPUBus.Read(address);

        var result = (byte)(_registers.A.Value & data);
        
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z, (result & 0x00ff) == 0x00);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N, (result & (1 << 7)) == 0x80);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.V, (result & (1 << 6)) == 0x40);
        return 0;
    }
}