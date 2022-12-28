using owi_arm_dotnet;
using FluentAssertions;
namespace owi_arm_dotnet_test
{
    public class OwiCommandTest
    {
        [Fact]
        public void LedOn_LedWasOff_LedBitIsSet()
        {
            var command = new OwiCommand();

            command.LedOn();

            IsBitSet(command.LedByte, 0).Should().BeTrue();
        }


        [Fact]
        public void LedOff_LedWasOn_LedBitIsNotSet()
        {
            var command = new OwiCommand();

            command.LedOn().LedOff();

            IsBitSet(command.LedByte, 0).Should().BeFalse();
        }

        [Fact]
        public void GripperClose_GriperIsStopped_GripperCloseBitIsSet()
        {
            var command = new OwiCommand();

            command.GripperStop().GripperClose();

            IsBitSet(command.ArmByte, 0).Should().BeTrue();
        }

        [Fact]
        public void GripperOpen_GriperIsClosing_GripperOpenBitIsSet()
        {
            var command = new OwiCommand();

            command.GripperClose().GripperOpen();

            IsBitSet(command.ArmByte, 0).Should().BeFalse();
            IsBitSet(command.ArmByte, 1).Should().BeTrue();
        }

        [Fact]
        public void WristUp_WristIsStopped_WristUpBitIsSet()
        {
            var command = new OwiCommand();

            command.WristUp();

            IsBitSet(command.ArmByte, 2).Should().BeTrue();
        }

        [Fact]
        public void WristDown_WristWasMovingUp_WristDownBitIsSet()
        {
            var command = new OwiCommand();

            command.WristUp().WristDown();

            IsBitSet(command.ArmByte, 2).Should().BeFalse();
            IsBitSet(command.ArmByte, 3).Should().BeTrue();
        }

        [Fact]
        public void WristStop_WristWasMovingUp_WristDownBitIsSet()
        {
            var command = new OwiCommand();

            command.WristUp().WristStop();

            IsBitSet(command.ArmByte, 2).Should().BeFalse();
            IsBitSet(command.ArmByte, 3);
        }

        [Fact]
        public void ElbowUp_ElbowWasStoped_ElbowUpBitIsSet()
        {
            var command = new OwiCommand();

            command.ElbowUp();

            IsBitSet(command.ArmByte, 4).Should().BeTrue();
        }

        [Fact]
        public void ElbowDown_ElbowWasMovingUp_ElbowDownBitIsSet()
        {
            var command = new OwiCommand();

            command.ElbowUp().ElbowDown();

            IsBitSet(command.ArmByte, 4).Should().BeFalse();
            IsBitSet(command.ArmByte, 5).Should().BeTrue();
        }

        [Fact]
        public void ElbowStop_ElbowWasDown_ElbowBitsAreReset()
        {
            var command = new OwiCommand();

            command.ElbowDown().ElbowStop();

            IsBitSet(command.ArmByte, 4).Should().BeFalse();
            IsBitSet(command.ArmByte, 5).Should().BeFalse();
        }

        [Fact]
        public void ShoulderUp_ShoulderWasStoped_ShoulderUpBitIsSet()
        {
            var command = new OwiCommand();

            command.ShoulderUp();

            IsBitSet(command.ArmByte, 6).Should().BeTrue();
        }

        [Fact]
        public void ShoulderDown_ShoulderWasMovingUp_ShoulderDownBitIsSet()
        {
            var command = new OwiCommand();

            command.ShoulderUp().ShoulderDown();

            IsBitSet(command.ArmByte, 6).Should().BeFalse();
            IsBitSet(command.ArmByte, 7).Should().BeTrue();
        }

        [Fact]
        public void ShoulderStop_ShoulderWasDown_ShoulderBitsAreReset()
        {
            var command = new OwiCommand();

            command.ShoulderDown().ShoulderStop();

            IsBitSet(command.ArmByte, 6).Should().BeFalse();
            IsBitSet(command.ArmByte, 7);
        }

        [Fact]
        public void BaseRotateClockwise_BaseRotateWasStoped_BaseRotateClockwiseBitIsSet()
        {
            var command = new OwiCommand();

            command.BaseRotateClockwise();

            IsBitSet(command.BaseByte, 1).Should().BeTrue();
        }

        [Fact]
        public void BaseRotateCounterClockwise_BaseRotateWasMovingClockwise_BaseRotateCounterClockwiseBitIsSet()
        {
            var command = new OwiCommand();

            command.BaseRotateClockwise().BaseRotateCounterClockwise();

            IsBitSet(command.BaseByte, 1).Should().BeFalse();
            IsBitSet(command.BaseByte, 0).Should().BeTrue();
        }

        [Fact]
        public void BaseRotateStop_BaseRotateWasClockwise_BaseRotateBitsAreReset()
        {
            var command = new OwiCommand();

            command.BaseRotateClockwise().BaseRotateStop();

            IsBitSet(command.BaseByte, 0).Should().BeFalse();
            IsBitSet(command.BaseByte, 1).Should().BeFalse();
        }

        [Fact]
        public void StopAllMovements_LedIsOneAndArmIsMoving_LedBitIsOnButMovementsBitsAreReset()
        {
            var command = new OwiCommand();

            command.LedOn().ElbowUp().StopAllMovements();

            command.LedByte.Should().NotBe(0);
            command.ArmByte.Should().Be(0);
            command.BaseByte.Should().Be(0);
        }

        [Fact]
        public void IsAllMovementOff_LedIsOn_ReturnsTrue()
        {
            var command = new OwiCommand();

            command.LedOn();

            command.IsAllMovementOff.Should().BeTrue();
        }

        bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}
