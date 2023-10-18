namespace _6502Emulator.Registers;

public class StatusRegister : IRegister<byte>
{
    [Flags]
    public enum StatusBits
    {
        C = 1 << 0, // Carry Bit
        Z = 1 << 1, // Zero 
        I = 1 << 2, // Disable Interupts
        D = 1 << 3, // Decimal Mode
        B = 1 << 4, // Break
        U = 1 << 5, // Unused
        V = 1 << 6, // Overflow
        N = 1 << 7  // Negative
    }

    public byte Value { get; set; }
}

