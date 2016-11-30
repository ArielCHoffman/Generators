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
	/// You can use UpdateBulkVolumeStatus in a script to return to the SolidFire system the status of a bulk volume job that you have started with the "StartBulkVolumeRead" or "StartBulkVolumeWrite" methods.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Update, "SFBulkVolumeStatus")]
	public class UpdateBulkVolumeStatus : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The key assigned during initialization of a "StartBulkVolumeRead" or "StartBulkVolumeWrite" session.
		/// </summary>
		private string _key;
		/// <summary>
		/// The SolidFire system sets the status of the given bulk volume job.<br/>
		/// Possible values:<br/>
		/// <br/><b>running</b>: jobs that are still active.
		/// <br/><b>complete</b>: jobs that are done. failed - jobs that have failed.
		/// <br/><b>failed</b>: jobs that have failed.
		/// </summary>
		private string _status;
		/// <summary>
		/// The completed progress of the bulk volume job as a percentage.
		/// </summary>
		private string _percentComplete;
		/// <summary>
		/// Returns the status of the bulk volume job when the job has completed.
		/// </summary>
		private string _message;
		/// <summary>
		/// JSON attributes  updates what is on the bulk volume job.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The key assigned during initialization of a \"StartBulkVolumeRead\" or \"StartBulkVolumeWrite\" session.")]
		public string Key
		{
			get
			{
				return _key;
			}
			set
			{
				_key = value;
			}
		}
		[Parameter(HelpMessage = "[u'The SolidFire system sets the status of the given bulk volume job.<br/>', u'Possible values:<br/>', u'<br/><b>running</b>: jobs that are still active.', u'<br/><b>complete</b>: jobs that are done. failed - jobs that have failed.', u'<br/><b>failed</b>: jobs that have failed.']")]
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'The completed progress of the bulk volume job as a percentage.']")]
		public string PercentComplete
		{
			get
			{
				return _percentComplete;
			}
			set
			{
				_percentComplete = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Returns the status of the bulk volume job when the job has completed.']")]
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'JSON attributes  updates what is on the bulk volume job.']")]
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
			var request = new UpdateBulkVolumeStatusRequest();
			request.Key = _key;
			request.Status = _status;
			request.PercentComplete = _percentComplete;
			request.Message = _message;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<UpdateBulkVolumeStatusResult>("UpdateBulkVolumeStatus", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		