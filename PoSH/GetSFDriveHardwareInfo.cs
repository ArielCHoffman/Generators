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
	/// GetDriveHardwareInfo returns all the hardware info for the given drive. This generally includes manufacturers, vendors, versions, and other associated hardware identification information.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFDriveHardwareInfo")]
	public class GetDriveHardwareInfo : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// DriveID for the drive information requested. DriveIDs can be obtained via the "ListDrives" method.
		/// </summary>
		private Int64 _driveID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "DriveID for the drive information requested. DriveIDs can be obtained via the \"ListDrives\" method.")]
		public Int64 DriveID
		{
			get
			{
				return _driveID;
			}
			set
			{
				_driveID = value;
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
			var request = new GetDriveHardwareInfoRequest();
			request.DriveID = _driveID;
			var objsFromAPI = SendRequest<GetDriveHardwareInfoResult>("GetDriveHardwareInfo", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		