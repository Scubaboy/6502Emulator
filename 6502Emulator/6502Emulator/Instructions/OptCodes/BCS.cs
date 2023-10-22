namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Registers;
using Utils;

public class BCS
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Branch if carry set
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public BCS(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }
    
    public byte Execute(ushort address)
    {
        var cycles = 1;
        
        if (RegisterUtils.GetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C) == (byte)StatusRegister.StatusBits.C)
        {
            var absAdrs = (ushort)(_registers.PC.Value + address);
            
            if ((absAdrs & 0xff00) != (_registers.PC.Value & 0xff00))
            {
                cycles++;
            }
                
            _registers.PC.Value = absAdrs;
        }

        return (byte)cycles;
    }
}