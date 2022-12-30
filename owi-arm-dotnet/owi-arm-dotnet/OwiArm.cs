namespace owi_arm_dotnet;

internal class OwiArm : IOwiArm
{
    private readonly IOwiUsbConnection owiUsbConnection;

    public OwiArm(IOwiUsbConnection owiUsbConnection)
    {
        this.owiUsbConnection = owiUsbConnection;
    }

    /// <inheritdoc />
    public bool IsConnected => owiUsbConnection.IsOpen;

    /// <inheritdoc />
    public Task ConnectAsync()
    {
        if (owiUsbConnection.IsOpen == false)
        {
            return owiUsbConnection.OpenAsync();
        }

        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public async Task DisconnectAsync()
    {
        if (owiUsbConnection.IsOpen)
        {
            await owiUsbConnection.CloseAsync();
        }
    }

    /// <inheritdoc />
    public Task SendCommandAsync(IOwiCommand command)
    {
        if (owiUsbConnection.IsOpen == false)
        {
            throw new InvalidOperationException("Unable to send command because the connection is not open.");
        }

        return owiUsbConnection.SendAsync(command.ArmByte, command.BaseByte, command.LedByte);
    }
}