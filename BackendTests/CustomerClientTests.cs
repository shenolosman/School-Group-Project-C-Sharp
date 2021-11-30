using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DataLayer.Backend;
using Xunit;

namespace BackendTests
{
    public class CustomerClientTests
    {
       public AdminBackend adminBackend = new AdminBackend();

        [Fact]
        public void ClientFrontendTest()
        {

            adminBackend.PrepDatabase();

            var UserList = AdminBackend.AllUsers();
            var UserNameList = new List<string>();
            string username = "Emmy";
            bool loggedin;
            foreach (var user in UserList)
            {
                UserNameList.Add(user.Username);
            }
            loggedin = UserNameList.Contains(username);
            Assert.True(loggedin);
        }
        
    }
}
