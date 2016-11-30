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
	/// PurgeDeletedVolume immediately and permanently purges a volume which has been deleted.
	/// A volume must be deleted using DeleteVolume before it can be purged.
	/// Volumes are purged automatically after a period of time, so usage of this method is not typically required.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Purge, "SFDeletedVolume")]
	public class PurgeDeletedVolume : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the volume to purge.
		/// </summary>
		private Int64 _volumeID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the volume to purge.")]
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
			var request = new PurgeDeletedVolumeRequest();
			request.VolumeID = _volumeID;
			var objsFromAPI = SendRequest<PurgeDeletedVolumeResult>("PurgeDeletedVolume", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		