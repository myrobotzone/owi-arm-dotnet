namespace owi_arm_dotnet;

/// <inheritdoc />
public class OwiFactory : IOwiFactory
{
    /// <inheritdoc />
    public IOwiArm CreateArm(IOwiUsbConnection owiUsbConnection)
    {
        return new OwiArm(owiUsbConnection);
    }

    /// <inheritdoc />
    public IOwiCommand CreateCommand()
    {
        return new OwiCommand();
    }
}