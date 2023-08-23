using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetACSEvent
{
    public class LoginDetail
    {
        public LoginDetail(string DeviceAddress, string UserName, string Password, string Port)
        {
            this.DeviceAddress = DeviceAddress;
            this.Password = Password;
            this.UserName = UserName;
            this.Port = Port;
        }
        public string DeviceAddress { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Port { get; set; }
    }
}
