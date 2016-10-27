
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SolidFire.Element.Api;
using SolidFire.Core;
using Moq;
using System.Linq;
using SolidFire.SDK.Adaptor;

namespace Element.Tests
{
    [TestClass]
    public class StorageContainersTests
    {
        [TestMethod]
        public void TestCreateStorageContainer()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_CreateStorageContainer_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            CreateStorageContainerResult result = sfe.CreateStorageContainer();
			Assert.IsTrue(result.StorageContainer.Status == "active", "Died on .StorageContainer.Status");
			Assert.IsTrue(result.StorageContainer.StorageContainerID == "2b0653fb-3d64-4ae0-9ef8-356e685b0b2e", "Died on .StorageContainer.StorageContainerID");
			Assert.IsTrue(result.StorageContainer.Name == "ExampleName", "Died on .StorageContainer.Name");
			Assert.IsTrue(result.StorageContainer.InitiatorSecret == "Z5k)Cf~jiIxp3R4[", "Died on .StorageContainer.InitiatorSecret");
			Assert.IsTrue(result.StorageContainer.TargetSecret == "N07S4}aEh5oU]L0l", "Died on .StorageContainer.TargetSecret");
			Assert.IsTrue(result.StorageContainer.ProtocolEndpointType == "SCSI", "Died on .StorageContainer.ProtocolEndpointType");
			
        }
        [TestMethod]
        public void TestDeleteStorageContainer()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_DeleteStorageContainer_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            DeleteStorageContainerResult result = sfe.DeleteStorageContainer();
			
        }
        [TestMethod]
        public void TestGetStorageContainerEfficiency()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_GetStorageContainerEfficiency_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            GetStorageContainerEfficiencyResult result = sfe.GetStorageContainerEfficiency();
			Assert.IsTrue(result.Timestamp == "2016-10-14T22:41:24Z", "Died on .Timestamp");
			
        }
        [TestMethod]
        public void TestListStorageContainer()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_ListStorageContainer_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            ListStorageContainerResult result = sfe.ListStorageContainer();
			Assert.IsTrue(result.Timestamp == "2016-10-14T22:41:24Z", "Died on .Timestamp");
			
        }
        [TestMethod]
        public void TestModifyStorageContainer()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_ModifyStorageContainer_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            ModifyStorageContainerResult result = sfe.ModifyStorageContainer();
			Assert.IsTrue(result.StorageContainer.Status == "active", "Died on .StorageContainer.Status");
			Assert.IsTrue(result.StorageContainer.StorageContainerID == "b4528ea8-2930-41a0-8b8e-6361e1f0a71f", "Died on .StorageContainer.StorageContainerID");
			Assert.IsTrue(result.StorageContainer.Name == "ExampleName", "Died on .StorageContainer.Name");
			Assert.IsTrue(result.StorageContainer.InitiatorSecret == "e;~t5f4T~r8"CQK9", "Died on .StorageContainer.InitiatorSecret");
			Assert.IsTrue(result.StorageContainer.TargetSecret == "LIU4?KW8mOlMZO^6", "Died on .StorageContainer.TargetSecret");
			Assert.IsTrue(result.StorageContainer.ProtocolEndpointType == "SCSI", "Died on .StorageContainer.ProtocolEndpointType");
			
        }
    }
}
