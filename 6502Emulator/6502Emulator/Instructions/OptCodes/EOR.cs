﻿namespace _6502Emulator.Instructions.OptCodes;

using DataBus;
using Instructoins.OptCodes;
using Registers;
using Utils;

public class EOR : IOptCode
{
    private readonly IRegisters _registers;
    private readonly ICPUBus _cPUBus;

    /// <summary>
    /// Bitwise Logic XOR
    /// </summary>
    /// <param name="registers"></param>
    /// <param name="cPUBus"></param>
    public EOR(IRegisters registers, ICPUBus cPUBus)
    {
        this._registers = registers;
        this._cPUBus = cPUBus;
    }

    public byte Execute(ushort address)
    {
        var data = _cPUBus.Read(address);
        _registers.A.Value = (byte)(_registers.A.Value ^ data);
        
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.Z, (_registers.A.Value & 0x00ff) == 0x0000);
        RegisterUtils.SetRegisterBit(_registers.Status, (byte)StatusRegister.StatusBits.N, (_registers.A.Value  & 0x0080) == 0x0080);
        
        return 0;
    }
}