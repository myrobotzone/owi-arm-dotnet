using System;
using System.Collections;

namespace owi_arm_dotnet
{
    public class OwiCommand : IOwiCommand
    {
        BitArray ledArray = new BitArray(8);
        BitArray armArray = new BitArray(8);
        BitArray baseArray = new BitArray(8);

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
                return ConvertToByte(this.armArray);
            }
        }

        public byte BaseByte
        {
            get
            {
                return ConvertToByte(this.baseArray);
            }
        }

        public IOwiCommand StopAllMovements()
        {
            armArray.SetAll(false);
            baseArray.SetAll(false);
            return this;
        }

        public IOwiCommand LedOn()
        {
            this.ledArray.Set(0, true);
            return this;
        }

        public IOwiCommand LedOff()
        {
            this.ledArray.Set(0, false);
            return this;
        }

        public IOwiCommand GripperOpen()
        {
            this.GripperStop();
            this.armArray.Set(1, true);
            return this;
        }

        public IOwiCommand GripperClose()
        {
            this.GripperStop();
            this.armArray.Set(0, true);
            return this;
        }

        public IOwiCommand GripperStop()
        {
            this.armArray.Set(0, false);
            this.armArray.Set(1, false);
            return this;
        }

        public IOwiCommand ElbowUp()
        {
            this.ElbowStop();
            this.armArray.Set(4, true);
            return this;
        }

        public IOwiCommand ElbowDown()
        {
            this.ElbowStop();
            this.armArray.Set(5, true);
            return this;
        }

        public IOwiCommand ElbowStop()
        {
            this.armArray.Set(4, false);
            this.armArray.Set(5, false);
            return this;
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
