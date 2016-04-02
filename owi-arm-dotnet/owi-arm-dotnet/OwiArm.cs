using System;
using System.Threading.Tasks;

namespace owi_arm_dotnet
{
    public class OwiArm : IOwiArm
    {
        private IOwiUsbConnection owiUsbConnection;

        public OwiArm()
        {
            this.owiUsbConnection = new OwiUsbConnection();
        }

        public OwiArm(IOwiUsbConnection owiUsbConnection)
        {
            this.owiUsbConnection = owiUsbConnection;
        }

        public bool IsConnected
        {
            get
            {
                return this.owiUsbConnection.IsOpen;
            }
        }

        public Task ConnectAsync()
        {
            if (this.owiUsbConnection.IsOpen == false)
            {
                return this.owiUsbConnection.OpenAsync();
            }
            return Task.FromResult(true);
        }

        public async Task DisconnectAsync()
        {
            if (this.owiUsbConnection.IsOpen)
            {
                await this.owiUsbConnection.CloseAsync();
            }
        }

        public Task SendCommandAsync(IOwiCommand command)
        {
            if (this.owiUsbConnection.IsOpen == false)
            {
                throw new InvalidOperationException("Unable to send command because the connection is not open.");
            }

            return this.owiUsbConnection.SendAsync(command.ArmByte, command.BaseByte, command.LedByte);
        }
    }
}
