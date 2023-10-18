namespace _6502Emulator.ExternalEvents;

public interface IExternalEvents
{
    void Reset();

    void Irq();

    void nmi();
}

