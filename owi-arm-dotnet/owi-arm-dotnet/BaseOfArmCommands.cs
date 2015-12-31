using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace owi_arm_dotnet
{
    public static class BaseOfArmCommands
    {
        static BaseOfArmCommands()
        {
            Stop = new BaseOfArmCommand(0);
            RotateClockwise = new BaseOfArmCommand(1);
            RotateCounterClockwise = new BaseOfArmCommand(10);
        }

        public static BaseOfArmCommand Stop { get; private set; }

        public static BaseOfArmCommand RotateClockwise { get; private set; }

        public static BaseOfArmCommand RotateCounterClockwise { get; set; }
    }
}
