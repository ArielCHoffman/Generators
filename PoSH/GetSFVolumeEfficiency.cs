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
	/// GetVolumeEfficiency is used to retrieve information about a volume.
	/// Only the volume given as a parameter in this API method is used to compute the capacity.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumeEfficiency")]
	public class GetVolumeEfficiency : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specifies the volume for which capacity is computed.
		/// </summary>
		private Int64 _volumeID;
		private boolean _force;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Specifies the volume for which capacity is computed.")]
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
		[Parameter(Mandatory = false)]
		public boolean Force
		{
			get
			{
				return _force;
			}
			set
			{
				_force = value;
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
			var request = new GetVolumeEfficiencyRequest();
			request.VolumeID = _volumeID;
			request.Force = _force;
			var objsFromAPI = SendRequest<GetVolumeEfficiencyResult>("GetVolumeEfficiency", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		