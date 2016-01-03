using System;
namespace owi_arm_dotnet
{
    public interface IOwiArm
    {
        void Connect();

        void Disconnect();

        void SendCommand(IOwiCommand command);
    }
}
