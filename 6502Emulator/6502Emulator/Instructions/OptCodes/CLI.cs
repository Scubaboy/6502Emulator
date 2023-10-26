namespace _6502Emulator.Instructions.OptCodes;

using Registers;
using Utils;

public class CLI
{
    private readonly IRegisters _registers;

    /// <summary>
    /// Clear Interrupt Flag
    /// </summary>
    /// <param name="registers"></param>
    public CLI(IRegisters registers)
    {
        this._registers = registers;
    }

    public byte Execute(ushort address)
    {
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.I, false);

        return 0;
    }
}