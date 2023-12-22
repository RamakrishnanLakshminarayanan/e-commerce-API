using e_commerce_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITestProject.WatchAPITest
{
    public class WatchAPICall
    {
        [Test]
        [Category("APITests")]
        public void API_Test()
        {
            //Arrange
            WatchService watchService = new WatchService();
            List<string> watches = new List<string>();
            watches.Add("001");
            watches.Add("002");
            watches.Add("003");
            watches.Add("004");

            //Act
            JsonResult Result = watchService.CheckoutWatches(watches);

            //Assert
            Assert.That(Result, Is.Not.Null);
        }
    }
}
