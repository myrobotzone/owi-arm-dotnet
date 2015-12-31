using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace owi_arm_dotnet
{
    public interface IOwiUsbConnection
    {
        bool IsOpen { get; }

        void Open();

        void Send(byte byte1, byte byte2, byte byte3);

        void Close();

    }
}
