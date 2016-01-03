using System;

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

        public void Connect()
        {
            if (this.owiUsbConnection.IsOpen == false)
            {
                this.owiUsbConnection.Open();
           } 
        }

        public void SendCommand(IOwiCommand command)
        {
            if (this.owiUsbConnection.IsOpen == false)
            {
                throw new InvalidOperationException("Unable to send command because the connection is not open.");
            }

            this.owiUsbConnection.Send(command.ArmByte, command.BaseByte, command.LedByte);
        }

        public void Disconnect()
        {
            if (this.owiUsbConnection.IsOpen)
            {
                this.owiUsbConnection.Close();
            }
        }
    }
}
