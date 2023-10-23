namespace _6502Emulator.Instructions.OptCodes;

using Architecture;
using DataBus;
using Registers;
using Utils;

public class BRK
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Program created interrupt.
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public BRK(IRegisters registers, ICPUBus cPUBus)
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    
    public byte Execute(ushort address)
    {
        //Advance the PC beyond the break label.
        _registers.PC.Value++;
        
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.I, true);
        _cPUBus.Write((ushort)(MemoryOffsets.StackBaseAddress + _registers.Stkp.Value),
            (byte)((_registers.PC.Value >> 8) & 0x00ff));
        _registers.Stkp.Value--;
        _cPUBus.Write((ushort)(MemoryOffsets.StackBaseAddress + _registers.Stkp.Value),
            (byte)(_registers.PC.Value & 0x00ff));
        _registers.Stkp.Value--;
        
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.B, true);
        _cPUBus.Write((ushort)(MemoryOffsets.StackBaseAddress + _registers.Stkp.Value),
            _registers.Status.Value);
        _registers.Stkp.Value--;
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.B, false);

        _registers.PC.Value = (ushort)(_cPUBus.Read(0xfffe) | (ushort)(_cPUBus.Read(0xffff) << 8));

        return 0;
    }
}