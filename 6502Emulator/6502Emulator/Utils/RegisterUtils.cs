using _6502Emulator.Registers;

namespace _6502Emulator.Utils;

static public class RegisterUtils
{
    static public void DisableRegisterBit(IRegister<byte> register, byte toggleBit)
    {
        register.Value &= (byte)~toggleBit;
    }

    static public void SetRegisterBit(IRegister<byte> register, byte value) 
    {
        register.Value |= value;
    }

    static public byte GetRegisterBit(IRegister<byte> register, byte bit)
    {
        return (byte)(register.Value & bit);
    }
}

