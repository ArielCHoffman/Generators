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
	/// Delete a volume access group from the system.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Delete, "SFVolumeAccessGroup")]
	public class DeleteVolumeAccessGroup : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the volume access group to delete.
		/// </summary>
		private Int64 _volumeAccessGroupID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'The ID of the volume access group to delete.']")]
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
			var request = new DeleteVolumeAccessGroupRequest();
			request.VolumeAccessGroupID = _volumeAccessGroupID;
			var objsFromAPI = SendRequest<DeleteVolumeAccessGroupResult>("DeleteVolumeAccessGroup", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		