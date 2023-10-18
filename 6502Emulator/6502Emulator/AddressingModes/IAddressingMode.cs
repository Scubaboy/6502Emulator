namespace _6502Emulator.AddressingModes;

using _6502Emulator.Models;

public interface IAddressingMode
{
    public Address Execute();
}

