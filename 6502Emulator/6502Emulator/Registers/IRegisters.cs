namespace _6502Emulator.Registers;

public interface IRegisters
{
    IRegister<byte> Status{ get; }

    IRegister<ushort> PC { get; }

    IRegister<byte> A { get; }

    IRegister<byte> X { get; }

    IRegister<byte> Y { get; }

    IRegister<byte> Stkp { get; }
}
