namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Instructoins.OptCodes;
using Registers;
using Utils;

public class AND : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Bitwise Logic AND
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public AND(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }
    
    public byte Execute(ushort address)
    {
        var data = _cPUBus.Read(address);

        _registers.A.Value &= data;
        RegisterUtils.SetRegisterBit(
            _registers.Status, 
            (byte)StatusRegister.StatusBits.Z, 
            _registers.A.Value == 0x00);
        RegisterUtils.SetRegisterBit(
            _registers.Status, 
            (byte)StatusRegister.StatusBits.N, 
            (_registers.A.Value & 0x80) != 0x00);
        return 1;
    }
}