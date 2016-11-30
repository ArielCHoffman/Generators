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
	/// GetVolumeAccessGroupEfficiency is used to retrieve efficiency information about a volume access group. Only the volume access group provided as parameters in this API method is used to compute the capacity.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumeAccessGroupEfficiency")]
	public class GetVolumeAccessGroupEfficiency : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specifies the volume access group for which capacity is computed.
		/// </summary>
		private Int64 _volumeAccessGroupID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Specifies the volume access group for which capacity is computed.")]
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
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new GetVolumeAccessGroupEfficiencyRequest();
			request.VolumeAccessGroupID = _volumeAccessGroupID;
			var objsFromAPI = SendRequest<GetVolumeAccessGroupEfficiencyResult>("GetVolumeAccessGroupEfficiency", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		