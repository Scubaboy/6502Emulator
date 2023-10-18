
namespace _6502Emulator.Procressor;

using _6502Emulator.DataBus;
using _6502Emulator.ExternalEvents;
using _6502Emulator.Instructoins;
using _6502Emulator.Processor;
using _6502Emulator.Registers;
using _6502Emulator.Utils;

public class BasicProcessor : IProcessor
{
    private readonly ICPUBus _cPUBus;
    private readonly IRegisters _registers;
    private readonly IInstructionSet _instructionSet;
    private readonly IExternalEvents _externalEvents;

    private int Cycles { get;set; }

    public BasicProcessor(ICPUBus cPUBus, IRegisters registers, IInstructionSet instructionSet, IExternalEvents externalEvents) 
    {
        _cPUBus = cPUBus;
        _registers = registers;
        _instructionSet = instructionSet;
        _externalEvents = externalEvents;
        Cycles = 0;
    }
    
    public void Process()
    {
        if (Cycles == 0)
        {
            //Completed current instruction can now move onto the next instruction.
            var instruction = _cPUBus.Read(_registers.PC.Value);
            
            RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.U);

            _registers.PC.Value++;

            //Setup the address
            var addr = _instructionSet.Instructions[instruction].AddressMode();

            Cycles += addr.Cycles;

            //Execute the instruction
            Cycles += _instructionSet.Instructions[instruction].Execute(addr.Value);

            RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.U);
        }

        Cycles--;
    }

    public bool ExecutionComplete()
    {
        return Cycles == 0;
    }

    public void Reset()
    {
        _externalEvents.Reset();
        Cycles = 6;
    }

    public void IRQ()
    {
        //Complete the last request before executing the IRQ
        while (Cycles > 0) 
        {
            Process();
        }
        _externalEvents.Irq();
        Cycles = 7;
    }
}
