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

        public IOwiCommand WristUp()
        {
            this.armArray.Set(2, true);
            return this;
        }

        public IOwiCommand WristDown()
        {
            this.WristStop();
            this.armArray.Set(3, true);
            return this;
        }

        public IOwiCommand WristStop()
        {
            this.armArray.Set(2, false);
            this.armArray.Set(3, false);
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

        public IOwiCommand ShoulderUp()
        {
            this.ShoulderStop();
            this.armArray.Set(6, true);
            return this;
        }

        public IOwiCommand ShoulderDown()
        {
            this.ShoulderStop();
            this.armArray.Set(7, true);
            return this;
        }

        public IOwiCommand ShoulderStop()
        {
            this.armArray.Set(6, false);
            this.armArray.Set(7, false);
            return this;
        }

        public IOwiCommand BaseRotateClockwise()
        {
            this.BaseRotateStop();
            this.baseArray.Set(1, true);
            return this;
        }

        public IOwiCommand BaseRotateCounterClockwise()
        {
            this.BaseRotateStop();
            this.baseArray.Set(0, true);
            return this;
        }

        public IOwiCommand BaseRotateStop()
        {
            this.baseArray.Set(0, false);
            this.baseArray.Set(1, false);
            return this;
        }

        private byte ConvertToByte(BitArray bits)
        {
            if (bits.Length != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            ((ICollection)bits).CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}
