using System;
namespace owi_arm_dotnet
{
    public interface IOwiCommand
    {
        byte ArmByte { get; }

        byte LedByte { get; }

        byte BaseByte { get; }

        void StopAllMovements();

        void GripperOpen();

        void GripperClose();

        void GripperStop();

        void ElbowUp();

        void ElbowDown();

        void ElbowStop();

        void LedOff();

        void LedOn();
    }
}
