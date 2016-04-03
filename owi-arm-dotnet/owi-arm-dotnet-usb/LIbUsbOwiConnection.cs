using LibUsbDotNet;
using LibUsbDotNet.Main;
using owi_arm_dotnet;
using System;
using System.Threading.Tasks;

namespace owi_arm_dotnet_usb
{
    public class LibUsbOwiConnection : IOwiUsbConnection
    {
        private UsbDevice device;

        public bool IsOpen
        {
            get
            {
                return this.device != null && this.device.IsOpen;
            }
        }

        public Task OpenAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                var finder = new UsbDeviceFinder((int)OwiUsbConstants.Vid, (int)OwiUsbConstants.Pid);
                this.device = UsbDevice.OpenUsbDevice(finder);
                if (device == null)
                {
                    var message = string.Format("Unable to locate owi robotic arm with pid = {0}, vid = {1}. Please ensure you have the device connected with a valid usb driver installed.", finder.Pid, finder.Vid);
                    throw new InvalidOperationException(message);
                }
            });
        }

        public Task SendAsync(byte byte1, byte byte2, byte byte3)
        {
            return Task.Factory.StartNew(() =>
            {
                const int expectedTransferLength = 3;
                var command = new Byte[]
                        {
                            byte1,
                            byte2,
                            byte3
                        };
                int transferLength = 0;
                var packet = new UsbSetupPacket(0x40, OwiUsbConstants.Requst, OwiUsbConstants.Value, 0, 0);
                var code = this.device.ControlTransfer(ref packet, command, expectedTransferLength, out transferLength);
                if (transferLength != expectedTransferLength)
                {
                    throw new InvalidOperationException("Sending message to robot arm failed with an unexpected number of transfer bytes.");
                }
            });
        }

        public Task CloseAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                this.device.Close();
            });
        }
    }
}
