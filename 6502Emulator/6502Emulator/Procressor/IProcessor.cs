namespace _6502Emulator.Processor;

public interface IProcessor
{
	public void Process();
	public void Reset();
	public void IRQ();
	public void NMI();
	public bool ExecutionComplete();
}
