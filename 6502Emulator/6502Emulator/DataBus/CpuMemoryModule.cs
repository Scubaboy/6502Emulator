namespace _6502Emulator.DataBus;

public class CpuMemoryModule : ICPUMemory
{
    public byte[] RAM => new byte[64 * 1024];

}
