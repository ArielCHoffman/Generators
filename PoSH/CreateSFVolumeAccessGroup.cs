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
	/// Creates a new volume access group.
	/// The new volume access group must be given a name when it is created.
	/// Entering initiators and volumes are optional when creating a volume access group.
	/// Once the group is created volumes and initiator IQNs can be added.
	/// Any initiator IQN that is successfully added to the volume access group is able to access any volume in the group without CHAP authentication.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFVolumeAccessGroup")]
	public class CreateVolumeAccessGroup : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Name of the volume access group.
		/// It is not required to be unique, but recommended.
		/// </summary>
		private string _name;
		/// <summary>
		/// List of initiators to include in the volume access group.
		/// If unspecified, the access group will start out without configured initiators.
		/// </summary>
		private string[] _initiators;
		/// <summary>
		/// List of volumes to initially include in the volume access group.
		/// If unspecified, the access group will start without any volumes.
		/// </summary>
		private Int64[] _volumes;
		/// <summary>
		/// The ID of the SolidFire Virtual Network ID to associate the volume access group with.
		/// </summary>
		private Int64[] _virtualNetworkID;
		/// <summary>
		/// The ID of the VLAN Virtual Network Tag to associate the volume access group with.
		/// </summary>
		private Int64[] _virtualNetworkTags;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Name of the volume access group.', u'It is not required to be unique, but recommended.']")]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'List of initiators to include in the volume access group.', u'If unspecified, the access group will start out without configured initiators.']")]
		public string[] Initiators
		{
			get
			{
				return _initiators;
			}
			set
			{
				_initiators = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'List of volumes to initially include in the volume access group.', u'If unspecified, the access group will start without any volumes.']")]
		public Int64[] Volumes
		{
			get
			{
				return _volumes;
			}
			set
			{
				_volumes = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The ID of the SolidFire Virtual Network ID to associate the volume access group with.")]
		public Int64[] VirtualNetworkID
		{
			get
			{
				return _virtualNetworkID;
			}
			set
			{
				_virtualNetworkID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The ID of the VLAN Virtual Network Tag to associate the volume access group with.")]
		public Int64[] VirtualNetworkTags
		{
			get
			{
				return _virtualNetworkTags;
			}
			set
			{
				_virtualNetworkTags = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "List of Name/Value pairs in JSON object format.")]
		public hashtable Attributes
		{
			get
			{
				return _attributes;
			}
			set
			{
				_attributes = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(5);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new CreateVolumeAccessGroupRequest();
			request.Name = _name;
			request.Initiators = _initiators;
			request.Volumes = _volumes;
			request.VirtualNetworkID = _virtualNetworkID;
			request.VirtualNetworkTags = _virtualNetworkTags;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<CreateVolumeAccessGroupResult>("CreateVolumeAccessGroup", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		