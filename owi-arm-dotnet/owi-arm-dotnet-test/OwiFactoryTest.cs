using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using owi_arm_dotnet;

namespace owi_arm_dotnet_test
{
    [TestClass]
    public class OwiFactoryTest
    {
        [TestMethod]
        public void CreateArm_CreatesOwiArm()
        {
            var factory = new OwiFactory();

            var arm = factory.CreateArm(new Mock<IOwiUsbConnection>().Object);

            Assert.IsInstanceOfType(arm, typeof(OwiArm));
        }

        [TestMethod]
        public void CreateCommand_CreatesOwiCommand()
        {
            var factory = new OwiFactory();

            var command = factory.CreateCommand();

            Assert.IsInstanceOfType(command, typeof(OwiCommand));
        }
    }
}
