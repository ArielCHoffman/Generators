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
	/// Update initiators and add or remove volumes from a volume access group.
	/// A specified initiator or volume that duplicates an existing volume or initiator in a volume access group is left as-is.
	/// If a value is not specified for volumes or initiators, the current list of initiators and volumes are not changed.
	/// <br/><br/>
	/// Often, it is easier to use the convenience functions to modify initiators and volumes independently:
	/// <br/><br/>
	/// AddInitiatorsToVolumeAccessGroup<br/>
	/// RemoveInitiatorsFromVolumeAccessGroup<br/>
	/// AddVolumesToVolumeAccessGroup<br/>
	/// RemoveVolumesFromVolumeAccessGroup<br/>
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFVolumeAccessGroup")]
	public class ModifyVolumeAccessGroup : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the volume access group to modify.
		/// </summary>
		private Int64 _volumeAccessGroupID;
		/// <summary>
		/// The ID of the SolidFire Virtual Network ID to associate the volume access group with.
		/// </summary>
		private Int64[] _virtualNetworkID;
		/// <summary>
		/// The ID of the VLAN Virtual Network Tag to associate the volume access group with.
		/// </summary>
		private Int64[] _virtualNetworkTags;
		/// <summary>
		/// Name of the volume access group.
		/// It is not required to be unique, but recommended.
		/// </summary>
		private string _name;
		/// <summary>
		/// List of initiators to include in the volume access group.
		/// If unspecified, the access group's configured initiators will not be modified.
		/// </summary>
		private string[] _initiators;
		/// <summary>
		/// List of volumes to initially include in the volume access group.
		/// If unspecified, the access group's volumes will not be modified.
		/// </summary>
		private Int64[] _volumes;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the volume access group to modify.")]
		public Int64 VolumeAccessGroupID
		{
			get
			{
				return _volumeAccessGroupID;
			}
			set
			{
				_volumeAccessGroupID = value;
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
		[Parameter(Mandatory = false, HelpMessage = "[u'Name of the volume access group.', u'It is not required to be unique, but recommended.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'List of initiators to include in the volume access group.', u\"If unspecified, the access group's configured initiators will not be modified.\"]")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'List of volumes to initially include in the volume access group.', u\"If unspecified, the access group's volumes will not be modified.\"]")]
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
			var request = new ModifyVolumeAccessGroupRequest();
			request.VolumeAccessGroupID = _volumeAccessGroupID;
			request.VirtualNetworkID = _virtualNetworkID;
			request.VirtualNetworkTags = _virtualNetworkTags;
			request.Name = _name;
			request.Initiators = _initiators;
			request.Volumes = _volumes;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<ModifyVolumeAccessGroupResult>("ModifyVolumeAccessGroup", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		