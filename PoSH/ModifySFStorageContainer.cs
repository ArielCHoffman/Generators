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
	/// Modifies an existing storage container.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFStorageContainer")]
	public class ModifyStorageContainer : SFCmdlet
	{
		#region Private Data
		private Guid _storageContainerID;
		private string _initiatorSecret;
		private string _targetSecret;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false)]
		public Guid StorageContainerID
		{
			get
			{
				return _storageContainerID;
			}
			set
			{
				_storageContainerID = value;
			}
		}
		[Parameter(Mandatory = false)]
		public string InitiatorSecret
		{
			get
			{
				return _initiatorSecret;
			}
			set
			{
				_initiatorSecret = value;
			}
		}
		[Parameter(Mandatory = false)]
		public string TargetSecret
		{
			get
			{
				return _targetSecret;
			}
			set
			{
				_targetSecret = value;
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
			var request = new ModifyStorageContainerRequest();
			request.StorageContainerID = _storageContainerID;
			request.InitiatorSecret = _initiatorSecret;
			request.TargetSecret = _targetSecret;
			var objsFromAPI = SendRequest<ModifyStorageContainerResult>("ModifyStorageContainer", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		