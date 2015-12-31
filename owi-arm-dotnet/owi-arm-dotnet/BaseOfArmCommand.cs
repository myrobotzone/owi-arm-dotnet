using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace owi_arm_dotnet
{
    public class BaseOfArmCommand : ICommand
    {
        internal BaseOfArmCommand(Byte value)
        {
            this.Value = value;
        }

        public Byte Value { get; private set; }
    }
}
