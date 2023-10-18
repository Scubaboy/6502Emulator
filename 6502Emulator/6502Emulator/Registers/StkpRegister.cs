namespace _6502Emulator.Registers;

internal class StkpRegister : IRegister<byte>
{
    public byte Value { get; set; }
}
