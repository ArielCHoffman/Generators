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
	/// CreateVolume is used to create a new (empty) volume on the cluster.
	/// When the volume is created successfully it is available for connection via iSCSI.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFVolume")]
	public class CreateVolume : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Name of the volume.
		/// Not required to be unique, but it is recommended.
		/// May be 1 to 64 characters in length.
		/// </summary>
		private string _name;
		/// <summary>
		/// AccountID for the owner of this volume.
		/// </summary>
		private Int64 _accountID;
		/// <summary>
		/// Total size of the volume, in bytes. Size is rounded up to the nearest 1MB size.
		/// </summary>
		private Int64 _totalSize;
		/// <summary>
		/// Should the volume provides 512-byte sector emulation?
		/// </summary>
		private boolean _enable512e;
		/// <summary>
		/// Initial quality of service settings for this volume.
		/// <br/><br/>
		/// Volumes created without specified QoS values are created with the default values for QoS.
		/// Default values for a volume can be found by running the GetDefaultQoS method.
		/// </summary>
		private QoS _qos;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Name of the volume.', u'Not required to be unique, but it is recommended.', u'May be 1 to 64 characters in length.']")]
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
		[Parameter(HelpMessage = "AccountID for the owner of this volume.")]
		public Int64 AccountID
		{
			get
			{
				return _accountID;
			}
			set
			{
				_accountID = value;
			}
		}
		[Parameter(HelpMessage = "Total size of the volume, in bytes. Size is rounded up to the nearest 1MB size.")]
		public Int64 TotalSize
		{
			get
			{
				return _totalSize;
			}
			set
			{
				_totalSize = value;
			}
		}
		[Parameter(HelpMessage = "Should the volume provides 512-byte sector emulation?")]
		public boolean Enable512e
		{
			get
			{
				return _enable512e;
			}
			set
			{
				_enable512e = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Initial quality of service settings for this volume.', u'<br/><br/>', u'Volumes created without specified QoS values are created with the default values for QoS.', u'Default values for a volume can be found by running the GetDefaultQoS method.']")]
		public QoS Qos
		{
			get
			{
				return _qos;
			}
			set
			{
				_qos = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "List of Name/Value pairs in JSON object format.")]
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
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new CreateVolumeRequest();
			request.Name = _name;
			request.AccountID = _accountID;
			request.TotalSize = _totalSize;
			request.Enable512e = _enable512e;
			request.Qos = _qos;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<CreateVolumeResult>("CreateVolume", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		