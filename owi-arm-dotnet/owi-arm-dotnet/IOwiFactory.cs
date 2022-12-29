namespace owi_arm_dotnet;

/// <summary>
/// Interface for creating an owi arm and owi commands.
/// </summary>
public interface IOwiFactory
{
    /// <summary>
    /// Creates an owi arm.
    /// </summary>
    /// <param name="owiUsbConnection">The usb connection to the arm.</param>
    IOwiArm CreateArm(IOwiUsbConnection owiUsbConnection);

    /// <summary>
    /// Creates a new owi command. Typically you can reuse the command.
    /// </summary>
    /// <returns>A new command initialized with all robot features turned off.</returns>
    IOwiCommand CreateCommand();
}