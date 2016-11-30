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
	/// AddLdapClusterAdmin is used to add a new LDAP Cluster Admin. An LDAP Cluster Admin can be used to manage the cluster via the API and management tools. LDAP Cluster Admins are completely separate and unrelated to standard tenant accounts.
	/// <br/><br/>
	/// An LDAP group that has been defined in Active Directory can also be added using this API method. The access level that is given to the group will be passed to the individual users in the LDAP group.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Add, "SFLdapClusterAdmin")]
	public class AddLdapClusterAdmin : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The distinguished user name for the new LDAP cluster admin.
		/// </summary>
		private string _username;
		/// <summary>
		/// Controls which methods this Cluster Admin can use. For more details on the levels of access, see the Access Control appendix in the SolidFire API Reference.
		/// </summary>
		private string[] _access;
		/// <summary>
		/// Indicate your acceptance of the End User License Agreement when creating this cluster admin. To accept the EULA, set this parameter to true.
		/// </summary>
		private boolean _acceptEula;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The distinguished user name for the new LDAP cluster admin.")]
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
		[Parameter(HelpMessage = "Controls which methods this Cluster Admin can use. For more details on the levels of access, see the Access Control appendix in the SolidFire API Reference.")]
		public string[] Access
		{
			get
			{
				return _access;
			}
			set
			{
				_access = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Indicate your acceptance of the End User License Agreement when creating this cluster admin. To accept the EULA, set this parameter to true.")]
		public boolean AcceptEula
		{
			get
			{
				return _acceptEula;
			}
			set
			{
				_acceptEula = value;
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
			CheckConnection(8);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new AddLdapClusterAdminRequest();
			request.Username = _username;
			request.Access = _access;
			request.AcceptEula = _acceptEula;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<AddLdapClusterAdminResult>("AddLdapClusterAdmin", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		