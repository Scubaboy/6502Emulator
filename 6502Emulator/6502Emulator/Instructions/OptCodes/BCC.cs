namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Instructoins.OptCodes;
using Registers;
using Utils;

public class BCC : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Branch if carry clear
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public BCC(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }
    
    public byte Execute(ushort address)
    {
        var cycles = 1;
        
        if (RegisterUtils.GetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C) != (byte)StatusRegister.StatusBits.C)
        {
            var absAdrs = (ushort)(_registers.PC.Value + address);
            
            if ((address & 0xff00) != (_registers.PC.Value & 0xff00))
            {
                cycles++;
            }
                
            _registers.PC.Value = absAdrs;
        }

        return (byte)cycles;
    }
}