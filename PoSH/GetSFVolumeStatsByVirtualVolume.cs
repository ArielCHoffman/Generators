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
	/// ListVolumeStatsByVirtualVolume enables you to list statistics for volumes, sorted by virtual volumes.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumeStatsByVirtualVolume")]
	public class ListVolumeStatsByVirtualVolume : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the virtual volume at which to begin the list.
		/// </summary>
		private UUIDNullable _startVirtualVolumeID;
		/// <summary>
		/// A list of virtual volume  IDs for which to retrieve information. If you specify this parameter, the method returns information about only these virtual volumes.
		/// </summary>
		private Guid[] _virtualVolumeIDs;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "The ID of the virtual volume at which to begin the list.")]
		public UUIDNullable StartVirtualVolumeID
		{
			get
			{
				return _startVirtualVolumeID;
			}
			set
			{
				_startVirtualVolumeID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "A list of virtual volume  IDs for which to retrieve information. If you specify this parameter, the method returns information about only these virtual volumes.")]
		public Guid[] VirtualVolumeIDs
		{
			get
			{
				return _virtualVolumeIDs;
			}
			set
			{
				_virtualVolumeIDs = value;
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
			var request = new ListVolumeStatsByVirtualVolumeRequest();
			request.StartVirtualVolumeID = _startVirtualVolumeID;
			request.VirtualVolumeIDs = _virtualVolumeIDs;
			var objsFromAPI = SendRequest<ListVolumeStatsByVirtualVolumeResult>("ListVolumeStatsByVirtualVolume", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		