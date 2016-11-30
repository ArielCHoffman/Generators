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
	/// CreateGroupSnapshot is used to create a point-in-time copy of a group of volumes.
	/// The snapshot created can then be used later as a backup or rollback to ensure the data on the group of volumes is consistent for the point in time in which the snapshot was created.
	/// <br/><br/>
	/// <b>Note</b>: Creating a group snapshot is allowed if cluster fullness is at stage 2 or 3.
	/// Snapshots are not created when cluster fullness is at stage 4 or 5.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFGroupSnapshot")]
	public class CreateGroupSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique ID of the volume image from which to copy.
		/// </summary>
		private Int64[] _volumes;
		/// <summary>
		/// A name for the snapshot.
		/// If no name is provided, the date and time the snapshot was taken is used.
		/// </summary>
		private string _name;
		/// <summary>
		/// Identifies if snapshot is enabled for remote replication.
		/// </summary>
		private boolean _enableRemoteReplication;
		/// <summary>
		/// The amount of time the snapshot will be retained. Enter in HH:mm:ss
		/// </summary>
		private string _retention;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Unique ID of the volume image from which to copy.")]
		public Int64[] Volumes
		{
			get
			{
				return _volumes;
			}
			set
			{
				_volumes = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'A name for the snapshot.', u'If no name is provided, the date and time the snapshot was taken is used.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "Identifies if snapshot is enabled for remote replication.")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'The amount of time the snapshot will be retained. Enter in HH:mm:ss']")]
		public string Retention
		{
			get
			{
				return _retention;
			}
			set
			{
				_retention = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "List of Name/Value pairs in JSON object format.")]
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
			var request = new CreateGroupSnapshotRequest();
			request.Volumes = _volumes;
			request.Name = _name;
			request.EnableRemoteReplication = _enableRemoteReplication;
			request.Retention = _retention;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<CreateGroupSnapshotResult>("CreateGroupSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		