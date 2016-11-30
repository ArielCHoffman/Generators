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
	/// GetVirtualVolumeAllocatedBitmap scans a VVol segment and returns the number of 
	/// chunks not shared between two volumes. This call will return results in less 
	/// than 30 seconds. If the specified VVol and the base VVil are not related, an 
	/// error is thrown. If the offset/length combination is invalid or out fo range 
	/// an error is thrown.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVirtualVolumeUnsharedChunks")]
	public class GetVirtualVolumeUnsharedChunks : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the Virtual Volume.
		/// </summary>
		private UUIDNullable _virtualVolumeID;
		/// <summary>
		/// The ID of the Virtual Volume to compare against.
		/// </summary>
		private UUIDNullable _baseVirtualVolumeID;
		/// <summary>
		/// Start Byte offset.
		/// </summary>
		private Int64 _segmentStart;
		/// <summary>
		/// Length of the scan segment in bytes.
		/// </summary>
		private Int64 _segmentLength;
		/// <summary>
		/// Number of bytes represented by one bit in the bitmap.
		/// </summary>
		private Int64 _chunkSize;
		private UUIDNullable _callingVirtualVolumeHostID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the Virtual Volume.")]
		public UUIDNullable VirtualVolumeID
		{
			get
			{
				return _virtualVolumeID;
			}
			set
			{
				_virtualVolumeID = value;
			}
		}
		[Parameter(HelpMessage = "The ID of the Virtual Volume to compare against.")]
		public UUIDNullable BaseVirtualVolumeID
		{
			get
			{
				return _baseVirtualVolumeID;
			}
			set
			{
				_baseVirtualVolumeID = value;
			}
		}
		[Parameter(HelpMessage = "Start Byte offset.")]
		public Int64 SegmentStart
		{
			get
			{
				return _segmentStart;
			}
			set
			{
				_segmentStart = value;
			}
		}
		[Parameter(HelpMessage = "Length of the scan segment in bytes.")]
		public Int64 SegmentLength
		{
			get
			{
				return _segmentLength;
			}
			set
			{
				_segmentLength = value;
			}
		}
		[Parameter(HelpMessage = "Number of bytes represented by one bit in the bitmap.")]
		public Int64 ChunkSize
		{
			get
			{
				return _chunkSize;
			}
			set
			{
				_chunkSize = value;
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
			var request = new GetVirtualVolumeUnsharedChunksRequest();
			request.VirtualVolumeID = _virtualVolumeID;
			request.BaseVirtualVolumeID = _baseVirtualVolumeID;
			request.SegmentStart = _segmentStart;
			request.SegmentLength = _segmentLength;
			request.ChunkSize = _chunkSize;
			request.CallingVirtualVolumeHostID = _callingVirtualVolumeHostID;
			var objsFromAPI = SendRequest<GetVirtualVolumeUnsharedChunksResult>("GetVirtualVolumeUnsharedChunks", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		