namespace _6502Emulator.Registers;

public class Registers : IRegisters
{
    public IRegister<byte> Status => new StatusRegister();

    public IRegister<ushort> PC => new ProgramCounter();

    public IRegister<byte> A => new AccumulatorRegister();

    public IRegister<byte> X => new XRegister();

    public IRegister<byte> Y => new YRegister();

    public IRegister<byte> Stkp => new StkpRegister();
}
