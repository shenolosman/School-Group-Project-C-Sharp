using System.Linq;
using DataLayer.Backend;
using Xunit;

namespace BackendTests
{
    public class FoodBoxesTests
    {
        //Testing if user exits
        [Fact]
        public void UserTest()
        {
            AdminBackend.PrepDatabase();
            var users = AdminBackend.AllUsers();
            Assert.True(users.Where(x => x.Equals("KevinJ")) != null);
        }
    }
}