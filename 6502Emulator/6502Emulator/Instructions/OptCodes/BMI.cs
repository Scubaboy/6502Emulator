namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Registers;
using Utils;

public class BMI
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Branch if minus
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public BMI(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }
    
    public byte Execute(ushort address)
    {
        var cycles = 1;
        
        if (RegisterUtils.GetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N) == (byte)StatusRegister.StatusBits.N)
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