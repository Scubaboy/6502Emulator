// See https://aka.ms/new-console-template for more information
using _6502Emulator.DataBus;
using _6502Emulator.ExternalEvents;
using _6502Emulator.Instructoins;
using _6502Emulator.Loader;
using _6502Emulator.Processor;
using _6502Emulator.Procressor;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IInstructionSet, InstructionSet>()
    .AddSingleton<IProcessor, BasicProcessor>()
    .AddSingleton<ICPUMemory, CpuMemoryModule>()
    .AddSingleton<ICPUBus, Bus>()
    .AddSingleton<IExternalEvents, ExternalEvents>()
    .AddSingleton<ILoader, FSLoader>()
    .AddSingleton<ICPUMemory, CpuMemoryModule>();

var sp = serviceProvider.BuildServiceProvider();

var processor = sp.GetService<IProcessor>();
var loader = sp.GetService<ILoader>();

loader.LoadProgram(@"c:\roms\tests.rom");
processor.Reset();

do
{
    processor.Process();

} while (!processor.ExecutionComplete());