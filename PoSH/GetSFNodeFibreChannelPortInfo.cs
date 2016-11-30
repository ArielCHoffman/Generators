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
	/// The ListNodeFibreChannelPortInfo is used to return information about the Fibre Channel ports. The API method is intended for use on individual nodes; userid and password is required for access to individual Fibre Channel nodes. However, this method can be used on the cluster if the force=true parameter is included in the method call. When used on the cluster, all Fibre Channel interfaces are listed.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFNodeFibreChannelPortInfo")]
	public class ListNodeFibreChannelPortInfo : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specify force=true to call method on all member nodes of the cluster.
		/// </summary>
		private boolean _force;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Specify force=true to call method on all member nodes of the cluster.")]
		public boolean Force
		{
			get
			{
				return _force;
			}
			set
			{
				_force = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(7);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ListNodeFibreChannelPortInfoRequest();
			request.Force = _force;
			var objsFromAPI = SendRequest<ListNodeFibreChannelPortInfoResult>("ListNodeFibreChannelPortInfo", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		