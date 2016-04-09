using System;
using System.Threading.Tasks;
namespace owi_arm_dotnet
{
    /// <summary>
    /// Interface for connecting and sending commands to the owi robotic arm.
    /// </summary>
    public interface IOwiArm
    {
        /// <summary>
        /// Gets a value indicating whether the arm is connected or not.
        /// </summary>
        /// <returns>True if the arm is connected, otherwise false.</returns>
        bool IsConnected { get; }

        /// <summary>
        /// Connects to the arm.
        /// </summary>
        Task ConnectAsync();

        /// <summary>
        /// Disconnects form the arm.
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// Sends a command to the arm. ConnectAsync() should be called
        /// before invoking this method.
        /// </summary>
        Task SendCommandAsync(IOwiCommand command);
    }
}
