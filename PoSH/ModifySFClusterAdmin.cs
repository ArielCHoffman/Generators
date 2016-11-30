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
	/// ModifyClusterAdmin is used to change the settings for a Cluster Admin or LDAP Cluster Admin. Access for the administrator Cluster Admin account cannot be changed.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFClusterAdmin")]
	public class ModifyClusterAdmin : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// ClusterAdminID for the Cluster Admin or LDAP Cluster Admin to modify.
		/// </summary>
		private Int64 _clusterAdminID;
		/// <summary>
		/// Password used to authenticate this Cluster Admin.
		/// </summary>
		private string _password;
		/// <summary>
		/// Controls which methods this Cluster Admin can use. For more details on the levels of access, see "Access Control" in the Element API Guide.
		/// </summary>
		private string[] _access;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "ClusterAdminID for the Cluster Admin or LDAP Cluster Admin to modify.")]
		public Int64 ClusterAdminID
		{
			get
			{
				return _clusterAdminID;
			}
			set
			{
				_clusterAdminID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Password used to authenticate this Cluster Admin.")]
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
		[Parameter(Mandatory = false, HelpMessage = "Controls which methods this Cluster Admin can use. For more details on the levels of access, see \"Access Control\" in the Element API Guide.")]
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
			var request = new ModifyClusterAdminRequest();
			request.ClusterAdminID = _clusterAdminID;
			request.Password = _password;
			request.Access = _access;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<ModifyClusterAdminResult>("ModifyClusterAdmin", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		