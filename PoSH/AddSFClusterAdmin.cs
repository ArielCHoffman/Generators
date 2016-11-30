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
	/// AddClusterAdmin adds a new Cluster Admin. A Cluster Admin can be used to manage the cluster via the API and management tools. Cluster Admins are completely separate and unrelated to standard tenant accounts.
	/// <br/><br/>
	/// Each Cluster Admin can be restricted to a sub-set of the API. SolidFire recommends using multiple Cluster Admins for different users and applications. Each Cluster Admin should be given the minimal permissions necessary to reduce the potential impact of credential compromise.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Add, "SFClusterAdmin")]
	public class AddClusterAdmin : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique username for this Cluster Admin.
		/// </summary>
		private string _username;
		/// <summary>
		/// Password used to authenticate this Cluster Admin.
		/// </summary>
		private string _password;
		/// <summary>
		/// Controls which methods this Cluster Admin can use. For more details on the levels of access, see "Access Control" in the Element API Guide.
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
		[Parameter(HelpMessage = "Unique username for this Cluster Admin.")]
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
		[Parameter(HelpMessage = "Password used to authenticate this Cluster Admin.")]
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
		[Parameter(HelpMessage = "Controls which methods this Cluster Admin can use. For more details on the levels of access, see \"Access Control\" in the Element API Guide.")]
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
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new AddClusterAdminRequest();
			request.Username = _username;
			request.Password = _password;
			request.Access = _access;
			request.AcceptEula = _acceptEula;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<AddClusterAdminResult>("AddClusterAdmin", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		