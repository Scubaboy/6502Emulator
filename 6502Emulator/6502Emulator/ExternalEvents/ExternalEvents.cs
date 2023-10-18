namespace _6502Emulator.ExternalEvents;

using _6502Emulator.DataBus;
using _6502Emulator.Registers;
using _6502Emulator.Utils;

public class ExternalEvents : IExternalEvents
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public ExternalEvents(IRegisters registers, ICPUBus cPUBus)
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }

    /// <summary>
    /// Interrupt process request that can happen at any time. The interrupt will execute if the 
    /// disable interrupt flag is 0 in the status register. Once the irq has executed an address is
    /// read from memory location 0xFFFE which is assigned to the program counter.
    /// </summary>
    public void Irq()
    {
        if (RegisterUtils.GetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.I) == 0)
        {
            //Push the program counter onto the stack high byte first then low byte.
            _cPUBus.Write((ushort)(0x0100 + _registers.Stkp.Value), (byte)((_registers.PC.Value >> 8) & 0x00FF));
            _registers.Stkp.Value--;
            _cPUBus.Write((ushort)(0x0100 + _registers.Stkp.Value), (byte)((_registers.PC.Value) & 0x00FF));
            _registers.Stkp.Value--;

            //Push the status register onto the stack.
            RegisterUtils.DisableRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.B);
            RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.U);
            RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.I);
            _cPUBus.Write((ushort)(0x0100 + _registers.Stkp.Value), _registers.Status.Value);
            _registers.Stkp.Value--;

            //Read new program counter location from fixed address
            ushort address = 0xFFFE;
            ushort lowByte = _cPUBus.Read((ushort)(address + 0));
            ushort highByte = _cPUBus.Read((ushort)(address + 1));
            _registers.PC.Value = (ushort)((highByte << 8) | lowByte);

            //Set cycles
            _clock.Cycles = 7;
        }
    }

    /// <summary>
    /// Non-Maskable interrupt cannot be ingnored. It executes in exactly the same way as a standard 
    /// IRQ. The new program counter address is read from memory address 0xFFFA.
    /// </summary>
    public void nmi()
    {

        //Push the program counter onto the stack high byte first then low byte.
        _cPUBus.Write((ushort)(0x0100 + _registers.Stkp.Value), (byte)((_registers.PC.Value >> 8) & 0x00FF));
        _registers.Stkp.Value--;
        _cPUBus.Write((ushort)(0x0100 + _registers.Stkp.Value), (byte)((_registers.PC.Value) & 0x00FF));
        _registers.Stkp.Value--;

        //Push the status register onto the stack.
        RegisterUtils.DisableRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.B);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.U);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.I);
        _cPUBus.Write((ushort)(0x0100 + _registers.Stkp.Value), _registers.Status.Value);
        _registers.Stkp.Value--;

        //Read new program counter location from fixed address
        ushort address = 0xFFFA;
        ushort lowByte = _cPUBus.Read((ushort)(address + 0));
        ushort highByte = _cPUBus.Read((ushort)(address + 1));
        _registers.PC.Value = (ushort)((highByte << 8) | lowByte);

        //Set cycles
        _clock.Cycles = 8;
    }

    /// <summary>
    /// Sets the 6502 emulation into a known state. This method mirrors the actual opreation of the 6502.
    /// </summary>
    public void Reset()
    {
        ushort address = 0xFFFC;
        ushort low = _cPUBus.Read((ushort)(address + 0));
        ushort high = _cPUBus.Read((ushort)(address + 1));

        //Set Program Counter
        _registers.PC.Value = (ushort)((high << 8) | low);

        //Reset registers
        _registers.A.Value = 0;
        _registers.X.Value = 0;
        _registers.Y.Value = 0;
        _registers.Stkp.Value = 0xFD;
        _registers.Status.Value = 0x00 | (byte)StatusRegister.StatusBits.U;

        //Set cycles
        _clock.Cycles = 8;
    }


}

