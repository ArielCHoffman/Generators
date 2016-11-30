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
	/// ModifyVirtualNetwork is used to change various attributes of a VirtualNetwork object. This method can be used to add or remove address blocks, change the netmask IP, or modify the name or description of the virtual network.
	/// <br/><br/>
	/// <b>Note:</b> This method requires either the VirtualNetworkID or the VirtualNetworkTag as a parameter, but not both.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFVirtualNetwork")]
	public class ModifyVirtualNetwork : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique identifier of the virtual network to modify. This is the virtual network ID assigned by the SolidFire cluster.
		/// </summary>
		private Int64 _virtualNetworkID;
		/// <summary>
		/// Network Tag that identifies the virtual network to modify.
		/// </summary>
		private Int64 _virtualNetworkTag;
		/// <summary>
		/// New name for the virtual network.
		/// </summary>
		private string _name;
		/// <summary>
		/// New addressBlock to set for this Virtual Network object. This may contain new address blocks to add to the existing object or it may omit unused address blocks that need to be removed. Alternatively, existing address blocks may be extended or reduced in size. The size of the starting addressBlocks for a Virtual Network object can only be increased, and can never be decreased.
		/// Attributes for this parameter are:
		/// <br/><b>start:</b> start of the IP address range. (String)
		/// <br/><b>size:</b> numbre of IP addresses to include in the block. (Integer)
		/// </summary>
		private AddressBlock[] _addressBlocks;
		/// <summary>
		/// New netmask for this virtual network.
		/// </summary>
		private string _netmask;
		/// <summary>
		/// The storage virtual IP address for this virtual network. The svip for Virtual Network cannot be changed. A new Virtual Network must be created in order to use a different svip address.
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
		/// A new list of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Unique identifier of the virtual network to modify. This is the virtual network ID assigned by the SolidFire cluster.")]
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
		[Parameter(Mandatory = false, HelpMessage = "Network Tag that identifies the virtual network to modify.")]
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
		[Parameter(Mandatory = false, HelpMessage = "New name for the virtual network.")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'New addressBlock to set for this Virtual Network object. This may contain new address blocks to add to the existing object or it may omit unused address blocks that need to be removed. Alternatively, existing address blocks may be extended or reduced in size. The size of the starting addressBlocks for a Virtual Network object can only be increased, and can never be decreased.', u'Attributes for this parameter are:', u'<br/><b>start:</b> start of the IP address range. (String)', u'<br/><b>size:</b> numbre of IP addresses to include in the block. (Integer)']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'New netmask for this virtual network.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'The storage virtual IP address for this virtual network. The svip for Virtual Network cannot be changed. A new Virtual Network must be created in order to use a different svip address.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'A new list of Name/Value pairs in JSON object format.']")]
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
			var request = new ModifyVirtualNetworkRequest();
			request.VirtualNetworkID = _virtualNetworkID;
			request.VirtualNetworkTag = _virtualNetworkTag;
			request.Name = _name;
			request.AddressBlocks = _addressBlocks;
			request.Netmask = _netmask;
			request.Svip = _svip;
			request.Gateway = _gateway;
			request.Namespace = _namespace;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<ModifyVirtualNetworkResult>("ModifyVirtualNetwork", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		