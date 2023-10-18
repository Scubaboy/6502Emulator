using _6502Emulator.DataBus;

namespace _6502Emulator.Loader;

public class FSLoader : ILoader
{
    private readonly ICPUMemory _cPUMemory;

    public FSLoader(ICPUMemory cPUMemory)
    {
        _cPUMemory = cPUMemory;
    }

    public void LoadProgram(string path)
    {
        using (var fs = File.OpenRead(path))    
        {
            var addr = (ushort)0x8000;

            while (addr < 0xFFFC)
            {
                if (fs.Read(_cPUMemory.RAM,addr,1) == 0)
                {
                    break;
                }
            }

            //Set Reset Vector
            _cPUMemory.RAM[0xFFFC] = 0x00;
            _cPUMemory.RAM[0xFFFD] = 0x80;
        }
    }
}
