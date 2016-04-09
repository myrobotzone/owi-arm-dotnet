using System;
using System.Threading.Tasks;

namespace owi_arm_dotnet
{
    internal class OwiArm : IOwiArm
    {
        private IOwiUsbConnection owiUsbConnection;

        public OwiArm(IOwiUsbConnection owiUsbConnection)
        {
            this.owiUsbConnection = owiUsbConnection;
        }

        /// <inheritdoc />
        public bool IsConnected
        {
            get
            {
                return this.owiUsbConnection.IsOpen;
            }
        }

        /// <inheritdoc />
        public Task ConnectAsync()
        {
            if (this.owiUsbConnection.IsOpen == false)
            {
                return this.owiUsbConnection.OpenAsync();
            }
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public async Task DisconnectAsync()
        {
            if (this.owiUsbConnection.IsOpen)
            {
                await this.owiUsbConnection.CloseAsync();
            }
        }

        /// <inheritdoc />
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
