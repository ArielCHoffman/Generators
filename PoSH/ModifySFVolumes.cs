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
	/// ModifyVolumes allows you to configure up to 500 existing volumes at one time. Changes take place immediately. If ModifyVolumes fails to modify any of the specified volumes, none of the specified volumes are changed.If you do not specify QoS values when you modify volumes, the QoS values for each volume remain unchanged. You can retrieve default QoS values for a newly created volume by running the GetDefaultQoS method.When you need to increase the size of volumes that are being replicated, do so in the following order to prevent replication errors:Increase the size of the "Replication Target" volume.Increase the size of the source or "Read / Write" volume. recommends that both the target and source volumes be the same size.NOTE: If you change access status to locked or replicationTarget all existing iSCSI connections are terminated.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFVolumes")]
	public class ModifyVolumes : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A list of volumeIDs for the volumes to be modified.
		/// </summary>
		private Int64[] _volumeIDs;
		/// <summary>
		/// AccountID to which the volume is reassigned. If none is specified, the previous account name is used.
		/// </summary>
		private Int64 _accountID;
		/// <summary>
		/// Access allowed for the volume. Possible values:readOnly: Only read operations are allowed.readWrite: Reads and writes are allowed.locked: No reads or writes are allowed.If not specified, the access value does not change.replicationTarget: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.If a value is not specified, the access value does not change. 
		/// </summary>
		private string _access;
		private hashtable _attributes;
		/// <summary>
		/// Volume replication mode. Possible values:asynch: Waits for system to acknowledge that data is stored on source before writing to the target.sync: Does not wait for data transmission acknowledgment from source to begin writing data to the target.
		/// </summary>
		private string _mode;
		/// <summary>
		/// New quality of service settings for this volume.If not specified, the QoS settings are not changed.
		/// </summary>
		private QoS _qos;
		/// <summary>
		/// New size of the volume in bytes. 1000000000 is equal to 1GB. Size is rounded up to the nearest 1MB in size. This parameter can only be used to increase the size of a volume.
		/// </summary>
		private Int64 _totalSize;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "A list of volumeIDs for the volumes to be modified.")]
		public Int64[] VolumeIDs
		{
			get
			{
				return _volumeIDs;
			}
			set
			{
				_volumeIDs = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "AccountID to which the volume is reassigned. If none is specified, the previous account name is used.")]
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
		[Parameter(Mandatory = false, HelpMessage = "Access allowed for the volume. Possible values:readOnly: Only read operations are allowed.readWrite: Reads and writes are allowed.locked: No reads or writes are allowed.If not specified, the access value does not change.replicationTarget: Identify a volume as the target volume for a paired set of volumes. If the volume is not paired, the access status is locked.If a value is not specified, the access value does not change. ")]
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
		[Parameter(Mandatory = false)]
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
		[Parameter(Mandatory = false, HelpMessage = "Volume replication mode. Possible values:asynch: Waits for system to acknowledge that data is stored on source before writing to the target.sync: Does not wait for data transmission acknowledgment from source to begin writing data to the target.")]
		public string Mode
		{
			get
			{
				return _mode;
			}
			set
			{
				_mode = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "New quality of service settings for this volume.If not specified, the QoS settings are not changed.")]
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
		[Parameter(Mandatory = false, HelpMessage = "New size of the volume in bytes. 1000000000 is equal to 1GB. Size is rounded up to the nearest 1MB in size. This parameter can only be used to increase the size of a volume.")]
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
			var request = new ModifyVolumesRequest();
			request.VolumeIDs = _volumeIDs;
			request.AccountID = _accountID;
			request.Access = _access;
			request.Attributes = _attributes;
			request.Mode = _mode;
			request.Qos = _qos;
			request.TotalSize = _totalSize;
			var objsFromAPI = SendRequest<ModifyVolumesResult>("ModifyVolumes", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		