using LibUsbDotNet;
using LibUsbDotNet.Main;
using owi_arm_dotnet;
using System;
using System.Threading.Tasks;

namespace owi_arm_dotnet_usb
{
    /// <summary>
    /// Implements an owi usb connection using lib usb. This can be used on
    /// Windows desktop devices.
    /// </summary>
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public Task SendAsync(byte armByte, byte baseByte, byte ledByte)
        {
            return Task.Factory.StartNew(() =>
            {
                const int expectedTransferLength = 3;
                var command = new Byte[]
                        {
                            armByte,
                            baseByte,
                            ledByte
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

        /// <inheritdoc />
        public Task CloseAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                this.device.Close();
            });
        }
    }
}
