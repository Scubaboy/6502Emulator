namespace _6502Emulator.DataBus;

public interface ICPUMemory
{
    // 2KB of RAM
    public byte[] RAM { get; }
}

