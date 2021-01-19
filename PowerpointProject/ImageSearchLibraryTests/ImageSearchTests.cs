using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerpointProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerpointProject.Tests
{
    [TestClass()]
    public class ImageSearchTests
    {
        [TestMethod()]
        public void SearchTest()
        {
            Assert.IsTrue(ImageSearch.Search("Light Box").Count > 0);
        }
    }
}