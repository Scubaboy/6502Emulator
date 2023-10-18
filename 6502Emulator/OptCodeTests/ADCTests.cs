namespace OptCodeTests;

using _6502Emulator.DataBus;
using _6502Emulator.Instructions.OptCodes;
using _6502Emulator.Registers;
using NSubstitute;

public class ADCTests
{
    [Fact]
    public void ADC_SetsTheStatusRegister_NFlag_True_IfResutMSB_Set()
    {
        //Arrange
        var registers = new Registers();
        var cpuBus = Substitute.For<ICPUBus>();

        cpuBus.Read(Arg.Any<ushort>()).ReturnsForAnyArgs<byte>((byte)0x80);
        registers.A.Value = 0;
        registers.Status.Value = 0;

        var adc = new ADC(registers, cpuBus);
        
        //Act
        adc.Execute(0x0000);

        //Assert
        Assert.True((registers.Status.Value & (byte)StatusRegister.StatusBits.N) != 0x00);
    }

    [Fact]
    public void ADC_SetsTheStatusRegister_ZFlag_True_IfResutIsZero()
    {
        //Arrange
        var registers = new Registers();
        var cpuBus = Substitute.For<ICPUBus>();

        cpuBus.Read(Arg.Any<ushort>()).ReturnsForAnyArgs<byte>((byte)0x00);
        registers.A.Value = 0;
        registers.Status.Value = 0;

        var adc = new ADC(registers, cpuBus);

        //Act
        adc.Execute(0x0000);

        //Assert
        Assert.True((registers.Status.Value & (byte)StatusRegister.StatusBits.Z) != 0x00);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void ADC_SetsTheStatusRegister_CFlag_True_IfHighByteBit0Set()
    {
        //Arrange
        var registers = new Registers();
        var cpuBus = Substitute.For<ICPUBus>();

        cpuBus.Read(Arg.Any<ushort>()).ReturnsForAnyArgs<byte>((byte)0xff);
        registers.A.Value = 1;
        registers.Status.Value = 0;

        var adc = new ADC(registers, cpuBus);

        //Act
        adc.Execute(0x0000);

        //Assert
        Assert.True((registers.Status.Value & (byte)StatusRegister.StatusBits.C) != 0x00);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void ADC_SetsTheStatusRegister_VFlag_True_IfInputsDontHaveDifferentSign_AndInputSignDifferentToOutputSign()
    {
        //Arrange
        var registers = new Registers();
        var cpuBus = Substitute.For<ICPUBus>();

        cpuBus.Read(Arg.Any<ushort>()).ReturnsForAnyArgs<byte>((byte)0x7f);
        registers.A.Value = 1;
        registers.Status.Value = 0;

        var adc = new ADC(registers, cpuBus);

        //Act
        adc.Execute(0x0000);

        //Assert
        Assert.True((registers.Status.Value & (byte)StatusRegister.StatusBits.V) == 0x40);
    }

    /// <summary>
    /// 
    /// </summary>
    [Fact]
    public void ADC_SetsTheStatusRegister_VFlag_False_IfInputsHaveDifferentSign_AndInputSignDifferentToOutputSign()
    {
        //Arrange
        var registers = new Registers();
        var cpuBus = Substitute.For<ICPUBus>();

        cpuBus.Read(Arg.Any<ushort>()).ReturnsForAnyArgs<byte>((byte)0xff);
        registers.A.Value = 1;
        registers.Status.Value = 0;

        var adc = new ADC(registers, cpuBus);

        //Act
        adc.Execute(0x0000);

        //Assert
        Assert.True((registers.Status.Value & (byte)StatusRegister.StatusBits.V) == 0x00);
    }

}