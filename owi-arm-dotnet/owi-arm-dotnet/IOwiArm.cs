using System;
namespace owi_arm_dotnet
{
    interface IOwiArm
    {
        void Connect();

        void Disconnect();

        void MoveArm(ICommandPacket packet);
    }
}
