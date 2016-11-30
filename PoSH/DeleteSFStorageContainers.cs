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
	/// Deletes a storage container from the system.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Delete, "SFStorageContainers")]
	public class DeleteStorageContainers : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// list of storageContainerID of the storage container to delete.
		/// </summary>
		private Guid[] _storageContainerIDs;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "list of storageContainerID of the storage container to delete.")]
		public Guid[] StorageContainerIDs
		{
			get
			{
				return _storageContainerIDs;
			}
			set
			{
				_storageContainerIDs = value;
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
			var request = new DeleteStorageContainersRequest();
			request.StorageContainerIDs = _storageContainerIDs;
			var objsFromAPI = SendRequest<DeleteStorageContainersResult>("DeleteStorageContainers", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		