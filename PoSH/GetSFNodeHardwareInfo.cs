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
	/// GetNodeHardwareInfo is used to return all the hardware info and status for the node specified. This generally includes manufacturers, vendors, versions, and other associated hardware identification information.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFNodeHardwareInfo")]
	public class GetNodeHardwareInfo : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the node for which hardware information is being requested.  Information about a  node is returned if a   node is specified.
		/// </summary>
		private Int64 _nodeID;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "The ID of the node for which hardware information is being requested.  Information about a  node is returned if a   node is specified.")]
		public Int64 NodeID
		{
			get
			{
				return _nodeID;
			}
			set
			{
				_nodeID = value;
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
			var request = new GetNodeHardwareInfoRequest();
			request.NodeID = _nodeID;
			var objsFromAPI = SendRequest<GetNodeHardwareInfoResult>("GetNodeHardwareInfo", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		