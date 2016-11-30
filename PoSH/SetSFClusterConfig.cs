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
	/// The SetClusterConfig API method is used to set the configuration this node uses to communicate with the cluster it is associated with. To see the states in which these objects can be modified see Cluster Object on page 109. To display the current cluster interface settings for a node, run the GetClusterConfig API method.
	/// <br/><br/>
	/// <b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Set, "SFClusterConfig")]
	public class SetClusterConfig : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Objects that are changed for the cluster interface settings. Only the fields you want changed need to be added to this method as objects in the "cluster" parameter.
		/// </summary>
		private ClusterConfig _cluster;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Objects that are changed for the cluster interface settings. Only the fields you want changed need to be added to this method as objects in the \"cluster\" parameter.")]
		public ClusterConfig Cluster
		{
			get
			{
				return _cluster;
			}
			set
			{
				_cluster = value;
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
			var request = new SetClusterConfigRequest();
			request.Cluster = _cluster;
			var objsFromAPI = SendRequest<SetClusterConfigResult>("SetClusterConfig", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		