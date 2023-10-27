namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Registers;
using Utils;

public class CMP
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Compare Accumulator
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public CMP(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    public byte Execute(ushort address)
    {
        var data = _cPUBus.Read(address);

        var result = (ushort)_registers.A.Value - (ushort)data;
        
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C, _registers.A.Value >= data);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z, (result & 0x00ff) == 0x0000);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N, (result & 0x0080) == 0x0080);
        
        return 0;
    }
}