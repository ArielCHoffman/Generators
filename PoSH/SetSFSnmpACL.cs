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
	/// SetSnmpACL is used to configure SNMP access permissions on the cluster nodes. The values set with this interface apply to all nodes in the cluster, and the values that are passed replace, in whole, all values set in any previous call to SetSnmpACL. Also note that the values set with this interface replace all "network" or "usmUsers" values set with the older SetSnmpInfo.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Set, "SFSnmpACL")]
	public class SetSnmpACL : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of networks and what type of access they have to the SNMP servers running on the cluster nodes. See SNMP Network Object for possible "networks" values. REQUIRED if SNMP v# is disabled.
		/// </summary>
		private SnmpNetwork[] _networks;
		/// <summary>
		/// List of users and the type of access they have to the SNMP servers running on the cluster nodes. REQUIRED if SNMP v3 is enabled.
		/// </summary>
		private SnmpV3UsmUser[] _usmUsers;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "List of networks and what type of access they have to the SNMP servers running on the cluster nodes. See SNMP Network Object for possible \"networks\" values. REQUIRED if SNMP v# is disabled.")]
		public SnmpNetwork[] Networks
		{
			get
			{
				return _networks;
			}
			set
			{
				_networks = value;
			}
		}
		[Parameter(HelpMessage = "List of users and the type of access they have to the SNMP servers running on the cluster nodes. REQUIRED if SNMP v3 is enabled.")]
		public SnmpV3UsmUser[] UsmUsers
		{
			get
			{
				return _usmUsers;
			}
			set
			{
				_usmUsers = value;
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
			var request = new SetSnmpACLRequest();
			request.Networks = _networks;
			request.UsmUsers = _usmUsers;
			var objsFromAPI = SendRequest<SetSnmpACLResult>("SetSnmpACL", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		