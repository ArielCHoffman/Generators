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
	/// CloneVolume is used to create a copy of the volume.
	/// This method is asynchronous and may take a variable amount of time to complete.
	/// The cloning process begins immediately when the CloneVolume request is made and is representative of the state of the volume when the API method is issued.
	/// GetAsyncResults can be used to determine when the cloning process is complete and the new volume is available for connections.
	/// ListSyncJobs can be used to see the progress of creating the clone.
	/// <br/><br/>
	/// <b>Note</b>: The initial attributes and quality of service settings for the volume are inherited from the volume being cloned.
	/// If different settings are required, they can be changed via ModifyVolume.
	/// <br/><br/>
	/// <b>Note</b>: Cloned volumes do not inherit volume access group memberships from the source volume.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Clone, "SFVolume")]
	public class CloneVolume : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the volume to clone.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// The name for the newly-created volume.
		/// </summary>
		private string _name;
		/// <summary>
		/// AccountID for the owner of the new volume.
		/// If unspecified, the AccountID of the owner of the volume being cloned is used.
		/// </summary>
		private Int64 _newAccountID;
		/// <summary>
		/// New size of the volume, in bytes.
		/// May be greater or less than the size of the volume being cloned.
		/// If unspecified, the clone's volume size will be the same as the source volume.
		/// Size is rounded up to the nearest 1 MiB.
		/// </summary>
		private Int64 _newSize;
		/// <summary>
		/// Access settings for the new volume.
		/// <br/><b>readOnly</b>: Only read operations are allowed.
		/// <br/><b>readWrite</b>: Reads and writes are allowed.
		/// <br/><b>locked</b>: No reads or writes are allowed.
		/// <br/><b>replicationTarget</b>: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.
		/// <br/><br/>
		/// If unspecified, the access settings of the clone will be the same as the source.
		/// </summary>
		private string _access;
		/// <summary>
		/// ID of the snapshot to use as the source of the clone.
		/// If unspecified, the clone will be created with a snapshot of the active volume.
		/// </summary>
		private Int64 _snapshotID;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the volume to clone.")]
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
		[Parameter(HelpMessage = "The name for the newly-created volume.")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'AccountID for the owner of the new volume.', u'If unspecified, the AccountID of the owner of the volume being cloned is used.']")]
		public Int64 NewAccountID
		{
			get
			{
				return _newAccountID;
			}
			set
			{
				_newAccountID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'New size of the volume, in bytes.', u'May be greater or less than the size of the volume being cloned.', u\"If unspecified, the clone's volume size will be the same as the source volume.\", u'Size is rounded up to the nearest 1 MiB.']")]
		public Int64 NewSize
		{
			get
			{
				return _newSize;
			}
			set
			{
				_newSize = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Access settings for the new volume.', u'<br/><b>readOnly</b>: Only read operations are allowed.', u'<br/><b>readWrite</b>: Reads and writes are allowed.', u'<br/><b>locked</b>: No reads or writes are allowed.', u'<br/><b>replicationTarget</b>: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.', u'<br/><br/>', u'If unspecified, the access settings of the clone will be the same as the source.']")]
		public string Access
		{
			get
			{
				return _access;
			}
			set
			{
				_access = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'ID of the snapshot to use as the source of the clone.', u'If unspecified, the clone will be created with a snapshot of the active volume.']")]
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
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new CloneVolumeRequest();
			request.VolumeID = _volumeID;
			request.Name = _name;
			request.NewAccountID = _newAccountID;
			request.NewSize = _newSize;
			request.Access = _access;
			request.SnapshotID = _snapshotID;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<CloneVolumeResult>("CloneVolume", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		