
namespace owi_arm_dotnet
{
    /// <summary>
    /// Interface for creating an owi arm.
    /// </summary>
    public interface IOwiArmFactory
    {
        /// <summary>
        /// Creates an owi arm.
        /// </summary>
        /// <param name="owiUsbConnection">The usb connection to the arm.</param>
        IOwiArm Create(IOwiUsbConnection owiUsbConnection);
    }
}
