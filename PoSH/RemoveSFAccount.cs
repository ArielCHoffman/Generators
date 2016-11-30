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
	/// Used to remove an existing account.
	/// All Volumes must be deleted and purged on the account before it can be removed.
	/// If volumes on the account are still pending deletion, RemoveAccount cannot be used until DeleteVolume to delete and purge the volumes.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFAccount")]
	public class RemoveAccount : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// AccountID for the account to remove.
		/// </summary>
		private Int64 _accountID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "AccountID for the account to remove.")]
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
			var request = new RemoveAccountRequest();
			request.AccountID = _accountID;
			var objsFromAPI = SendRequest<RemoveAccountResult>("RemoveAccount", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		