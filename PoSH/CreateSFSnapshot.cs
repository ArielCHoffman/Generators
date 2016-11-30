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
	/// CreateSnapshot is used to create a point-in-time copy of a volume.
	/// A snapshot can be created from any volume or from an existing snapshot.
	/// <br/><br/>
	/// <b>Note</b>: Creating a snapshot is allowed if cluster fullness is at stage 2 or 3.
	/// Snapshots are not created when cluster fullness is at stage 4 or 5.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFSnapshot")]
	public class CreateSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// ID of the volume image from which to copy.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// Unique ID of a snapshot from which the new snapshot is made.
		/// The snapshotID passed must be a snapshot on the given volume.
		/// If a SnapshotID is not provided, a snapshot is created from the volume's active branch.
		/// </summary>
		private Int64 _snapshotID;
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
		[Parameter(HelpMessage = "ID of the volume image from which to copy.")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'Unique ID of a snapshot from which the new snapshot is made.', u'The snapshotID passed must be a snapshot on the given volume.', u\"If a SnapshotID is not provided, a snapshot is created from the volume's active branch.\"]")]
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
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new CreateSnapshotRequest();
			request.VolumeID = _volumeID;
			request.SnapshotID = _snapshotID;
			request.Name = _name;
			request.EnableRemoteReplication = _enableRemoteReplication;
			request.Retention = _retention;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<CreateSnapshotResult>("CreateSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		