
namespace _6502Emulator.Registers;

public interface IRegister<T>
{
    T Value { get; set; }
}

