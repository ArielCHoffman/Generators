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
	/// The ListVolumes method is used to return a list of volumes that are in a cluster.
	/// You can specify the volumes you want to return in the list by using the available parameters.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumes")]
	public class ListVolumes : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the first volume to list.
		/// This can be useful for paging results.
		/// By default, this starts at the lowest VolumeID.
		/// </summary>
		private Int64 _startVolumeID;
		/// <summary>
		/// The maximum number of volumes to return from the API.
		/// </summary>
		private Int64 _limit;
		/// <summary>
		/// If specified, filter to only volumes with the provided status.
		/// By default, list all volumes.
		/// </summary>
		private string _volumeStatus;
		/// <summary>
		/// If specified, only fetch volumes which belong to the provided accounts.
		/// By default, list volumes for all accounts.
		/// </summary>
		private Int64[] _accounts;
		/// <summary>
		/// If specified, only fetch volumes which are paired (if true) or non-paired (if false).
		/// By default, list all volumes regardless of their pairing status.
		/// </summary>
		private boolean _isPaired;
		/// <summary>
		/// If specified, only fetch volumes specified in this list.
		/// This option cannot be specified if startVolumeID, limit, or accounts option is specified.
		/// </summary>
		private Int64[] _volumeIDs;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "[u'The ID of the first volume to list.', u'This can be useful for paging results.', u'By default, this starts at the lowest VolumeID.']")]
		public Int64 StartVolumeID
		{
			get
			{
				return _startVolumeID;
			}
			set
			{
				_startVolumeID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'The maximum number of volumes to return from the API.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'If specified, filter to only volumes with the provided status.', u'By default, list all volumes.']")]
		public string VolumeStatus
		{
			get
			{
				return _volumeStatus;
			}
			set
			{
				_volumeStatus = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'If specified, only fetch volumes which belong to the provided accounts.', u'By default, list volumes for all accounts.']")]
		public Int64[] Accounts
		{
			get
			{
				return _accounts;
			}
			set
			{
				_accounts = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'If specified, only fetch volumes which are paired (if true) or non-paired (if false).', u'By default, list all volumes regardless of their pairing status.']")]
		public boolean IsPaired
		{
			get
			{
				return _isPaired;
			}
			set
			{
				_isPaired = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'If specified, only fetch volumes specified in this list.', u'This option cannot be specified if startVolumeID, limit, or accounts option is specified.']")]
		public Int64[] VolumeIDs
		{
			get
			{
				return _volumeIDs;
			}
			set
			{
				_volumeIDs = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(8);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ListVolumesRequest();
			request.StartVolumeID = _startVolumeID;
			request.Limit = _limit;
			request.VolumeStatus = _volumeStatus;
			request.Accounts = _accounts;
			request.IsPaired = _isPaired;
			request.VolumeIDs = _volumeIDs;
			var objsFromAPI = SendRequest<ListVolumesResult>("ListVolumes", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		