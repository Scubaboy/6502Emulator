namespace _6502Emulator.AddressingModes;

using DataBus;
using Models;
using Registers;

public class REL
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    public REL(IRegisters registers, ICPUBus cPUBus) 
    {
        _registers = registers;
        _cPUBus = cPUBus;
    }
    /// <summary>
    /// This final address mode is exlusive to conditional branch instructions, which branch in the execution path
    /// depending on the state of a given CPU flag. Here, the instruction provides only a relative offset, which is
    /// added to the contents of the program counter (PC) as it points to the immediate next instruction. The relative
    /// offset is a signed single byte value in two's complement encoding (giving a range of −128…+127), which allows
    /// for branching up to half a page forwards and backwards
    /// </summary>
    /// <returns>Address</returns>
    public Address Execute()
    {
        var address = (ushort)(_cPUBus.Read(_registers.PC.Value));
        _registers.PC.Value++;

        if ((address & 0x80) != 0x00)
        {
            address |= 0xff00;
        }
        
        return new Address(address,2);
    }
}