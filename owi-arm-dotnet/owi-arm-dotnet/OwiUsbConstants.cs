using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace owi_arm_dotnet
{
    public static class OwiUsbConstants
    {
        public static uint Vid
        {
            get
            {
                return 0x1267;
            }
        }

        public static uint Pid
        {
            get
            {
                return 0x0000;
            }
        }
    }
}
