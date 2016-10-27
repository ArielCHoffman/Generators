
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
    public class VirtualVolumesTests
    {
        [TestMethod]
        public void TestGetVirtualVolumesCount()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_GetVirtualVolumesCount_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            GetVirtualVolumesCountResult result = sfe.GetVirtualVolumesCount();
			
        }
        [TestMethod]
        public void TestListVirtualVolumeBindings()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_ListVirtualVolumeBindings_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            ListVirtualVolumeBindingsResult result = sfe.ListVirtualVolumeBindings();
			Assert.IsTrue(result.Bindings[0].VirtualVolumeHostID == "564de1a4-9a99-da0f-8b7c-3a41dfd64bf1", "Died on .Bindings[0].VirtualVolumeHostID");
			Assert.IsTrue(result.Bindings[0].ProtocolEndpointID == "5dd53da0-b9b7-43f9-9b7e-b41c2558e92b", "Died on .Bindings[0].ProtocolEndpointID");
			Assert.IsTrue(result.Bindings[0].VirtualVolumeSecondaryID == "0xe200000000a6", "Died on .Bindings[0].VirtualVolumeSecondaryID");
			Assert.IsTrue(result.Bindings[0].ProtocolEndpointInBandID == "naa.6f47acc2000000016a67746700000000", "Died on .Bindings[0].ProtocolEndpointInBandID");
			Assert.IsTrue(result.Bindings[0].ProtocolEndpointType == "SCSI", "Died on .Bindings[0].ProtocolEndpointType");
			Assert.IsTrue(result.Bindings[0].VirtualVolumeID == "269d3378-1ca6-4175-a18f-6d4839e5c746", "Died on .Bindings[0].VirtualVolumeID");
			
        }
        [TestMethod]
        public void TestListVirtualVolumeHosts()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_ListVirtualVolumeHosts_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            ListVirtualVolumeHostsResult result = sfe.ListVirtualVolumeHosts();
			Assert.IsTrue(result.Hosts[0].VisibleProtocolEndpointIDs[0] == "5dd53da0-b9b7-43f9-9b7e-b41c2558e92b", "Died on .Hosts[0].VisibleProtocolEndpointIDs[0]");
			Assert.IsTrue(result.Hosts[0].VirtualVolumeHostID == "564de1a4-9a99-da0f-8b7c-3a41dfd64bf1", "Died on .Hosts[0].VirtualVolumeHostID");
			Assert.IsTrue(result.Hosts[0].InitiatorNames[1] == "iqn.1998-01.com.vmware:zdc-dhcp-0-c-29-d6-4b-f1-5bcf9254", "Died on .Hosts[0].InitiatorNames[1]");
			Assert.IsTrue(result.Hosts[0].InitiatorNames[0] == "iqn.1998-01.com.vmware:zdc-dhcp-0-c-29-d6-4b-f1-1a0cd614", "Died on .Hosts[0].InitiatorNames[0]");
			Assert.IsTrue(result.Hosts[0].ClusterID == "5ebdb4ad-9617-4647-adfd-c1013578483b", "Died on .Hosts[0].ClusterID");
			Assert.IsTrue(result.Hosts[0].HostAddress == "172.30.89.117", "Died on .Hosts[0].HostAddress");
			
        }
        [TestMethod]
        public void TestListVirtualVolumeTasks()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_ListVirtualVolumeTasks_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            ListVirtualVolumeTasksResult result = sfe.ListVirtualVolumeTasks();
			Assert.IsTrue(result.Tasks[0].Status == "success", "Died on .Tasks[0].Status");
			Assert.IsTrue(result.Tasks[0].VirtualVolumeHostID == "564de1a4-9a99-da0f-8b7c-3a41dfd64bf1", "Died on .Tasks[0].VirtualVolumeHostID");
			Assert.IsTrue(result.Tasks[0].VirtualVolumeTaskID == "a1b72df7-66a6-489a-86e4-538d0dbe05bf", "Died on .Tasks[0].VirtualVolumeTaskID");
			Assert.IsTrue(result.Tasks[0].CloneVirtualVolumeID == "fafeb3a0-7dd9-4c9f-8a07-80e0bbf6f4d0", "Died on .Tasks[0].CloneVirtualVolumeID");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_GosType == "windows7Server64Guest", "Died on .Tasks[0].ParentMetadata.VMW_GosType");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.SFProfileId == "f4e5bade-15a2-4805-bf8e-52318c4ce443", "Died on .Tasks[0].ParentMetadata.SFProfileId");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_VmID == "502e0676-e510-ccdd-394c-667f6867fcdf", "Died on .Tasks[0].ParentMetadata.VMW_VmID");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_VvolProfile == "f4e5bade-15a2-4805-bf8e-52318c4ce443:0", "Died on .Tasks[0].ParentMetadata.VMW_VvolProfile");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_VvolAllocationType == "4", "Died on .Tasks[0].ParentMetadata.VMW_VvolAllocationType");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_VVolName == "asdf.vmdk", "Died on .Tasks[0].ParentMetadata.VMW_VVolName");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_VVolNamespace == "/vmfs/volumes/vvol:abaab415bedc44cd-98b8f37495884db0/rfc4122.269d3378-1ca6-4175-a18f-6d4839e5c746", "Died on .Tasks[0].ParentMetadata.VMW_VVolNamespace");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.SFgenerationId == "0", "Died on .Tasks[0].ParentMetadata.SFgenerationId");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_VVolType == "Data", "Died on .Tasks[0].ParentMetadata.VMW_VVolType");
			Assert.IsTrue(result.Tasks[0].ParentMetadata.VMW_ContainerId == "abaab415-bedc-44cd-98b8-f37495884db0", "Died on .Tasks[0].ParentMetadata.VMW_ContainerId");
			Assert.IsTrue(result.Tasks[0].Operation == "clone", "Died on .Tasks[0].Operation");
			Assert.IsTrue(result.Tasks[0].VirtualvolumeID == "fafeb3a0-7dd9-4c9f-8a07-80e0bbf6f4d0", "Died on .Tasks[0].VirtualvolumeID");
			
        }
    }
}
