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

        IOwiCommand WristUp();

        IOwiCommand WristDown();

        IOwiCommand WristStop();

        IOwiCommand ElbowUp();

        IOwiCommand ElbowDown();

        IOwiCommand ElbowStop();

        IOwiCommand ShoulderUp();

        IOwiCommand ShoulderDown();

        IOwiCommand ShoulderStop();

        IOwiCommand BaseRotateClockwise();

        IOwiCommand BaseRotateCounterClockwise();

        IOwiCommand BaseRotateStop();

        IOwiCommand LedOff();

        IOwiCommand LedOn();
    }
}
