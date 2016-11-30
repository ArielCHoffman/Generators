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
	/// AddVirtualNetwork is used to add a new virtual network to a cluster configuration. When a virtual network is added, an interface for each node is created and each will require a virtual network IP address. The number of IP addresses specified as a parameter for this API method must be equal to or greater than the number of nodes in the cluster. Virtual network addresses are bulk provisioned by SolidFire and assigned to individual nodes automatically. Virtual network addresses do not need to be assigned to nodes manually.
	/// <br/><br/>
	/// <b>Note:</b> The AddVirtualNetwork method is used only to create a new virtual network. If you want to make changes to a virtual network, please use the ModifyVirtualNetwork method.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Add, "SFVirtualNetwork")]
	public class AddVirtualNetwork : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A unique virtual network (VLAN) tag. Supported values are 1 to 4095 (the number zero (0) is not supported).
		/// </summary>
		private Int64 _virtualNetworkTag;
		/// <summary>
		/// User defined name for the new virtual network.
		/// </summary>
		private string _name;
		/// <summary>
		/// Unique Range of IP addresses to include in the virtual network.
		/// Attributes for this parameter are:
		/// <br/><b>start:</b> start of the IP address range. (String)
		/// <br/><b>size:</b> numbre of IP addresses to include in the block. (Integer)
		/// </summary>
		private AddressBlock[] _addressBlocks;
		/// <summary>
		/// Unique netmask for the virtual network being created.
		/// </summary>
		private string _netmask;
		/// <summary>
		/// Unique storage IP address for the virtual network being created.
		/// </summary>
		private string _svip;
		/// <summary>
		/// 
		/// </summary>
		private string _gateway;
		/// <summary>
		/// 
		/// </summary>
		private boolean _namespace;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "A unique virtual network (VLAN) tag. Supported values are 1 to 4095 (the number zero (0) is not supported).")]
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
		[Parameter(HelpMessage = "User defined name for the new virtual network.")]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		[Parameter(HelpMessage = "[u'Unique Range of IP addresses to include in the virtual network.', u'Attributes for this parameter are:', u'<br/><b>start:</b> start of the IP address range. (String)', u'<br/><b>size:</b> numbre of IP addresses to include in the block. (Integer)']")]
		public AddressBlock[] AddressBlocks
		{
			get
			{
				return _addressBlocks;
			}
			set
			{
				_addressBlocks = value;
			}
		}
		[Parameter(HelpMessage = "[u'Unique netmask for the virtual network being created.']")]
		public string Netmask
		{
			get
			{
				return _netmask;
			}
			set
			{
				_netmask = value;
			}
		}
		[Parameter(HelpMessage = "[u'Unique storage IP address for the virtual network being created.']")]
		public string Svip
		{
			get
			{
				return _svip;
			}
			set
			{
				_svip = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'']")]
		public string Gateway
		{
			get
			{
				return _gateway;
			}
			set
			{
				_gateway = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'']")]
		public boolean Namespace
		{
			get
			{
				return _namespace;
			}
			set
			{
				_namespace = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'List of Name/Value pairs in JSON object format.']")]
		public hashtable Attributes
		{
			get
			{
				return _attributes;
			}
			set
			{
				_attributes = value;
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
			var request = new AddVirtualNetworkRequest();
			request.VirtualNetworkTag = _virtualNetworkTag;
			request.Name = _name;
			request.AddressBlocks = _addressBlocks;
			request.Netmask = _netmask;
			request.Svip = _svip;
			request.Gateway = _gateway;
			request.Namespace = _namespace;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<AddVirtualNetworkResult>("AddVirtualNetwork", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		