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
	/// PrepareVirtualSnapshot is used to set up VMware Virtual Volume snapshot.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Prepare, "SFVirtualSnapshot")]
	public class PrepareVirtualSnapshot : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the Virtual Volume to clone.
		/// </summary>
		private UUIDNullable _virtualVolumeID;
		/// <summary>
		/// The name for the newly-created volume.
		/// </summary>
		private string _name;
		/// <summary>
		/// Will the snapshot be writable?
		/// </summary>
		private boolean _writableSnapshot;
		private UUIDNullable _callingVirtualVolumeHostID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the Virtual Volume to clone.")]
		public UUIDNullable VirtualVolumeID
		{
			get
			{
				return _virtualVolumeID;
			}
			set
			{
				_virtualVolumeID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The name for the newly-created volume.")]
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
		[Parameter(Mandatory = false, HelpMessage = "Will the snapshot be writable?")]
		public boolean WritableSnapshot
		{
			get
			{
				return _writableSnapshot;
			}
			set
			{
				_writableSnapshot = value;
			}
		}
		[Parameter(Mandatory = false)]
		public UUIDNullable CallingVirtualVolumeHostID
		{
			get
			{
				return _callingVirtualVolumeHostID;
			}
			set
			{
				_callingVirtualVolumeHostID = value;
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
			var request = new PrepareVirtualSnapshotRequest();
			request.VirtualVolumeID = _virtualVolumeID;
			request.Name = _name;
			request.WritableSnapshot = _writableSnapshot;
			request.CallingVirtualVolumeHostID = _callingVirtualVolumeHostID;
			var objsFromAPI = SendRequest<PrepareVirtualSnapshotResult>("PrepareVirtualSnapshot", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		