using System;
namespace owi_arm_dotnet
{
    public interface IOwiCommand
    {
        byte ArmByte { get; }

        byte LedByte { get; }

        byte BaseByte { get; }

        IOwiCommand StopAllMovements();

        IOwiCommand GripperOpen();

        IOwiCommand GripperClose();

        IOwiCommand GripperStop();

        IOwiCommand ElbowUp();

        IOwiCommand ElbowDown();

        IOwiCommand ElbowStop();

        IOwiCommand LedOff();

        IOwiCommand LedOn();
    }
}
