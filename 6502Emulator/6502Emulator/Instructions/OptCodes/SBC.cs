namespace _6502Emulator.Instructoins.OptCodes;

using _6502Emulator.DataBus;
using _6502Emulator.Registers;
using _6502Emulator.Utils;

public class SBC : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Subtraction with Borrow In
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public SBC(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    /// <summary>
    /// Deatiled explanation of SBC instruction https://www.righto.com/2012/12/ & https://forums.nesdev.org/viewtopic.php?t=6331
    /// </summary>
    /// <param name="address"> Address of input value.</param>
    /// <returns></returns>
    public byte Execute(ushort address)
    {
        byte data = _cPUBus.Read(address);

        var invertedData = ((ushort)data) ^ 0x00ff;
        ushort result = (ushort)((ushort)(_registers.A.Value) + invertedData + (ushort)RegisterUtils.GetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C));

        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C, result > 255);

        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z, (result & 0x00FF) == 0);

        ushort p1 = (ushort)((ushort)_registers.A.Value ^ result);
        ushort p2 = (ushort)(invertedData ^ result);

        RegisterUtils.SetRegisterBit(_registers.Status,
            (byte)StatusRegister.StatusBits.V,
            ((p1 & p2) & 0x0080) != 0x00);

        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N, (result & 0x80) != 0x00);

        _registers.A.Value = (byte)(result & 0x00FF);

        return 1;
    }
}
