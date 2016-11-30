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
	/// ListDriveStats enables you to retrieve  high-level activity measurements for multiple drives in the cluster. By default, this method returns statistics for all drives in the cluster, and these measurements are cumulative from the addition of the drive to the cluster. Some values this method returns are specific to block drives, and some are specific to metadata drives. For more information on what data each drive type returns, see the response examples for the GetDriveStats method.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFDriveStats")]
	public class ListDriveStats : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Optional list of DriveIDs for which to return drive statistics. If you omit this parameter, measurements for all drives are returned.
		/// </summary>
		private Int64[] _drives;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Optional list of DriveIDs for which to return drive statistics. If you omit this parameter, measurements for all drives are returned.")]
		public Int64[] Drives
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
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(9);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ListDriveStatsRequest();
			request.Drives = _drives;
			var objsFromAPI = SendRequest<ListDriveStatsResult>("ListDriveStats", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		