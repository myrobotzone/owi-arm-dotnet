using FluentAssertions;
using owi_arm_dotnet_usb;

namespace owi_arm_dotnet_usb_test
{
    public class LIbUsbOwiConnectionTest
    {
        [Fact]
        public async Task SendAsync_ConnectionIsNotOpen_ThrowsException()
        {
            var connection = new LibUsbOwiConnection();
            
            await connection
                .Invoking(async con => { await con.SendAsync(0, 0, 0); })
                .Should()
                .ThrowAsync<InvalidOperationException>();
        }
    }
}
