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
	/// CloneMultipleVolumes is used to create a clone of a group of specified volumes. A consistent set of characteristics can be assigned to a group of multiple volume when they are cloned together.
	/// If groupSnapshotID is going to be used to clone the volumes in a group snapshot, the group snapshot must be created first using the CreateGroupSnapshot API method or the SolidFire Element WebUI. Using groupSnapshotID is optional when cloning multiple volumes.
	/// <br/><br/>
	/// <b>Note</b>: Cloning multiple volumes is allowed if cluster fullness is at stage 2 or 3. Clones are not created when cluster fullness is at stage 4 or 5.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Clone, "SFMultipleVolumes")]
	public class CloneMultipleVolumes : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Array of Unique ID for each volume to include in the clone with optional parameters. If optional parameters are not specified, the values will be inherited from the source volumes.
		/// </summary>
		private CloneMultipleVolumeParams[] _volumes;
		/// <summary>
		/// New default access method for the new volumes if not overridden by information passed in the volumes array.
		/// <br/><b>readOnly</b>: Only read operations are allowed.
		/// <br/><b>readWrite</b>: Reads and writes are allowed.
		/// <br/><b>locked</b>: No reads or writes are allowed.
		/// <br/><b>replicationTarget</b>: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.
		/// <br/><br/>
		/// If unspecified, the access settings of the clone will be the same as the source.
		/// </summary>
		private string _access;
		/// <summary>
		/// ID of the group snapshot to use as a basis for the clone.
		/// </summary>
		private Int64 _groupSnapshotID;
		/// <summary>
		/// New account ID for the volumes if not overridden by information passed in the volumes array.
		/// </summary>
		private Int64 _newAccountID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Array of Unique ID for each volume to include in the clone with optional parameters. If optional parameters are not specified, the values will be inherited from the source volumes.")]
		public CloneMultipleVolumeParams[] Volumes
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
		[Parameter(Mandatory = false, HelpMessage = "[u'New default access method for the new volumes if not overridden by information passed in the volumes array.', u'<br/><b>readOnly</b>: Only read operations are allowed.', u'<br/><b>readWrite</b>: Reads and writes are allowed.', u'<br/><b>locked</b>: No reads or writes are allowed.', u'<br/><b>replicationTarget</b>: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.', u'<br/><br/>', u'If unspecified, the access settings of the clone will be the same as the source.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "ID of the group snapshot to use as a basis for the clone.")]
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
		[Parameter(Mandatory = false, HelpMessage = "New account ID for the volumes if not overridden by information passed in the volumes array.")]
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
			var request = new CloneMultipleVolumesRequest();
			request.Volumes = _volumes;
			request.Access = _access;
			request.GroupSnapshotID = _groupSnapshotID;
			request.NewAccountID = _newAccountID;
			var objsFromAPI = SendRequest<CloneMultipleVolumesResult>("CloneMultipleVolumes", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		