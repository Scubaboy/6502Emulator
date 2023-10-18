namespace _6502Emulator.Registers;

public class ProgramCounter : IRegister<ushort>
{
    public ushort Value {get;set;}
}
