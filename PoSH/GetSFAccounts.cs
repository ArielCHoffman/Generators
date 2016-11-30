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
	/// Returns the entire list of accounts, with optional paging support.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFAccounts")]
	public class ListAccounts : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Starting AccountID to return.
		/// If no Account exists with this AccountID,
		/// the next Account by AccountID order is used as the start of the list.
		/// To page through the list, pass the AccountID of the last Account in the previous response + 1
		/// </summary>
		private Int64 _startAccountID;
		/// <summary>
		/// Maximum number of AccountInfo objects to return.
		/// </summary>
		private Int64 _limit;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "[u'Starting AccountID to return.', u'If no Account exists with this AccountID,', u'the next Account by AccountID order is used as the start of the list.', u'To page through the list, pass the AccountID of the last Account in the previous response + 1']")]
		public Int64 StartAccountID
		{
			get
			{
				return _startAccountID;
			}
			set
			{
				_startAccountID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Maximum number of AccountInfo objects to return.")]
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
			var request = new ListAccountsRequest();
			request.StartAccountID = _startAccountID;
			request.Limit = _limit;
			var objsFromAPI = SendRequest<ListAccountsResult>("ListAccounts", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		