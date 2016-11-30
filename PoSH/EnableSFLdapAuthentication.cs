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
	/// The EnableLdapAuthentication method is used to configure an LDAP server connection to use for LDAP authentication to a SolidFire cluster. Users that are members on the LDAP server can then log in to a SolidFire storage system using their LDAP authentication userid and password.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Enable, "SFLdapAuthentication")]
	public class EnableLdapAuthentication : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Identifies which user authentcation method will be used. <br/>
		/// Must be one of the following:<br/>
		/// <b>DirectBind</b><br/>
		/// <b>SearchAndBind</b> (default)
		/// </summary>
		private string _authType;
		/// <summary>
		/// The base DN of the tree to start the group search (will do a subtree search from here).
		/// </summary>
		private string _groupSearchBaseDN;
		/// <summary>
		/// REQUIRED for CustomFilter<br/>
		/// For use with the CustomFilter search type, an LDAP filter to use to return the DNs of a user's groups.<br/>
		/// The string can have placeholder text of %USERNAME% and %USERDN% to be replaced with their username and full userDN as needed.
		/// </summary>
		private string _groupSearchCustomFilter;
		/// <summary>
		/// Controls the default group search filter used, can be one of the following:<br/>
		/// <b>NoGroups</b>: No group support.<br/>
		/// <b>ActiveDirectory</b>: (default) Nested membership of all of a user's AD groups.<br/>
		/// <b>MemberDN</b>: MemberDN style groups (single-level).
		/// </summary>
		private string _groupSearchType;
		/// <summary>
		/// REQUIRED for SearchAndBind<br/>
		/// A fully qualified DN to log in with to perform an LDAP search for the user (needs read access to the LDAP directory).
		/// </summary>
		private string _searchBindDN;
		/// <summary>
		/// REQUIRED for SearchAndBind<br/>
		/// The password for the searchBindDN account used for searching.
		/// </summary>
		private string _searchBindPassword;
		/// <summary>
		/// A list of LDAP server URIs (examples: "ldap://1.2.3.4" and ldaps://1.2.3.4:123")
		/// </summary>
		private string[] _serverURIs;
		/// <summary>
		/// REQUIRED for DirectBind<br/>
		/// A string that is used to form a fully qualified user DN.<br/>
		/// The string should have the placeholder text "%USERNAME%" which will be replaced with the username of the authenticating user.
		/// </summary>
		private string _userDNTemplate;
		/// <summary>
		/// REQUIRED for SearchAndBind
		/// The base DN of the tree used to start the search (will do a subtree search from here).
		/// </summary>
		private string _userSearchBaseDN;
		/// <summary>
		/// REQUIRED for SearchAndBind.<br/>
		/// The LDAP filter to use.<br/>
		/// The string should have the placeholder text "%USERNAME%" which will be replaced with the username of the authenticating user.<br/>
		/// Example: (&(objectClass=person) (sAMAccountName=%USERNAME%)) will use the sAMAccountName field in Active Directory to match the nusername entered at cluster login.
		/// </summary>
		private string _userSearchFilter;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "[u'Identifies which user authentcation method will be used. <br/>', u'Must be one of the following:<br/>', u'<b>DirectBind</b><br/>', u'<b>SearchAndBind</b> (default)']")]
		public string AuthType
		{
			get
			{
				return _authType;
			}
			set
			{
				_authType = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'The base DN of the tree to start the group search (will do a subtree search from here).']")]
		public string GroupSearchBaseDN
		{
			get
			{
				return _groupSearchBaseDN;
			}
			set
			{
				_groupSearchBaseDN = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'REQUIRED for CustomFilter<br/>', u\"For use with the CustomFilter search type, an LDAP filter to use to return the DNs of a user's groups.<br/>\", u'The string can have placeholder text of %USERNAME% and %USERDN% to be replaced with their username and full userDN as needed.']")]
		public string GroupSearchCustomFilter
		{
			get
			{
				return _groupSearchCustomFilter;
			}
			set
			{
				_groupSearchCustomFilter = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Controls the default group search filter used, can be one of the following:<br/>', u'<b>NoGroups</b>: No group support.<br/>', u\"<b>ActiveDirectory</b>: (default) Nested membership of all of a user's AD groups.<br/>\", u'<b>MemberDN</b>: MemberDN style groups (single-level).']")]
		public string GroupSearchType
		{
			get
			{
				return _groupSearchType;
			}
			set
			{
				_groupSearchType = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'REQUIRED for SearchAndBind<br/>', u'A fully qualified DN to log in with to perform an LDAP search for the user (needs read access to the LDAP directory).']")]
		public string SearchBindDN
		{
			get
			{
				return _searchBindDN;
			}
			set
			{
				_searchBindDN = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'REQUIRED for SearchAndBind<br/>', u'The password for the searchBindDN account used for searching.']")]
		public string SearchBindPassword
		{
			get
			{
				return _searchBindPassword;
			}
			set
			{
				_searchBindPassword = value;
			}
		}
		[Parameter(HelpMessage = "[u'A list of LDAP server URIs (examples: \"ldap://1.2.3.4\" and ldaps://1.2.3.4:123\")']")]
		public string[] ServerURIs
		{
			get
			{
				return _serverURIs;
			}
			set
			{
				_serverURIs = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'REQUIRED for DirectBind<br/>', u'A string that is used to form a fully qualified user DN.<br/>', u'The string should have the placeholder text \"%USERNAME%\" which will be replaced with the username of the authenticating user.']")]
		public string UserDNTemplate
		{
			get
			{
				return _userDNTemplate;
			}
			set
			{
				_userDNTemplate = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'REQUIRED for SearchAndBind', u'The base DN of the tree used to start the search (will do a subtree search from here).']")]
		public string UserSearchBaseDN
		{
			get
			{
				return _userSearchBaseDN;
			}
			set
			{
				_userSearchBaseDN = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'REQUIRED for SearchAndBind.<br/>', u'The LDAP filter to use.<br/>', u'The string should have the placeholder text \"%USERNAME%\" which will be replaced with the username of the authenticating user.<br/>', u'Example: (&(objectClass=person) (sAMAccountName=%USERNAME%)) will use the sAMAccountName field in Active Directory to match the nusername entered at cluster login.']")]
		public string UserSearchFilter
		{
			get
			{
				return _userSearchFilter;
			}
			set
			{
				_userSearchFilter = value;
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
			var request = new EnableLdapAuthenticationRequest();
			request.AuthType = _authType;
			request.GroupSearchBaseDN = _groupSearchBaseDN;
			request.GroupSearchCustomFilter = _groupSearchCustomFilter;
			request.GroupSearchType = _groupSearchType;
			request.SearchBindDN = _searchBindDN;
			request.SearchBindPassword = _searchBindPassword;
			request.ServerURIs = _serverURIs;
			request.UserDNTemplate = _userDNTemplate;
			request.UserSearchBaseDN = _userSearchBaseDN;
			request.UserSearchFilter = _userSearchFilter;
			var objsFromAPI = SendRequest<EnableLdapAuthenticationResult>("EnableLdapAuthentication", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		