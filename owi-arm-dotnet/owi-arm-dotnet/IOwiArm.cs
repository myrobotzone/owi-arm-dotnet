using System;
using System.Threading.Tasks;
namespace owi_arm_dotnet
{
    public interface IOwiArm
    {
        bool IsConnected { get; }

        Task ConnectAsync();

        Task DisconnectAsync();

        Task SendCommandAsync(IOwiCommand command);
    }
}
