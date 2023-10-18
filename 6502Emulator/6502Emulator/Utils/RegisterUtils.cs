using _6502Emulator.Registers;

namespace _6502Emulator.Utils;

static public class RegisterUtils
{
    static public void SetRegisterBit(IRegister<byte> register,byte bit, bool value) 
    {
        if (value)
        {
            register.Value |= bit;
        }
        else
        {
            register.Value &= (byte)~bit;
        }
    }

    static public byte GetRegisterBit(IRegister<byte> register, byte bit)
    {
        return (byte)(register.Value & bit);
    }
}

