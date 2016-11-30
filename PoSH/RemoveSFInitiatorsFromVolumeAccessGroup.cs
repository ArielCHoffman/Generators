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
	/// Remove initiators from a volume access group.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFInitiatorsFromVolumeAccessGroup")]
	public class RemoveInitiatorsFromVolumeAccessGroup : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the volume access group to modify.
		/// </summary>
		private Int64 _volumeAccessGroupID;
		/// <summary>
		/// List of initiators to remove from the volume access group.
		/// </summary>
		private string[] _initiators;
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
		[Parameter(HelpMessage = "[u'List of initiators to remove from the volume access group.']")]
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
			var request = new RemoveInitiatorsFromVolumeAccessGroupRequest();
			request.VolumeAccessGroupID = _volumeAccessGroupID;
			request.Initiators = _initiators;
			var objsFromAPI = SendRequest<RemoveInitiatorsFromVolumeAccessGroupResult>("RemoveInitiatorsFromVolumeAccessGroup", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		