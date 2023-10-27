namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Instructoins.OptCodes;
using Registers;
using Utils;

public class INC : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Increment Value at Memory Location
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public INC(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    public byte Execute(ushort address)
    {
        var result = _cPUBus.Read(address) + 1;
        
        _cPUBus.Write(address, (byte)(result & 0x00ff));
        
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z, (result & 0x00ff) == 0x0000);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N, (result & 0x0080) == 0x0080);
        
        return 0;
    }
}