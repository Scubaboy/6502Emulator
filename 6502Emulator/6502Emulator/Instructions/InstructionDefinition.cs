using _6502Emulator.Models;

namespace _6502Emulator.Instructoins;

public record InstructionDefinition
{
    public string Name { get; init; }
    public Func<ushort,byte> Execute { get; init; }
    public Func<Address> AddressMode { get; init; }

    public InstructionDefinition(string name, Func<ushort, byte> execute, Func<Address> addressMode) => (Name, Execute, AddressMode) = (name, execute, addressMode);
}
