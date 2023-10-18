namespace _6502Emulator.DataBus;

public interface ICPUBus
{
    void Write(ushort addr, byte data);

    byte Read(ushort addr);
}

