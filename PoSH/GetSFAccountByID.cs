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
	/// Returns details about an account, given its AccountID.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFAccountByID")]
	public class GetAccountByID : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specifies the account for which details are gathered.
		/// </summary>
		private Int64 _accountID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Specifies the account for which details are gathered.")]
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
			var request = new GetAccountByIDRequest();
			request.AccountID = _accountID;
			var objsFromAPI = SendRequest<GetAccountByIDResult>("GetAccountByID", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		