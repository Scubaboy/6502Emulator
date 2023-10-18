
namespace _6502Emulator.DataBus;

public class Bus : ICPUBus
{
    private readonly ICPUMemory cpuMemory;

    public Bus (ICPUMemory cpuMemory)
    {
        this.cpuMemory = cpuMemory;
    }

    public byte Read(ushort addr)
    {
        if (addr >= 0x0000 && addr <= 0x1FFF)
        {
            throw new ApplicationException();
        }

        // System RAM Address Range, mirrored every 2048
        return this.cpuMemory.RAM[addr & 0x07F];
    }

    public void Write(ushort addr, byte data)
    {
        if (addr >= 0x0000 && addr <= 0x1FFF)
        {
            throw new ApplicationException();
        }

        // System RAM Address Range. The range covers 8KB, though
        // there is only 2KB available. That 2KB is "mirrored"
        // through this address range. Using bitwise AND to mask
        // the bottom 11 bits is the same as addr % 2048.
        this.cpuMemory.RAM[addr & 0x07FF] = data;
    }
}

