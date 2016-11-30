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
	/// ModifyVolumePair is used to pause or restart replication between a pair of volumes.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFVolumePair")]
	public class ModifyVolumePair : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Identification number of the volume to be modified.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// Valid values that can be entered:<br/>
		/// <b>true</b>: to pause volume replication.<br/>
		/// <b>false</b>: to restart volume replication.<br/>
		/// If no value is specified, no change in replication is performed.
		/// </summary>
		private boolean _pausedManual;
		/// <summary>
		/// Volume replication mode.<br/>
		/// Possible values:<br/>
		/// <b>Async</b>: Writes are acknowledged when they complete locally. The cluster does not wait for writes to be replicated to the target cluster.<br/>
		/// <b>Sync</b>: The source acknowledges the write when the data is stored locally and on the remote cluster.<br/>
		/// <b>SnapshotsOnly</b>: Only snapshots created on the source cluster will be replicated. Active writes from the source volume are not replicated.<br/>
		/// </summary>
		private string _mode;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Identification number of the volume to be modified.")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'Valid values that can be entered:<br/>', u'<b>true</b>: to pause volume replication.<br/>', u'<b>false</b>: to restart volume replication.<br/>', u'If no value is specified, no change in replication is performed.']")]
		public boolean PausedManual
		{
			get
			{
				return _pausedManual;
			}
			set
			{
				_pausedManual = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Volume replication mode.<br/>', u'Possible values:<br/>', u'<b>Async</b>: Writes are acknowledged when they complete locally. The cluster does not wait for writes to be replicated to the target cluster.<br/>', u'<b>Sync</b>: The source acknowledges the write when the data is stored locally and on the remote cluster.<br/>', u'<b>SnapshotsOnly</b>: Only snapshots created on the source cluster will be replicated. Active writes from the source volume are not replicated.<br/>']")]
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
			var request = new ModifyVolumePairRequest();
			request.VolumeID = _volumeID;
			request.PausedManual = _pausedManual;
			request.Mode = _mode;
			var objsFromAPI = SendRequest<ModifyVolumePairResult>("ModifyVolumePair", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		