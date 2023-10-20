namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Instructoins.OptCodes;
using Registers;
using Utils;

/// <summary>
/// Arithmetic Shift Left
/// </summary>
public class ASL : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;
    private readonly bool _isImpliedAddress;

    public ASL(IRegisters registers, ICPUBus cPUBus, bool isImpliedAddress)
    {
        _registers = registers;
        _cPUBus = cPUBus;
        _isImpliedAddress = isImpliedAddress;
    }
    
    public byte Execute(ushort address)
    {
        var data = (ushort)_cPUBus.Read(address);

        data <<= 1;
        RegisterUtils.SetRegisterBit(
            _registers.Status,
            (byte)StatusRegister.StatusBits.C,
            (data & 0xff00) > 0 );
        RegisterUtils.SetRegisterBit(
            _registers.Status,
            (byte)StatusRegister.StatusBits.Z,
            (data & 0x00ff) == 0x00);
        RegisterUtils.SetRegisterBit(
            _registers.Status,
            (byte)StatusRegister.StatusBits.N,
            (data & 0x80) != 0x00);

        if (_isImpliedAddress)
        {
            _registers.A.Value = (byte)(data & 0x00ff);
        }
        else
        {
            _cPUBus.Write(address, (byte)(data & 0x00ff));
        }
        
        return 0;
    }
}