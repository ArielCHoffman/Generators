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
	/// StartBulkVolumeWrite allows you to initialize a bulk volume write session on a specified volume.
	/// Only two bulk volume processes can run simultaneously on a volume.
	/// When the session is initialized, data can be written to a SolidFire storage volume from an external backup source.
	/// The external data is accessed by a web server running on a SolidFire node.
	/// Communications and server interaction information for external data access is passed by a script running on the SolidFire storage system.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Start, "SFBulkVolumeWrite")]
	public class StartBulkVolumeWrite : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// ID of the volume to be written to.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// The format of the volume data. Can be either:
		/// <br/><b>uncompressed</b>: every byte of the volume is returned without any compression.
		/// <br/><b>native</b>: opaque data is returned that is smaller and more efficiently stored and written on a subsequent bulk volume write
		/// </summary>
		private string _format;
		/// <summary>
		/// Executable name of a script.
		/// If no script name is given then the key and URL are necessary to access SolidFire nodes.
		/// The script runs on the primary node and the key and URL is returned to the script so the local web server can be contacted.
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
		[Parameter(HelpMessage = "ID of the volume to be written to.")]
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
		[Parameter(HelpMessage = "[u'The format of the volume data. Can be either:', u'<br/><b>uncompressed</b>: every byte of the volume is returned without any compression.', u'<br/><b>native</b>: opaque data is returned that is smaller and more efficiently stored and written on a subsequent bulk volume write']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'Executable name of a script.', u'If no script name is given then the key and URL are necessary to access SolidFire nodes.', u'The script runs on the primary node and the key and URL is returned to the script so the local web server can be contacted.']")]
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
			var request = new StartBulkVolumeWriteRequest();
			request.VolumeID = _volumeID;
			request.Format = _format;
			request.Script = _script;
			request.ScriptParameters = _scriptParameters;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<StartBulkVolumeWriteResult>("StartBulkVolumeWrite", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		