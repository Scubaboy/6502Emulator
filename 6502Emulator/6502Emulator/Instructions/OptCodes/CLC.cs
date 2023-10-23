namespace _6502Emulator.Instructions.OptCodes;

using Instructoins.OptCodes;
using Registers;
using Utils;

public class CLC : IOptCode
{
    private readonly IRegisters _registers;

    /// <summary>
    /// Clear Carry Flag.
    /// </summary>
    /// <param name="registers"></param>
    public CLC(IRegisters registers)
    {
        this._registers = registers;
    }
    
    public byte Execute(ushort address)
    {
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.C, false);

        return 0;
    }
}