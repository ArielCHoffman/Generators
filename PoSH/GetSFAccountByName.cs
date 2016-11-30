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
	/// Returns details about an account, given its Username.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFAccountByName")]
	public class GetAccountByName : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Username for the account.
		/// </summary>
		private string _username;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Username for the account.")]
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
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
			var request = new GetAccountByNameRequest();
			request.Username = _username;
			var objsFromAPI = SendRequest<GetAccountByNameResult>("GetAccountByName", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		