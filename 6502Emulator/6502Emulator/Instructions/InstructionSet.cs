namespace _6502Emulator.Instructoins;

using _6502Emulator.AddressingModes;
using _6502Emulator.DataBus;
using _6502Emulator.Instructoins.OptCodes;
using _6502Emulator.Registers;

public class InstructionSet : IInstructionSet
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public InstructionSet(IRegisters registers, ICPUBus cPUBus) 
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }

    public List<InstructionDefinition> Instructions => new List<InstructionDefinition> { new InstructionDefinition("SBC", new SBC(_registers, _cPUBus).Execute, new IMP(_registers).Execute ) };
}
