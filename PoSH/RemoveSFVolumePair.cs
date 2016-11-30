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
	/// RemoveVolumePair is used to remove the remote pairing between two volumes.
	/// When the volume pairing information is removed, data is no longer replicated to or from the volume.
	/// This method should be run on both the source and target volumes that are paired together.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFVolumePair")]
	public class RemoveVolumePair : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// ID of the volume on which to stop the replication process.
		/// </summary>
		private Int64 _volumeID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "ID of the volume on which to stop the replication process.")]
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
			var request = new RemoveVolumePairRequest();
			request.VolumeID = _volumeID;
			var objsFromAPI = SendRequest<RemoveVolumePairResult>("RemoveVolumePair", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		