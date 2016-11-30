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
	/// RollbackToGroupSnapshot is used to roll back each individual volume in a snapshot group to a copy of their individual snapshots.
	/// <br/><br/>
	/// <b>Note</b>: Creating a snapshot is allowed if cluster fullness is at stage 2 or 3.
	/// Snapshots are not created when cluster fullness is at stage 4 or 5.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Rollback, "SFToGroupSnapshot")]
	public class RollbackToGroupSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique ID of the group snapshot.
		/// </summary>
		private Int64 _groupSnapshotID;
		/// <summary>
		/// <br/><b>true</b>: The previous active volume image is kept.
		/// <br/><b>false</b>: (default) The previous active volume image is deleted.
		/// </summary>
		private boolean _saveCurrentState;
		/// <summary>
		/// Name for the snapshot. If no name is given, then the name of the snapshot being rolled back to is used with 
		/// "-copy" appended to the end of the name.
		/// </summary>
		private string _name;
		/// <summary>
		/// List of Name/Value pairs in JSON object format
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Unique ID of the group snapshot.']")]
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
		[Parameter(HelpMessage = "[u'<br/><b>true</b>: The previous active volume image is kept.', u'<br/><b>false</b>: (default) The previous active volume image is deleted.']")]
		public boolean SaveCurrentState
		{
			get
			{
				return _saveCurrentState;
			}
			set
			{
				_saveCurrentState = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Name for the snapshot. If no name is given, then the name of the snapshot being rolled back to is used with ', u'\"-copy\" appended to the end of the name.']")]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'List of Name/Value pairs in JSON object format']")]
		public hashtable Attributes
		{
			get
			{
				return _attributes;
			}
			set
			{
				_attributes = value;
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
			var request = new RollbackToGroupSnapshotRequest();
			request.GroupSnapshotID = _groupSnapshotID;
			request.SaveCurrentState = _saveCurrentState;
			request.Name = _name;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<RollbackToGroupSnapshotResult>("RollbackToGroupSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		