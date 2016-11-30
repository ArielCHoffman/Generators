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
	/// Gets protocol endpoints in the system
	/// If protocolEndpointIDs isn't specified all protocol endpoints
	/// are returned. Else the supplied protocolEndpointIDs are.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFProtocolEndpoints")]
	public class ListProtocolEndpoints : SFCmdlet
	{
		#region Private Data
		private Guid[] _protocolEndpointIDs;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false)]
		public Guid[] ProtocolEndpointIDs
		{
			get
			{
				return _protocolEndpointIDs;
			}
			set
			{
				_protocolEndpointIDs = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(9);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ListProtocolEndpointsRequest();
			request.ProtocolEndpointIDs = _protocolEndpointIDs;
			var objsFromAPI = SendRequest<ListProtocolEndpointsResult>("ListProtocolEndpoints", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		