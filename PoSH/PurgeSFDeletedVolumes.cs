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
	/// PurgeDeletedVolumes immediately and permanently purges volumes that have been deleted; you can use this method to purge up to 500 volumes at one time. You must delete volumes using DeleteVolumes before they can be purged. Volumes are purged by the system automatically after a period of time, so usage of this method is not typically required.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Purge, "SFDeletedVolumes")]
	public class PurgeDeletedVolumes : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A list of volumeIDs of volumes to be purged from the system.
		/// </summary>
		private Int64[] _volumeIDs;
		/// <summary>
		/// A list of accountIDs. All of the volumes from all of the specified accounts are purged from the system.
		/// </summary>
		private Int64[] _accountIDs;
		/// <summary>
		/// A list of volumeAccessGroupIDs. All of the volumes from all of the specified Volume Access Groups are purged from the system.
		/// </summary>
		private Int64[] _volumeAccessGroupIDs;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "A list of volumeIDs of volumes to be purged from the system.")]
		public Int64[] VolumeIDs
		{
			get
			{
				return _volumeIDs;
			}
			set
			{
				_volumeIDs = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "A list of accountIDs. All of the volumes from all of the specified accounts are purged from the system.")]
		public Int64[] AccountIDs
		{
			get
			{
				return _accountIDs;
			}
			set
			{
				_accountIDs = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "A list of volumeAccessGroupIDs. All of the volumes from all of the specified Volume Access Groups are purged from the system.")]
		public Int64[] VolumeAccessGroupIDs
		{
			get
			{
				return _volumeAccessGroupIDs;
			}
			set
			{
				_volumeAccessGroupIDs = value;
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
			var request = new PurgeDeletedVolumesRequest();
			request.VolumeIDs = _volumeIDs;
			request.AccountIDs = _accountIDs;
			request.VolumeAccessGroupIDs = _volumeAccessGroupIDs;
			var objsFromAPI = SendRequest<PurgeDeletedVolumesResult>("PurgeDeletedVolumes", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		