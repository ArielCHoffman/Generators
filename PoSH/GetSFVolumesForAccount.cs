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
	/// ListVolumesForAccount returns the list of active AND (pending) deleted volumes for an account.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumesForAccount")]
	public class ListVolumesForAccount : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the account to list the volumes for.
		/// </summary>
		private Int64 _accountID;
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
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The ID of the account to list the volumes for.")]
		public Int64 AccountID
		{
			get
			{
				return _accountID;
			}
			set
			{
				_accountID = value;
			}
		}
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
			var request = new ListVolumesForAccountRequest();
			request.AccountID = _accountID;
			request.StartVolumeID = _startVolumeID;
			request.Limit = _limit;
			var objsFromAPI = SendRequest<ListVolumesForAccountResult>("ListVolumesForAccount", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		