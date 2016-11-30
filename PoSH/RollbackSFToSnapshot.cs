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
	/// RollbackToSnapshot is used to make an existing snapshot the "active" volume image. This method creates a new 
	/// snapshot from an existing snapshot. The new snapshot becomes "active" and the existing snapshot is preserved until 
	/// it is manually deleted. The previously "active" snapshot is deleted unless the parameter saveCurrentState is set with 
	/// a value of "true."
	/// <b>Note</b>: Creating a snapshot is allowed if cluster fullness is at stage 2 or 3.
	/// Snapshots are not created when cluster fullness is at stage 4 or 5.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Rollback, "SFToSnapshot")]
	public class RollbackToSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// VolumeID for the volume.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// ID of a previously created snapshot on the given volume.
		/// </summary>
		private Int64 _snapshotID;
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
		[Parameter(HelpMessage = "[u'VolumeID for the volume.']")]
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
		[Parameter(HelpMessage = "[u'ID of a previously created snapshot on the given volume.']")]
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
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new RollbackToSnapshotRequest();
			request.VolumeID = _volumeID;
			request.SnapshotID = _snapshotID;
			request.SaveCurrentState = _saveCurrentState;
			request.Name = _name;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<RollbackToSnapshotResult>("RollbackToSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		