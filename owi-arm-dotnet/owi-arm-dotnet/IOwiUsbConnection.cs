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

        Task OpenAsync();

        Task SendAsync(byte byte1, byte byte2, byte byte3);

        Task CloseAsync();
    }
}
