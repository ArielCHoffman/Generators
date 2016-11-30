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
	/// Used to modify an existing account.
	/// When locking an account, any existing connections from that account are immediately terminated.
	/// When changing CHAP settings, any existing connections continue to be active,
	/// and the new CHAP values are only used on subsequent connection or reconnection.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFAccount")]
	public class ModifyAccount : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// AccountID for the account to modify.
		/// </summary>
		private Int64 _accountID;
		/// <summary>
		/// Change the username of the account to this value.
		/// </summary>
		private string _username;
		/// <summary>
		/// Status of the account.
		/// </summary>
		private string _status;
		/// <summary>
		/// CHAP secret to use for the initiator.
		/// Should be 12-16 characters long and impenetrable.
		/// </summary>
		private CHAPSecret _initiatorSecret;
		/// <summary>
		/// CHAP secret to use for the target (mutual CHAP authentication).
		/// Should be 12-16 characters long and impenetrable.
		/// </summary>
		private CHAPSecret _targetSecret;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "AccountID for the account to modify.")]
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
		[Parameter(Mandatory = false, HelpMessage = "Change the username of the account to this value.")]
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
		[Parameter(Mandatory = false, HelpMessage = "Status of the account.")]
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'CHAP secret to use for the initiator.', u'Should be 12-16 characters long and impenetrable.']")]
		public CHAPSecret InitiatorSecret
		{
			get
			{
				return _initiatorSecret;
			}
			set
			{
				_initiatorSecret = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'CHAP secret to use for the target (mutual CHAP authentication).', u'Should be 12-16 characters long and impenetrable.']")]
		public CHAPSecret TargetSecret
		{
			get
			{
				return _targetSecret;
			}
			set
			{
				_targetSecret = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "List of Name/Value pairs in JSON object format.")]
		public hashtable Attributes
		{
			get
			{
				return _attributes;
			}
			set
			{
				_attributes = value;
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
			var request = new ModifyAccountRequest();
			request.AccountID = _accountID;
			request.Username = _username;
			request.Status = _status;
			request.InitiatorSecret = _initiatorSecret;
			request.TargetSecret = _targetSecret;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<ModifyAccountResult>("ModifyAccount", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		