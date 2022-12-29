using System.Collections;

namespace owi_arm_dotnet;

internal class OwiCommand : IOwiCommand
{
    private readonly BitArray armArray = new(8);
    private readonly BitArray baseArray = new(8);
    private readonly BitArray ledArray = new(8);

    public byte LedByte => ConvertToByte(ledArray);

    public byte ArmByte => ConvertToByte(armArray);

    public byte BaseByte => ConvertToByte(baseArray);

    public IOwiCommand StopAllMovements()
    {
        armArray.SetAll(false);
        baseArray.SetAll(false);
        return this;
    }

    public IOwiCommand LedOn()
    {
        ledArray.Set(0, true);
        return this;
    }

    public IOwiCommand LedOff()
    {
        ledArray.Set(0, false);
        return this;
    }

    public IOwiCommand GripperOpen()
    {
        GripperStop();
        armArray.Set(1, true);
        return this;
    }

    public IOwiCommand GripperClose()
    {
        GripperStop();
        armArray.Set(0, true);
        return this;
    }

    public IOwiCommand GripperStop()
    {
        armArray.Set(0, false);
        armArray.Set(1, false);
        return this;
    }

    public IOwiCommand WristUp()
    {
        armArray.Set(2, true);
        return this;
    }

    public IOwiCommand WristDown()
    {
        WristStop();
        armArray.Set(3, true);
        return this;
    }

    public IOwiCommand WristStop()
    {
        armArray.Set(2, false);
        armArray.Set(3, false);
        return this;
    }

    public IOwiCommand ElbowUp()
    {
        ElbowStop();
        armArray.Set(4, true);
        return this;
    }

    public IOwiCommand ElbowDown()
    {
        ElbowStop();
        armArray.Set(5, true);
        return this;
    }

    public IOwiCommand ElbowStop()
    {
        armArray.Set(4, false);
        armArray.Set(5, false);
        return this;
    }

    public IOwiCommand ShoulderUp()
    {
        ShoulderStop();
        armArray.Set(6, true);
        return this;
    }

    public IOwiCommand ShoulderDown()
    {
        ShoulderStop();
        armArray.Set(7, true);
        return this;
    }

    public IOwiCommand ShoulderStop()
    {
        armArray.Set(6, false);
        armArray.Set(7, false);
        return this;
    }

    public IOwiCommand BaseRotateClockwise()
    {
        BaseRotateStop();
        baseArray.Set(1, true);
        return this;
    }

    public IOwiCommand BaseRotateCounterClockwise()
    {
        BaseRotateStop();
        baseArray.Set(0, true);
        return this;
    }

    public IOwiCommand BaseRotateStop()
    {
        baseArray.Set(0, false);
        baseArray.Set(1, false);
        return this;
    }

    public bool IsAllMovementOff => ArmByte == 0 && BaseByte == 0;

    public bool Equals(IOwiCommand other)
    {
        return ArmByte == other.ArmByte && BaseByte == other.BaseByte && LedByte == other.LedByte;
    }

    private static byte ConvertToByte(BitArray bits)
    {
        if (bits.Length != 8)
        {
            throw new ArgumentException("The number of bits must be 8");
        }

        var bytes = new byte[1];
        bits.CopyTo(bytes, 0);
        return bytes[0];
    }
}