#region Copyright
/*
* Copyright 2014-2016 NetApp, Inc. All Rights Reserved.
*
* CONFIDENTIALITY NOTICE: THIS SOFTWARE CONTAINS CONFIDENTIAL INFORMATION OF
* NETAPP, INC. USE, DISCLOSURE OR REPRODUCTION IS PROHIBITED WITHOUT THE PRIOR
* EXPRESS WRITTEN PERMISSION OF NETAPP, INC.
*/
#endregion


#region Using Directives
using System;
using System.Linq;
using System.ComponentModel;
using System.Management.Automation;
using SolidFire.Core;
using SolidFire.Element.Api;
#endregion

namespace SolidFire.element
{
	/// <summary>
	/// CreateVirtualVolumeHost creates a new ESX host.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFVirtualVolumeHost")]
	public class CreateVirtualVolumeHost : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The GUID of the ESX host.
		/// </summary>
		private UUIDNullable _virtualVolumeHostID;
		/// <summary>
		/// The GUID of the ESX Cluster.
		/// </summary>
		private UUIDNullable _clusterID;
		private string[] _initiatorNames;
		/// <summary>
		/// A list of PEs the host is aware of.
		/// </summary>
		private Guid[] _visibleProtocolEndpointIDs;
		/// <summary>
		/// IP or DNS name for the host.
		/// </summary>
		private string _hostAddress;
		private UUIDNullable _callingVirtualVolumeHostID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The GUID of the ESX host.")]
		public UUIDNullable VirtualVolumeHostID
		{
			get
			{
				return _virtualVolumeHostID;
			}
			set
			{
				_virtualVolumeHostID = value;
			}
		}
		[Parameter(HelpMessage = "The GUID of the ESX Cluster.")]
		public UUIDNullable ClusterID
		{
			get
			{
				return _clusterID;
			}
			set
			{
				_clusterID = value;
			}
		}
		[Parameter(Mandatory = false)]
		public string[] InitiatorNames
		{
			get
			{
				return _initiatorNames;
			}
			set
			{
				_initiatorNames = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "A list of PEs the host is aware of.")]
		public Guid[] VisibleProtocolEndpointIDs
		{
			get
			{
				return _visibleProtocolEndpointIDs;
			}
			set
			{
				_visibleProtocolEndpointIDs = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "IP or DNS name for the host.")]
		public string HostAddress
		{
			get
			{
				return _hostAddress;
			}
			set
			{
				_hostAddress = value;
			}
		}
		[Parameter(Mandatory = false)]
		public UUIDNullable CallingVirtualVolumeHostID
		{
			get
			{
				return _callingVirtualVolumeHostID;
			}
			set
			{
				_callingVirtualVolumeHostID = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(9);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new CreateVirtualVolumeHostRequest();
			request.VirtualVolumeHostID = _virtualVolumeHostID;
			request.ClusterID = _clusterID;
			request.InitiatorNames = _initiatorNames;
			request.VisibleProtocolEndpointIDs = _visibleProtocolEndpointIDs;
			request.HostAddress = _hostAddress;
			request.CallingVirtualVolumeHostID = _callingVirtualVolumeHostID;
			var objsFromAPI = SendRequest<CreateVirtualVolumeHostResult>("CreateVirtualVolumeHost", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		