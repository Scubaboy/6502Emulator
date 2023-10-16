using _6502Emulator.Clock;
using _6502Emulator.Instructoins;
using _6502Emulator.Instructoins.OptCodes;
using _6502Emulator.Registers;

public class InstructionSet : IInstructionSet
{
    private readonly IRegisters registers;
    private readonly IClock clock;

    public InstructionSet(IRegisters registers, IClock clock) 
    {
        this.registers = registers;
        this.clock = clock;
    }

    public List<InstructionDefinition> Instructions => new List<InstructionDefinition> { new InstructionDefinition("SBC", new SBC(registers, clock).Execute, ) };
}
