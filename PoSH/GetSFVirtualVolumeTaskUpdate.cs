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
	/// GetVirtualVolumeTaskUpdate checks the status of a VVol Async Task.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVirtualVolumeTaskUpdate")]
	public class GetVirtualVolumeTaskUpdate : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The UUID of the VVol Task.
		/// </summary>
		private UUIDNullable _virtualVolumeTaskID;
		private UUIDNullable _callingVirtualVolumeHostID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The UUID of the VVol Task.")]
		public UUIDNullable VirtualVolumeTaskID
		{
			get
			{
				return _virtualVolumeTaskID;
			}
			set
			{
				_virtualVolumeTaskID = value;
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
			var request = new GetVirtualVolumeTaskUpdateRequest();
			request.VirtualVolumeTaskID = _virtualVolumeTaskID;
			request.CallingVirtualVolumeHostID = _callingVirtualVolumeHostID;
			var objsFromAPI = SendRequest<GetVirtualVolumeTaskUpdateResult>("GetVirtualVolumeTaskUpdate", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		