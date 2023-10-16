namespace _6502Emulator.Instructoins.OptCodes;

public interface IOptCode
{
    public byte Execute(ushort address);
}
