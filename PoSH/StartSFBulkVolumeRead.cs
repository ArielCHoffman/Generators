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
	/// StartBulkVolumeRead allows you to initialize a bulk volume read session on a specified volume.
	/// Only two bulk volume processes can run simultaneously on a volume.
	/// When you initialize the session, data is read from a SolidFire storage volume for the purposes of storing the data on an external backup source.
	/// The external data is accessed by a web server running on a SolidFire node.
	/// Communications and server interaction information for external data access is passed by a script running on the SolidFire storage system.<br/>
	/// <br/>
	/// At the start of a bulk volume read operation, a snapshot of the volume is made and the snapshot is deleted when the read has completed.
	/// You can also read a snapshot of the volume by entering the ID of the snapshot as a parameter.
	/// Reading a previous snapshot does not create a new snapshot of the volume, nor does the previous snapshot be deleted when the read completes.<br/>
	/// <br/>
	/// <b>Note</b>: This process creates a new snapshot if the ID of an existing snapshot is not provided.
	/// Snapshots can be created if cluster fullness is at stage 2 or 3.
	/// Snapshots are not created when cluster fullness is at stage 4 or 5.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Start, "SFBulkVolumeRead")]
	public class StartBulkVolumeRead : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// ID of the volume to be read.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// The format of the volume data. Can be either:
		/// <br/><b>uncompressed</b>: every byte of the volume is returned without any compression.
		/// <br/><b>native</b>: opaque data is returned that is smaller and more efficiently stored and written on a subsequent bulk volume write.
		/// </summary>
		private string _format;
		/// <summary>
		/// ID of a previously created snapshot used for bulk volume reads.
		/// If no ID is entered, a snapshot of the current active volume image is made.
		/// </summary>
		private Int64 _snapshotID;
		/// <summary>
		/// Executable name of a script.
		/// If no script name is given then the key and URL is necessary to access SolidFire nodes.
		/// The script is run on the primary node and the key and URL is returned to the script so the local web server can be contacted.
		/// </summary>
		private string _script;
		/// <summary>
		/// JSON parameters to pass to the script.
		/// </summary>
		private Object _scriptParameters;
		/// <summary>
		/// JSON attributes for the bulk volume job.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "ID of the volume to be read.")]
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
		[Parameter(HelpMessage = "[u'The format of the volume data. Can be either:', u'<br/><b>uncompressed</b>: every byte of the volume is returned without any compression.', u'<br/><b>native</b>: opaque data is returned that is smaller and more efficiently stored and written on a subsequent bulk volume write.']")]
		public string Format
		{
			get
			{
				return _format;
			}
			set
			{
				_format = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'ID of a previously created snapshot used for bulk volume reads.', u'If no ID is entered, a snapshot of the current active volume image is made.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'Executable name of a script.', u'If no script name is given then the key and URL is necessary to access SolidFire nodes.', u'The script is run on the primary node and the key and URL is returned to the script so the local web server can be contacted.']")]
		public string Script
		{
			get
			{
				return _script;
			}
			set
			{
				_script = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'JSON parameters to pass to the script.']")]
		public Object ScriptParameters
		{
			get
			{
				return _scriptParameters;
			}
			set
			{
				_scriptParameters = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'JSON attributes for the bulk volume job.']")]
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
			var request = new StartBulkVolumeReadRequest();
			request.VolumeID = _volumeID;
			request.Format = _format;
			request.SnapshotID = _snapshotID;
			request.Script = _script;
			request.ScriptParameters = _scriptParameters;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<StartBulkVolumeReadResult>("StartBulkVolumeRead", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		