namespace _6502Emulator.Instructions.OptCodes;

using Registers;
using Utils;

public class CLV
{
    private readonly IRegisters _registers;

    /// <summary>
    /// Clear Overflow Flag
    /// </summary>
    /// <param name="registers"></param>
    public CLV(IRegisters registers)
    {
        this._registers = registers;
    }

    public byte Execute(ushort address)
    {
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.V, false);

        return 0;
    }
}