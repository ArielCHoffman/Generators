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
	/// ListVirtualVolumes enables you to list the virtual volumes currently in the system. You can use this method to list all virtual volumes, or only list a subset.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVirtualVolumes")]
	public class ListVirtualVolumes : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Possible values:true: Include more details about each VVOL in the response.false: Include the standard level of detail about each VVOL in the response.
		/// </summary>
		private boolean _details;
		/// <summary>
		/// The maximum number of virtual volumes to list.
		/// </summary>
		private Int64 _limit;
		/// <summary>
		/// Possible values:true: Include information about the children of each VVOL in the response.false: Do not include information about the children of each VVOL in the response.
		/// </summary>
		private boolean _recursive;
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
		[Parameter(Mandatory = false, HelpMessage = "Possible values:true: Include more details about each VVOL in the response.false: Include the standard level of detail about each VVOL in the response.")]
		public boolean Details
		{
			get
			{
				return _details;
			}
			set
			{
				_details = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The maximum number of virtual volumes to list.")]
		public Int64 Limit
		{
			get
			{
				return _limit;
			}
			set
			{
				_limit = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Possible values:true: Include information about the children of each VVOL in the response.false: Do not include information about the children of each VVOL in the response.")]
		public boolean Recursive
		{
			get
			{
				return _recursive;
			}
			set
			{
				_recursive = value;
			}
		}
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
			var request = new ListVirtualVolumesRequest();
			request.Details = _details;
			request.Limit = _limit;
			request.Recursive = _recursive;
			request.StartVirtualVolumeID = _startVirtualVolumeID;
			request.VirtualVolumeIDs = _virtualVolumeIDs;
			var objsFromAPI = SendRequest<ListVirtualVolumesResult>("ListVirtualVolumes", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		