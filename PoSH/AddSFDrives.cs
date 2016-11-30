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
	/// AddDrives is used to add one or more available drives to the cluster enabling the drives to host a portion of the cluster's data.
	/// When you add a node to the cluster or install new drives in an existing node, the new drives are marked as "available" and must be added via AddDrives before they can be utilized.
	/// Use the "ListDrives" method to display drives that are "available" to be added.
	/// When you add multiple drives, it is more efficient to add them in a single "AddDrives" method call rather than multiple individual methods with a single drive each.
	/// This reduces the amount of data balancing that must occur to stabilize the storage load on the cluster.
	/// <br/><br/>
	/// When you add a drive, the system automatically determines the "type" of drive it should be.
	/// <br/><br/>
	/// The method returns immediately. However, it may take some time for the data in the cluster to be rebalanced using the newly added drives.
	/// As the new drive(s) are syncing on the system, you can use the "ListSyncJobs" method to see how the drive(s) are being rebalanced and the progress of adding the new drive.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Add, "SFDrives")]
	public class AddDrives : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of drives to add to the cluster.
		/// </summary>
		private NewDrive[] _drives;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "List of drives to add to the cluster.")]
		public NewDrive[] Drives
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
			var request = new AddDrivesRequest();
			request.Drives = _drives;
			var objsFromAPI = SendRequest<AddDrivesResult>("AddDrives", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		