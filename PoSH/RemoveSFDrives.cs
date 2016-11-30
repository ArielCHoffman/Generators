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
	/// You can use RemoveDrives to proactively remove drives that are part of the cluster.
	/// You may want to use this method when reducing cluster capacity or preparing to replace drives nearing the end of their service life.
	/// Any data on the drives is removed and migrated to other drives in the cluster before the drive is removed from the cluster. This is an asynchronous method.
	/// Depending on the total capacity of the drives being removed, it may take several minutes to migrate all of the data.
	/// Use the "GetAsyncResult" method to check the status of the remove operation.
	/// <br/><br/>
	/// When removing multiple drives, use a single "RemoveDrives" method call rather than multiple individual methods with a single drive each.
	/// This reduces the amount of data balancing that must occur to even stabilize the storage load on the cluster.
	/// <br/><br/>
	/// You can also remove drives with a "failed" status using "RemoveDrives".
	/// When you remove a drive with a "failed" status it is not returned to an "available" or "active" status.
	/// The drive is unavailable for use in the cluster.
	/// <br/><br/>
	/// Use the "ListDrives" method to obtain the driveIDs for the drives you want to remove.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFDrives")]
	public class RemoveDrives : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of driveIDs to remove from the cluster.
		/// </summary>
		private Int64[] _drives;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "List of driveIDs to remove from the cluster.")]
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
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new RemoveDrivesRequest();
			request.Drives = _drives;
			var objsFromAPI = SendRequest<RemoveDrivesResult>("RemoveDrives", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		