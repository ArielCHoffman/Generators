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
	/// DeleteGroupSnapshot is used to delete a group snapshot.
	/// The saveMembers parameter can be used to preserve all the snapshots that
	/// were made for the volumes in the group but the group association will be removed.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Delete, "SFGroupSnapshot")]
	public class DeleteGroupSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique ID of the group snapshot.
		/// </summary>
		private Int64 _groupSnapshotID;
		/// <summary>
		/// <br/><b>true</b>: Snapshots are kept, but group association is removed.
		/// <br/><b>false</b>: The group and snapshots are deleted.
		/// </summary>
		private boolean _saveMembers;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Unique ID of the group snapshot.")]
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
		[Parameter(HelpMessage = "[u'<br/><b>true</b>: Snapshots are kept, but group association is removed.', u'<br/><b>false</b>: The group and snapshots are deleted.']")]
		public boolean SaveMembers
		{
			get
			{
				return _saveMembers;
			}
			set
			{
				_saveMembers = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(7);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new DeleteGroupSnapshotRequest();
			request.GroupSnapshotID = _groupSnapshotID;
			request.SaveMembers = _saveMembers;
			var objsFromAPI = SendRequest<DeleteGroupSnapshotResult>("DeleteGroupSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		