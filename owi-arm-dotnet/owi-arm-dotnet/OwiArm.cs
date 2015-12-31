using System;

namespace owi_arm_dotnet
{
    public class OwiArm : IOwiArm
    {
        private IOwiUsbConnection owiUsbConnection;

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

        public void MoveArm(ICommandPacket packet)
        {
            if (this.owiUsbConnection.IsOpen == false)
            {
                throw new InvalidOperationException("Unable to send command because the connection is not open.");
            }

            this.owiUsbConnection.Send(packet.ArmCommand.Value, packet.BaseOfArmCommand.Value, 0);
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
