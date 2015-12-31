using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;

namespace owi_arm_dotnet
{
    internal class OwiUsbConnection : IOwiUsbConnection
    {
        private UsbDevice device;

        public bool IsOpen
        {
            get
            {
                return this.device != null && this.device.IsOpen;
            }
        }

        public void Open()
        {
            var finder = new UsbDeviceFinder(0x1267, 0);
            this.device = UsbDevice.OpenUsbDevice(finder);
            if (device == null)
            {
                var message = string.Format("Unable to locate owi robotic arm with pid = {0}, vid = {1}. Please ensure you have the device connected with a valid usb driver installed.", finder.Pid, finder.Vid);
                throw new InvalidOperationException(message);
            }
        }

        public void Send(byte byte1, byte byte2, byte byte3)
        {
            const int expectedTransferLength = 3;
            var command = new Byte[]
                        {
                            byte1,
                            byte2,
                            byte3
                        };
            int transferLength = 0;
            var packet = new UsbSetupPacket(0x40, 6, 0x100, 0, 0);
            var code = this.device.ControlTransfer(ref packet, command, expectedTransferLength, out transferLength);
            if (transferLength != expectedTransferLength)
            {
                throw new InvalidOperationException("Sending message to robot arm failed with an unexpected number of transfer bytes.");
            }
        }

        public void Close()
        {
            this.device.Close();
        }
    }
}
