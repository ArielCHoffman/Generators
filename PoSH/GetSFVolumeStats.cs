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
	/// GetVolumeStats is used to retrieve high-level activity measurements for a single volume.
	/// Values are cumulative from the creation of the volume.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumeStats")]
	public class GetVolumeStats : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specifies the volume for which statistics is gathered.
		/// </summary>
		private Int64 _volumeID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Specifies the volume for which statistics is gathered.")]
		public Int64 VolumeID
		{
			get
			{
				return _volumeID;
			}
			set
			{
				_volumeID = value;
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
			var request = new GetVolumeStatsRequest();
			request.VolumeID = _volumeID;
			var objsFromAPI = SendRequest<GetVolumeStatsResult>("GetVolumeStats", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		