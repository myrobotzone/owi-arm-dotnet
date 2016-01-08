using System;
namespace owi_arm_dotnet
{
    public interface IOwiArm
    {
        bool IsConnected { get; }

        void Connect();

        void Disconnect();

        void SendCommand(IOwiCommand command);
    }
}
