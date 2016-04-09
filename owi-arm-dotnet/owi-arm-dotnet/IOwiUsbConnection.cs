using System.Threading.Tasks;

namespace owi_arm_dotnet
{
    /// <summary>
    /// Interface for a Usb connection to the owi robotic arm.
    /// </summary>
    public interface IOwiUsbConnection
    {
        /// <summary>
        /// Gets a value indicating whether the usb connection is open.
        /// </summary>
        /// <returns>True if the connection is open, otherwise false.</returns>
        bool IsOpen { get; }

        /// <summary>
        /// Opens the usb connection.
        /// </summary>
        Task OpenAsync();

        /// <summary>
        /// Sends bytes to the arm.
        /// </summary>
        Task SendAsync(byte byte1, byte byte2, byte byte3);

        /// <summary>
        /// Closes the usb connection.
        /// </summary>
        Task CloseAsync();
    }
}
