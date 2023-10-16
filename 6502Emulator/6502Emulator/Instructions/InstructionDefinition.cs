namespace _6502Emulator.Instructoins;

public record InstructionDefinition
{
    public string Name { get; init; }
    public Func<ushort,byte> Execute { get; init; }
    public Func<byte> AddressMode { get; init; }

    public InstructionDefinition(string name, Func<ushort, byte> execute, Func< byte> addressMode) => (Name, Execute, AddressMode) = (name, execute, addressMode);
}
