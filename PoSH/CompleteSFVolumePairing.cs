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
	/// CompleteVolumePairing is used to complete the pairing of two volumes.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Complete, "SFVolumePairing")]
	public class CompleteVolumePairing : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The key returned from the "StartVolumePairing" API method.
		/// </summary>
		private string _volumePairingKey;
		/// <summary>
		/// The ID of volume on which to complete the pairing process.
		/// </summary>
		private Int64 _volumeID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The key returned from the \"StartVolumePairing\" API method.")]
		public string VolumePairingKey
		{
			get
			{
				return _volumePairingKey;
			}
			set
			{
				_volumePairingKey = value;
			}
		}
		[Parameter(HelpMessage = "The ID of volume on which to complete the pairing process.")]
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
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new CompleteVolumePairingRequest();
			request.VolumePairingKey = _volumePairingKey;
			request.VolumeID = _volumeID;
			var objsFromAPI = SendRequest<CompleteVolumePairingResult>("CompleteVolumePairing", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		