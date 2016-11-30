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
	/// SetSnmpInfo is used to configure SNMP v2 and v3 on the cluster nodes. The values set with this interface apply to all nodes in the cluster, and the values that are passed replace, in whole, all values set in any previous call to SetSnmpInfo.
	/// <br/><br/>
	/// <b>Note</b>: EnableSnmp and SetSnmpACL methods can be used to accomplish the same results as SetSnmpInfo. SetSnmpInfo will no longer be available after the Element 8 release. Please use EnableSnmp and SetSnmpACL in the future.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Set, "SFSnmpInfo")]
	public class SetSnmpInfo : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of networks and what type of access they have to the SNMP servers running on the cluster nodes. See SNMP Network Object for possible "networks" values. SNMP v2 only.
		/// </summary>
		private SnmpNetwork[] _networks;
		/// <summary>
		/// If set to "true", then SNMP is enabled on each node in the cluster.
		/// </summary>
		private boolean _enabled;
		/// <summary>
		/// If set to "true", then SNMP v3 is enabled on each node in the cluster.
		/// </summary>
		private boolean _snmpV3Enabled;
		/// <summary>
		/// If SNMP v3 is enabled, this value must be passed in place of the "networks" parameter. SNMP v3 only.
		/// </summary>
		private SnmpV3UsmUser[] _usmUsers;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "List of networks and what type of access they have to the SNMP servers running on the cluster nodes. See SNMP Network Object for possible \"networks\" values. SNMP v2 only.")]
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
		[Parameter(Mandatory = false, HelpMessage = "If set to \"true\", then SNMP is enabled on each node in the cluster.")]
		public boolean Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "If set to \"true\", then SNMP v3 is enabled on each node in the cluster.")]
		public boolean SnmpV3Enabled
		{
			get
			{
				return _snmpV3Enabled;
			}
			set
			{
				_snmpV3Enabled = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "If SNMP v3 is enabled, this value must be passed in place of the \"networks\" parameter. SNMP v3 only.")]
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
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new SetSnmpInfoRequest();
			request.Networks = _networks;
			request.Enabled = _enabled;
			request.SnmpV3Enabled = _snmpV3Enabled;
			request.UsmUsers = _usmUsers;
			var objsFromAPI = SendRequest<SetSnmpInfoResult>("SetSnmpInfo", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		