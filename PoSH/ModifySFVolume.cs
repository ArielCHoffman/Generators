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
	/// ModifyVolume is used to modify settings on an existing volume.
	/// Modifications can be made to one volume at a time and changes take place immediately.
	/// If an optional parameter is left unspecified, the value will not be changed.
	/// <br/><br/>
	/// Extending the size of a volume that is being replicated should be done in an order.
	/// The target (Replication Target) volume should first be increased in size, then the source (Read/Write) volume can be resized.
	/// It is recommended that both the target and the source volumes be the same size.
	/// <br/><br/>
	/// <b>Note</b>: If you change access status to locked or target all existing iSCSI connections are terminated.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFVolume")]
	public class ModifyVolume : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// VolumeID for the volume to be modified.
		/// </summary>
		private Int64 _volumeID;
		/// <summary>
		/// AccountID to which the volume is reassigned.
		/// If none is specified, the previous account name is used.
		/// </summary>
		private Int64 _accountID;
		/// <summary>
		/// Access allowed for the volume.
		/// <br/><b>readOnly</b>: Only read operations are allowed.
		/// <br/><b>readWrite</b>: Reads and writes are allowed.
		/// <br/><b>locked</b>: No reads or writes are allowed.
		/// <br/><b>replicationTarget</b>: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.
		/// <br/><br/>
		/// If unspecified, the access settings of the clone will be the same as the source.
		/// </summary>
		private string _access;
		/// <summary>
		/// New quality of service settings for this volume.
		/// </summary>
		private QoS _qos;
		/// <summary>
		/// New size of the volume in bytes.
		/// Size is rounded up to the nearest 1MiB size.
		/// This parameter can only be used to *increase* the size of a volume.
		/// </summary>
		private Int64 _totalSize;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "VolumeID for the volume to be modified.")]
		public Int64 VolumeID
		{
			get
			{
				return _volumeID;
			}
			set
			{
				_volumeID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'AccountID to which the volume is reassigned.', u'If none is specified, the previous account name is used.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'Access allowed for the volume.', u'<br/><b>readOnly</b>: Only read operations are allowed.', u'<br/><b>readWrite</b>: Reads and writes are allowed.', u'<br/><b>locked</b>: No reads or writes are allowed.', u'<br/><b>replicationTarget</b>: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.', u'<br/><br/>', u'If unspecified, the access settings of the clone will be the same as the source.']")]
		public string Access
		{
			get
			{
				return _access;
			}
			set
			{
				_access = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "New quality of service settings for this volume.")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'New size of the volume in bytes.', u'Size is rounded up to the nearest 1MiB size.', u'This parameter can only be used to *increase* the size of a volume.']")]
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
			var request = new ModifyVolumeRequest();
			request.VolumeID = _volumeID;
			request.AccountID = _accountID;
			request.Access = _access;
			request.Qos = _qos;
			request.TotalSize = _totalSize;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<ModifyVolumeResult>("ModifyVolume", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		