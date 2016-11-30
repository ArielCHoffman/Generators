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
	/// RemoveVirtualNetwork is used to remove a previously added virtual network.
	/// <br/><br/>
	/// <b>Note:</b> This method requires either the VirtualNetworkID of the VirtualNetworkTag as a parameter, but not both.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFVirtualNetwork")]
	public class RemoveVirtualNetwork : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Network ID that identifies the virtual network to remove.
		/// </summary>
		private Int64 _virtualNetworkID;
		/// <summary>
		/// Network Tag that identifies the virtual network to remove.
		/// </summary>
		private Int64 _virtualNetworkTag;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Network ID that identifies the virtual network to remove.")]
		public Int64 VirtualNetworkID
		{
			get
			{
				return _virtualNetworkID;
			}
			set
			{
				_virtualNetworkID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Network Tag that identifies the virtual network to remove.")]
		public Int64 VirtualNetworkTag
		{
			get
			{
				return _virtualNetworkTag;
			}
			set
			{
				_virtualNetworkTag = value;
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
			var request = new RemoveVirtualNetworkRequest();
			request.VirtualNetworkID = _virtualNetworkID;
			request.VirtualNetworkTag = _virtualNetworkTag;
			var objsFromAPI = SendRequest<RemoveVirtualNetworkResult>("RemoveVirtualNetwork", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		