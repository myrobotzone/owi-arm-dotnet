﻿using System;
namespace owi_arm_dotnet
{
    public interface ICommand
    {
        byte ArmByte { get; }

        byte LedByte { get; }

        byte BaseByte { get; }

        void ElbowDown();

        void ElbowStop();

        void ElbowUp();

        void LedOff();

        void LedOn();
    }
}
