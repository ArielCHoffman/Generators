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
	/// The GetVolumeAccessGroupLunAssignments is used to return information LUN mappings of a specified volume access group.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumeAccessGroupLunAssignments")]
	public class GetVolumeAccessGroupLunAssignments : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique volume access group ID used to return information.
		/// </summary>
		private Int64 _volumeAccessGroupID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Unique volume access group ID used to return information.")]
		public Int64 VolumeAccessGroupID
		{
			get
			{
				return _volumeAccessGroupID;
			}
			set
			{
				_volumeAccessGroupID = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(7);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new GetVolumeAccessGroupLunAssignmentsRequest();
			request.VolumeAccessGroupID = _volumeAccessGroupID;
			var objsFromAPI = SendRequest<GetVolumeAccessGroupLunAssignmentsResult>("GetVolumeAccessGroupLunAssignments", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		