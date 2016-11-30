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
	/// EnableSnmp is used to enable SNMP on the cluster nodes. The values set with this interface apply to all nodes in the cluster, and the values that are passed replace, in whole, all values set in any previous call to EnableSnmp.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Enable, "SFSnmp")]
	public class EnableSnmp : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// If set to "true", then SNMP v3 is enabled on each node in the cluster. If set to "false", then SNMP v2 is enabled.
		/// </summary>
		private boolean _snmpV3Enabled;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "If set to \"true\", then SNMP v3 is enabled on each node in the cluster. If set to \"false\", then SNMP v2 is enabled.")]
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
			var request = new EnableSnmpRequest();
			request.SnmpV3Enabled = _snmpV3Enabled;
			var objsFromAPI = SendRequest<EnableSnmpResult>("EnableSnmp", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		