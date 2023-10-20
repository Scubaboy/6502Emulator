namespace _6502Emulator.Instructions.OptCodes;

using _6502Emulator.DataBus;
using _6502Emulator.Instructoins.OptCodes;
using _6502Emulator.Registers;
using _6502Emulator.Utils;

public class ADC : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Add with Carry In
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public ADC(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    /// <summary>
    /// Deatiled explanation of ADC instruction https://www.righto.com/2012/12/ & https://forums.nesdev.org/viewtopic.php?t=6331
    /// Overflow is set if:
    /// Positive + Positive = Negative
    /// or
    /// Negative + Negative = Positive
    /// Overflow is cleared in all other instances.
    /// </summary>
    /// <param name="address">Address of input value.</param>
    /// <returns></returns>
    public byte Execute(ushort address)
    {
        byte data = _cPUBus.Read(address);

        ushort result = (ushort)((ushort)(_registers.A.Value) + (ushort)data + (ushort)RegisterUtils.GetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C));

        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C, result > 255);

        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z, (result & 0x00FF) == 0);

        ushort p1 = (ushort)~((ushort)_registers.A.Value ^ (ushort)data);
        ushort p2 = (ushort)((ushort)_registers.A.Value ^ (ushort)result);

        RegisterUtils.SetRegisterBit(_registers.Status, 
            (byte)StatusRegister.StatusBits.V, 
            ((p1 & p2) & 0x0080) != 0x00);

        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N, (result & 0x80) != 0x00);

        _registers.A.Value = (byte)(result & 0x00FF);

        return 1;
    }
}
