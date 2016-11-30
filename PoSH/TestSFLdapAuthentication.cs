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
	/// The TestLdapAuthentication is used to verify the currently enabled LDAP authentication configuration settings are correct. If the configuration settings are correct, the API call returns a list of the groups the tested user is a member of.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Test, "SFLdapAuthentication")]
	public class TestLdapAuthentication : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The username to be tested.
		/// </summary>
		private string _username;
		/// <summary>
		/// The password for the username to be tester.
		/// </summary>
		private string _password;
		/// <summary>
		/// An ldapConfiguration object to be tested. If this parameter is provided, the API call will test the provided configuration even if LDAP authentication is currently disabled.
		/// </summary>
		private LdapConfiguration _ldapConfiguration;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The username to be tested.")]
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
		[Parameter(HelpMessage = "The password for the username to be tester.")]
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "An ldapConfiguration object to be tested. If this parameter is provided, the API call will test the provided configuration even if LDAP authentication is currently disabled.")]
		public LdapConfiguration LdapConfiguration
		{
			get
			{
				return _ldapConfiguration;
			}
			set
			{
				_ldapConfiguration = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(7);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new TestLdapAuthenticationRequest();
			request.Username = _username;
			request.Password = _password;
			request.LdapConfiguration = _ldapConfiguration;
			var objsFromAPI = SendRequest<TestLdapAuthenticationResult>("TestLdapAuthentication", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		