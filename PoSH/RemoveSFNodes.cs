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
	/// RemoveNodes is used to remove one or more nodes that should no longer participate in the cluster. Before removing a node, all drives it contains must first be removed with "RemoveDrives" method. A node cannot be removed until the RemoveDrives process has completed and all data has been migrated away from the node.
	/// <br/><br/>
	/// Once removed, a node registers itself as a pending node and can be added again, or shut down which removes it from the "Pending Node" list.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFNodes")]
	public class RemoveNodes : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of NodeIDs for the nodes to be removed.
		/// </summary>
		private Int64[] _nodes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "List of NodeIDs for the nodes to be removed.")]
		public Int64[] Nodes
		{
			get
			{
				return _nodes;
			}
			set
			{
				_nodes = value;
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
			var request = new RemoveNodesRequest();
			request.Nodes = _nodes;
			var objsFromAPI = SendRequest<RemoveNodesResult>("RemoveNodes", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		