using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace owi_arm_dotnet
{
    public class CommandPacket : ICommandPacket
    {
        public CommandPacket(ArmCommand command)
            : this(command, BaseOfArmCommands.Stop, LedCommands.Off)
        {
        }

        public CommandPacket(BaseOfArmCommand command)
            : this(ArmCommands.Stop, command, LedCommands.Off)
        {
        }

        public CommandPacket(LedCommand command)
            : this(ArmCommands.Stop, BaseOfArmCommands.Stop, command)
        {
        }

        public CommandPacket(ArmCommand armCommand, BaseOfArmCommand baseOfArmCommand, LedCommand ledCommand)
        {
            this.ArmCommand = armCommand;
            this.BaseOfArmCommand = baseOfArmCommand;
            this.LedCommand = ledCommand;
        }

        public ArmCommand ArmCommand { get; private set; }

        public BaseOfArmCommand BaseOfArmCommand { get; private set; }

        public LedCommand LedCommand { get; private set; }
    }
}
