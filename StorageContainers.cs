using Moq;
using System.Linq;
using SolidFire.SDK.Adaptor;
using System;

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
			Assert.IsTrue(result.StorageContainer.Status == "active", "Died on .StorageContainer.Status");
			Assert.IsTrue(result.StorageContainer.StorageContainerID == "2b0653fb-3d64-4ae0-9ef8-356e685b0b2e", "Died on .StorageContainer.StorageContainerID");
			Assert.IsTrue(result.StorageContainer.Name == "ExampleName", "Died on .StorageContainer.Name");
			Assert.IsTrue(result.StorageContainer.InitiatorSecret == "Z5k)Cf~jiIxp3R4[", "Died on .StorageContainer.InitiatorSecret");
			Assert.IsTrue(result.StorageContainer.TargetSecret == "N07S4}aEh5oU]L0l", "Died on .StorageContainer.TargetSecret");
			Assert.IsTrue(result.StorageContainer.ProtocolEndpointType == "SCSI", "Died on .StorageContainer.ProtocolEndpointType");
			
        }
        [TestMethod]
        public void TestGetStorageContainer()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_GetStorageContainer_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            GetStorageContainerResult result = sfe.GetStorageContainer();
			Assert.IsTrue(result.StorageContainer.Status == "active", "Died on .StorageContainer.Status");
			Assert.IsTrue(result.StorageContainer.StorageContainerID == "2b0653fb-3d64-4ae0-9ef8-356e685b0b2e", "Died on .StorageContainer.StorageContainerID");
			Assert.IsTrue(result.StorageContainer.Name == "ExampleName", "Died on .StorageContainer.Name");
			Assert.IsTrue(result.StorageContainer.InitiatorSecret == "Z5k)Cf~jiIxp3R4[", "Died on .StorageContainer.InitiatorSecret");
			Assert.IsTrue(result.StorageContainer.TargetSecret == "N07S4}aEh5oU]L0l", "Died on .StorageContainer.TargetSecret");
			Assert.IsTrue(result.StorageContainer.ProtocolEndpointType == "SCSI", "Died on .StorageContainer.ProtocolEndpointType");
			
        }
        [TestMethod]
        public void TestGetStorageContainerEfficiency()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_GetStorageContainerEfficiency_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            GetStorageContainerEfficiencyResult result = sfe.GetStorageContainerEfficiency();
			Assert.IsTrue(result.StorageContainer.Status == "active", "Died on .StorageContainer.Status");
			Assert.IsTrue(result.StorageContainer.StorageContainerID == "2b0653fb-3d64-4ae0-9ef8-356e685b0b2e", "Died on .StorageContainer.StorageContainerID");
			Assert.IsTrue(result.StorageContainer.Name == "ExampleName", "Died on .StorageContainer.Name");
			Assert.IsTrue(result.StorageContainer.InitiatorSecret == "Z5k)Cf~jiIxp3R4[", "Died on .StorageContainer.InitiatorSecret");
			Assert.IsTrue(result.StorageContainer.TargetSecret == "N07S4}aEh5oU]L0l", "Died on .StorageContainer.TargetSecret");
			Assert.IsTrue(result.StorageContainer.ProtocolEndpointType == "SCSI", "Died on .StorageContainer.ProtocolEndpointType");
			
        }
        [TestMethod]
        public void TestListStorageContainer()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_ListStorageContainer_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            ListStorageContainerResult result = sfe.ListStorageContainer();
			Assert.IsTrue(result.StorageContainer.Status == "active", "Died on .StorageContainer.Status");
			Assert.IsTrue(result.StorageContainer.StorageContainerID == "2b0653fb-3d64-4ae0-9ef8-356e685b0b2e", "Died on .StorageContainer.StorageContainerID");
			Assert.IsTrue(result.StorageContainer.Name == "ExampleName", "Died on .StorageContainer.Name");
			Assert.IsTrue(result.StorageContainer.InitiatorSecret == "Z5k)Cf~jiIxp3R4[", "Died on .StorageContainer.InitiatorSecret");
			Assert.IsTrue(result.StorageContainer.TargetSecret == "N07S4}aEh5oU]L0l", "Died on .StorageContainer.TargetSecret");
			Assert.IsTrue(result.StorageContainer.ProtocolEndpointType == "SCSI", "Died on .StorageContainer.ProtocolEndpointType");
			
        }"
    }
}
