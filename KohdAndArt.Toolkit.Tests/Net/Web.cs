using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kohd = KohdAndArt.Toolkit.Net;

namespace KohdAndArt.UtilityLibrary.Tests.Net
{
    [TestClass]
    public class Web
    {
        [TestMethod]
        public void DownloadHtmlAsString_GoogleDotCom()
        {
            // Arrange
            string result = string.Empty;
            string url = "http://www.google.com";

            // Act
            var kohdWebClient = new Kohd.Web();
            result = kohdWebClient.DownloadHtmlAsString(new Uri(url));

            // Assert
            Assert.IsTrue(result.Length > 0);
        }
    }
}
 