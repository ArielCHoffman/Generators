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
	/// Copies one volume to another.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Copy, "SFVolume")]
	public class CopyVolume : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Source volume to copy.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// Destination volume for the copy.
		/// </summary>
		private Int64 _dstVolumeID;
		/// <summary>
		/// Snapshot ID of the source volume to create the copy from.
		/// </summary>
		private Int64 _snapshotID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Source volume to copy.")]
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
		[Parameter(HelpMessage = "Destination volume for the copy.")]
		public Int64 DstVolumeID
		{
			get
			{
				return _dstVolumeID;
			}
			set
			{
				_dstVolumeID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Snapshot ID of the source volume to create the copy from.")]
		public Int64 SnapshotID
		{
			get
			{
				return _snapshotID;
			}
			set
			{
				_snapshotID = value;
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
			var request = new CopyVolumeRequest();
			request.VolumeID = _volumeID;
			request.DstVolumeID = _dstVolumeID;
			request.SnapshotID = _snapshotID;
			var objsFromAPI = SendRequest<CopyVolumeResult>("CopyVolume", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		