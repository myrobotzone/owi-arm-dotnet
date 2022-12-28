using FluentAssertions;
using Moq;
using owi_arm_dotnet;

namespace owi_arm_dotnet_test
{
    public class OwiFactoryTest
    {
        [Fact]
        public void CreateArm_CreatesOwiArm()
        {
            var factory = new OwiFactory();

            var arm = factory.CreateArm(new Mock<IOwiUsbConnection>().Object);

            arm.Should().BeOfType(typeof(OwiArm));
        }

        [Fact]
        public void CreateCommand_CreatesOwiCommand()
        {
            var factory = new OwiFactory();

            var command = factory.CreateCommand();

            command.Should().BeOfType(typeof(OwiCommand));
        }
    }
}
