namespace _6502Emulator.Instructions.OptCodes;

using Instructoins.OptCodes;
using Registers;
using Utils;

public class JMP : IOptCode
{
    private readonly IRegisters _registers;

    /// <summary>
    /// Clear Decimal Flag.
    /// </summary>
    /// <param name="registers"></param>
    public JMP(IRegisters registers)
    {
        this._registers = registers;
    }

    public byte Execute(ushort address)
    {
        _registers.PC.Value = address;

        return 0;
    }
}