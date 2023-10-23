namespace _6502Emulator.Instructions.OptCodes;

using Instructoins.OptCodes;
using Registers;
using Utils;

public class CLD : IOptCode
{
private readonly IRegisters _registers;

/// <summary>
/// Clear Decimal Flag.
/// </summary>
/// <param name="registers"></param>
public CLD(IRegisters registers)
{
    this._registers = registers;
}
    
public byte Execute(ushort address)
{
    RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.D, false);

    return 0;
}
}