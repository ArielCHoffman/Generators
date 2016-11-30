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
	/// The "SetNetworkConfig" method is used to set the network configuration for a node. To see the states in which these objects can be modified, see "Network Object for 1G and 10G Interfaces" on page 109 of the Element API. To display the current network settings for a node, run the "GetNetworkConfig" method.
	/// <br/><br/>
	/// <b>WARNING!</b> Changing the "bond-mode" on a node can cause a temporary loss of network connectivity. Caution should be taken when using this method.
	/// <br/><br/>
	/// <b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Set, "SFNetworkConfig")]
	public class SetNetworkConfig : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Objects that will be changed for the node network settings.
		/// </summary>
		private Network _network;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Objects that will be changed for the node network settings.")]
		public Network Network
		{
			get
			{
				return _network;
			}
			set
			{
				_network = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(5);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new SetNetworkConfigRequest();
			request.Network = _network;
			var objsFromAPI = SendRequest<SetNetworkConfigResult>("SetNetworkConfig", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		