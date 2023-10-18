namespace _6502Emulator.Registers;

public class Registers : IRegisters
{

    public Registers() 
    {
        Status = new StatusRegister();
        PC = new ProgramCounter(); 
        A = new AccumulatorRegister();
        X = new XRegister();
        Y = new YRegister();
        Stkp = new StkpRegister();
    }

    public IRegister<byte> Status { get; private set; }
    
    public IRegister<ushort> PC { get; private set; }

    public IRegister<byte> A { get; private set; }

    public IRegister<byte> X {  get; private set; }

    public IRegister<byte> Y { get; private set; }

    public IRegister<byte> Stkp {  get; private set; }
}
