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
	/// ListVirtualNetworks is used to get a list of all the configured virtual networks for the cluster. This method can be used to verify the virtual network settings in the cluster.
	/// 
	/// This method does not require any parameters to be passed. But, one or more VirtualNetworkIDs or VirtualNetworkTags can be passed in order to filter the results.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVirtualNetworks")]
	public class ListVirtualNetworks : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Network ID to filter the list for a single virtual network
		/// </summary>
		private Int64 _virtualNetworkID;
		/// <summary>
		/// Network Tag to filter the list for a single virtual network
		/// </summary>
		private Int64 _virtualNetworkTag;
		/// <summary>
		/// NetworkIDs to include in the list.
		/// </summary>
		private Int64[] _virtualNetworkIDs;
		/// <summary>
		/// Network Tags to include in the list.
		/// </summary>
		private Int64[] _virtualNetworkTags;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Network ID to filter the list for a single virtual network")]
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
		[Parameter(Mandatory = false, HelpMessage = "Network Tag to filter the list for a single virtual network")]
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
		[Parameter(Mandatory = false, HelpMessage = "NetworkIDs to include in the list.")]
		public Int64[] VirtualNetworkIDs
		{
			get
			{
				return _virtualNetworkIDs;
			}
			set
			{
				_virtualNetworkIDs = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Network Tags to include in the list.")]
		public Int64[] VirtualNetworkTags
		{
			get
			{
				return _virtualNetworkTags;
			}
			set
			{
				_virtualNetworkTags = value;
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
			var request = new ListVirtualNetworksRequest();
			request.VirtualNetworkID = _virtualNetworkID;
			request.VirtualNetworkTag = _virtualNetworkTag;
			request.VirtualNetworkIDs = _virtualNetworkIDs;
			request.VirtualNetworkTags = _virtualNetworkTags;
			var objsFromAPI = SendRequest<ListVirtualNetworksResult>("ListVirtualNetworks", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		