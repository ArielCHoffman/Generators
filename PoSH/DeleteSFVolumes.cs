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
	/// DeleteVolumes marks multiple (up to 500) active volumes for deletion. Once marked, the volumes are purged (permanently deleted) after the cleanup interval elapses.The cleanup interval can be set in the SetClusterSettings method. For more information on using this method, see SetClusterSettings on page 1. After making a request to delete volumes, any active iSCSI connections to the volumes are immediately terminated and no further connections are allowed while the volumes are in this state. A marked volume is not returned in target discovery requests. Any snapshots of a volume that has been marked for deletion are not affected. Snapshots are kept until the volume is purged from the system. If a volume is marked for deletion and has a bulk volume read or bulk volume write operation in progress, the bulk volume read or write operation is stopped. If the volumes you delete are paired with a volume, replication between the paired volumes is suspended and no data is transferred to them or from them while in a deleted state. The remote volumes the deleted volumes were paired with enter into a PausedMisconfigured state and data is no longer sent to them or from the deleted volumes. Until the deleted volumes are purged, they can be restored and data transfers resume. If the deleted volumes are purged from the system, the volumes they were paired with enter into a StoppedMisconfigured state and the volume pairing status is removed. The purged volumes become permanently unavailable.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Delete, "SFVolumes")]
	public class DeleteVolumes : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A list of account IDs. All volumes from these accounts are deleted from the system. 
		/// </summary>
		private Int64[] _accountIDs;
		/// <summary>
		/// A list of volume access group IDs. All of the volumes from all of the volume access groups you specify in this list are deleted from the system.
		/// </summary>
		private Int64[] _volumeAccessGroupIDs;
		/// <summary>
		/// The list of IDs of the volumes to delete from the system.
		/// </summary>
		private Int64[] _volumeIDs;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "A list of account IDs. All volumes from these accounts are deleted from the system. ")]
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
		[Parameter(Mandatory = false, HelpMessage = "A list of volume access group IDs. All of the volumes from all of the volume access groups you specify in this list are deleted from the system.")]
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
		[Parameter(Mandatory = false, HelpMessage = "The list of IDs of the volumes to delete from the system.")]
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
			var request = new DeleteVolumesRequest();
			request.AccountIDs = _accountIDs;
			request.VolumeAccessGroupIDs = _volumeAccessGroupIDs;
			request.VolumeIDs = _volumeIDs;
			var objsFromAPI = SendRequest<DeleteVolumesResult>("DeleteVolumes", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		