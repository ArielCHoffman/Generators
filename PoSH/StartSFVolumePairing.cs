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
	/// StartVolumePairing is used to create an encoded key from a volume that is used to pair with another volume.
	/// The key that this method creates is used in the "CompleteVolumePairing" API method to establish a volume pairing.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Start, "SFVolumePairing")]
	public class StartVolumePairing : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the volume on which to start the pairing process.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// The mode of the volume on which to start the pairing process. The mode can only be set if the volume is the source volume.<br/>
		/// Possible values:<br/>
		/// <b>Async</b>: (default if no mode parameter specified) Writes are acknowledged when they complete locally. The cluster does not wait for writes to be replicated to the target cluster.<br/>
		/// <b>Sync</b>: Source acknowledges write when the data is stored locally and on the remote cluster.<br/>
		/// <b>SnapshotsOnly</b>: Only snapshots created on the source cluster will be replicated. Active writes from the source volume will not be replicated.<br/>
		/// </summary>
		private string _mode;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the volume on which to start the pairing process.")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'The mode of the volume on which to start the pairing process. The mode can only be set if the volume is the source volume.<br/>', u'Possible values:<br/>', u'<b>Async</b>: (default if no mode parameter specified) Writes are acknowledged when they complete locally. The cluster does not wait for writes to be replicated to the target cluster.<br/>', u'<b>Sync</b>: Source acknowledges write when the data is stored locally and on the remote cluster.<br/>', u'<b>SnapshotsOnly</b>: Only snapshots created on the source cluster will be replicated. Active writes from the source volume will not be replicated.<br/>']")]
		public string Mode
		{
			get
			{
				return _mode;
			}
			set
			{
				_mode = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new StartVolumePairingRequest();
			request.VolumeID = _volumeID;
			request.Mode = _mode;
			var objsFromAPI = SendRequest<StartVolumePairingResult>("StartVolumePairing", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		