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
	/// ResetDrives is used to pro-actively initialize drives and remove all data currently residing on the drive. The drive can then be reused in an existing node or used in an upgraded SolidFire node. This method requires the force=true parameter to be included in the method call.
	/// <br/><br/>
	/// <b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Reset, "SFDrives")]
	public class ResetDrives : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of device names (not driveIDs) to reset.
		/// </summary>
		private string _drives;
		/// <summary>
		/// The "force" parameter must be included on this method to successfully reset a drive.
		/// </summary>
		private boolean _force;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "List of device names (not driveIDs) to reset.")]
		public string Drives
		{
			get
			{
				return _drives;
			}
			set
			{
				_drives = value;
			}
		}
		[Parameter(HelpMessage = "The \"force\" parameter must be included on this method to successfully reset a drive.")]
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
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ResetDrivesRequest();
			request.Drives = _drives;
			request.Force = _force;
			var objsFromAPI = SendRequest<ResetDrivesResult>("ResetDrives", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		