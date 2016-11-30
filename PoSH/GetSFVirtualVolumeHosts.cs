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
	/// ListVirtualVolumeHosts returns a list of known ESX hosts.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVirtualVolumeHosts")]
	public class ListVirtualVolumeHosts : SFCmdlet
	{
		#region Private Data
		private Guid[] _virtualVolumeHostIDs;
		private UUIDNullable _callingVirtualVolumeHostID;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false)]
		public Guid[] VirtualVolumeHostIDs
		{
			get
			{
				return _virtualVolumeHostIDs;
			}
			set
			{
				_virtualVolumeHostIDs = value;
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
			var request = new ListVirtualVolumeHostsRequest();
			request.VirtualVolumeHostIDs = _virtualVolumeHostIDs;
			request.CallingVirtualVolumeHostID = _callingVirtualVolumeHostID;
			var objsFromAPI = SendRequest<ListVirtualVolumeHostsResult>("ListVirtualVolumeHosts", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		