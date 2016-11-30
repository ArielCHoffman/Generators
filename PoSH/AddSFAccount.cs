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
	/// Used to add a new account to the system.
	/// New volumes can be created under the new account.
	/// The CHAP settings specified for the account applies to all volumes owned by the account.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Add, "SFAccount")]
	public class AddAccount : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique username for this account.
		/// (May be 1 to 64 characters in length).
		/// </summary>
		private string _username;
		/// <summary>
		/// CHAP secret to use for the initiator.
		/// Should be 12-16 characters long and impenetrable.
		/// The CHAP initiator secrets must be unique and cannot be the same as the target CHAP secret.
		/// <br/><br/>
		/// If not specified, a random secret is created.
		/// </summary>
		private CHAPSecret _initiatorSecret;
		/// <summary>
		/// CHAP secret to use for the target (mutual CHAP authentication).
		/// Should be 12-16 characters long and impenetrable.
		/// The CHAP target secrets must be unique and cannot be the same as the initiator CHAP secret.
		/// <br/><br/>
		/// If not specified, a random secret is created.
		/// </summary>
		private CHAPSecret _targetSecret;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Unique username for this account.', u'(May be 1 to 64 characters in length).']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'CHAP secret to use for the initiator.', u'Should be 12-16 characters long and impenetrable.', u'The CHAP initiator secrets must be unique and cannot be the same as the target CHAP secret.', u'<br/><br/>', u'If not specified, a random secret is created.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'CHAP secret to use for the target (mutual CHAP authentication).', u'Should be 12-16 characters long and impenetrable.', u'The CHAP target secrets must be unique and cannot be the same as the initiator CHAP secret.', u'<br/><br/>', u'If not specified, a random secret is created.']")]
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
			var request = new AddAccountRequest();
			request.Username = _username;
			request.InitiatorSecret = _initiatorSecret;
			request.TargetSecret = _targetSecret;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<AddAccountResult>("AddAccount", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		