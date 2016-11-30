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
	/// The TestDrives API method is used to run a hardware validation on all the drives on the node. Hardware failures on the drives are detected if present and they are reported in the results of the validation tests.
	/// <br/><br/>
	/// <b>Note</b>: This test takes approximately 10 minutes.
	/// <br/><br/>
	/// <b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Test, "SFDrives")]
	public class TestDrives : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The number of minutes to run the test can be specified.
		/// </summary>
		private Int64 _minutes;
		/// <summary>
		/// The "force" parameter must be included on this method to successfully test the drives on the node.
		/// </summary>
		private boolean _force;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "The number of minutes to run the test can be specified.")]
		public Int64 Minutes
		{
			get
			{
				return _minutes;
			}
			set
			{
				_minutes = value;
			}
		}
		[Parameter(HelpMessage = "The \"force\" parameter must be included on this method to successfully test the drives on the node.")]
		public boolean Force
		{
			get
			{
				return _force;
			}
			set
			{
				_force = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(5);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new TestDrivesRequest();
			request.Minutes = _minutes;
			request.Force = _force;
			var objsFromAPI = SendRequest<TestDrivesResult>("TestDrives", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		