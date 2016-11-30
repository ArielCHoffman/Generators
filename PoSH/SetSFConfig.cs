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
	/// The SetConfig API method is used to set all the configuration information for the node. This includes the same information available via calls to SetClusterConfig and SetNetworkConfig in one API method.
	/// <br/><br/>
	/// <b>Warning!</b> Changing the 'bond-mode' on a node can cause a temporary loss of network connectivity. Caution should be taken when using this method.
	/// <br/><br/>
	/// <b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Set, "SFConfig")]
	public class SetConfig : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Objects that you want changed for the cluster interface settings.
		/// </summary>
		private Config _config;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Objects that you want changed for the cluster interface settings.")]
		public Config Config
		{
			get
			{
				return _config;
			}
			set
			{
				_config = value;
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
			var request = new SetConfigRequest();
			request.Config = _config;
			var objsFromAPI = SendRequest<SetConfigResult>("SetConfig", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		