namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Registers;
using Utils;

public class BEQ
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Branch if equal (if zero flag is set)
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public BEQ(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }
    
    public byte Execute(ushort address)
    {
        var cycles = 1;
        
        if (RegisterUtils.GetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z) == (byte)StatusRegister.StatusBits.Z)
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