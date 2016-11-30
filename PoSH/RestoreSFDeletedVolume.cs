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
	/// RestoreDeletedVolume marks a deleted volume as active again.
	/// This action makes the volume immediately available for iSCSI connection.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Restore, "SFDeletedVolume")]
	public class RestoreDeletedVolume : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// VolumeID for the deleted volume to restore.
		/// </summary>
		private Int64 _volumeID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "VolumeID for the deleted volume to restore.")]
		public Int64 VolumeID
		{
			get
			{
				return _volumeID;
			}
			set
			{
				_volumeID = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new RestoreDeletedVolumeRequest();
			request.VolumeID = _volumeID;
			var objsFromAPI = SendRequest<RestoreDeletedVolumeResult>("RestoreDeletedVolume", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		