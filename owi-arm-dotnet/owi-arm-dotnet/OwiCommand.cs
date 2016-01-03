using System;
using System.Collections;

namespace owi_arm_dotnet
{
    public class OwiCommand : IOwiCommand
    {
        BitArray ledArray = new BitArray(8);
        BitArray elbowArray = new BitArray(8);

        public byte LedByte
        {
            get
            {
                return ConvertToByte(this.ledArray);
            }
        }

        public byte ArmByte
        {
            get
            {
                return ConvertToByte(this.elbowArray);
            }
        }

        public byte BaseByte
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void LedOn()
        {
            this.ledArray.Set(0, true);
        }

        public void LedOff()
        {
            this.ledArray.Set(0, false);
        }

        public void ElbowUp()
        {
            this.ElbowStop();
            this.elbowArray.Set(4, true);
        }

        public void ElbowDown()
        {
            this.ElbowStop();
            this.elbowArray.Set(5, true);
        }

        public void ElbowStop()
        {
            this.elbowArray.Set(4, false);
            this.elbowArray.Set(5, false);
        }

        private byte ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}
