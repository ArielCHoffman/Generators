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
	/// ModifyGroupSnapshot is used to change the attributes currently assigned to a group snapshot.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFGroupSnapshot")]
	public class ModifyGroupSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// ID of the snapshot.
		/// </summary>
		private Int64 _groupSnapshotID;
		/// <summary>
		/// Use to set the time when the snapshot should be removed.
		/// </summary>
		private string _expirationTime;
		/// <summary>
		/// Use to enable the snapshot created to be replicated to a remote SolidFire cluster.
		/// Possible values:
		/// <br/><b>true</b>: the snapshot will be replicated to remote storage.
		/// <br/><b>false</b>: Default. No replication.
		/// </summary>
		private boolean _enableRemoteReplication;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "ID of the snapshot.")]
		public Int64 GroupSnapshotID
		{
			get
			{
				return _groupSnapshotID;
			}
			set
			{
				_groupSnapshotID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Use to set the time when the snapshot should be removed.")]
		public string ExpirationTime
		{
			get
			{
				return _expirationTime;
			}
			set
			{
				_expirationTime = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Use to enable the snapshot created to be replicated to a remote SolidFire cluster.', u'Possible values:', u'<br/><b>true</b>: the snapshot will be replicated to remote storage.', u'<br/><b>false</b>: Default. No replication.']")]
		public boolean EnableRemoteReplication
		{
			get
			{
				return _enableRemoteReplication;
			}
			set
			{
				_enableRemoteReplication = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(8);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ModifyGroupSnapshotRequest();
			request.GroupSnapshotID = _groupSnapshotID;
			request.ExpirationTime = _expirationTime;
			request.EnableRemoteReplication = _enableRemoteReplication;
			var objsFromAPI = SendRequest<ModifyGroupSnapshotResult>("ModifyGroupSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		