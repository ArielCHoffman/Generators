using Moq;
using System.Linq;
using SolidFire.SDK.Adaptor;
using System;

namespace Element.Tests
{
    [TestClass]
    public class FeatureTests
    {
        [TestMethod]
        public void TestEnableFeature()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_EnableFeature_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            EnableFeatureResult result = sfe.EnableFeature();
			
        }
        [TestMethod]
        public void TestGetFeatureStatus()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_GetFeatureStatus_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            GetFeatureStatusResult result = sfe.GetFeatureStatus();
			Assert.IsTrue(result.Features[0].Feature == "Vvols", "Died on .Features[0].Feature");
			
        }"
    }
}
