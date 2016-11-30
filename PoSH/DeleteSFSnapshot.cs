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
	/// DeleteSnapshot is used to delete a snapshot.
	/// A snapshot that is currently the "active" snapshot cannot be deleted.
	/// You must rollback and make another snapshot "active" before the current snapshot can be deleted.
	/// To rollback a snapshot, use RollbackToSnapshot.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Delete, "SFSnapshot")]
	public class DeleteSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the snapshot to delete.
		/// </summary>
		private Int64 _snapshotID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the snapshot to delete.")]
		public Int64 SnapshotID
		{
			get
			{
				return _snapshotID;
			}
			set
			{
				_snapshotID = value;
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
			var request = new DeleteSnapshotRequest();
			request.SnapshotID = _snapshotID;
			var objsFromAPI = SendRequest<DeleteSnapshotResult>("DeleteSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		