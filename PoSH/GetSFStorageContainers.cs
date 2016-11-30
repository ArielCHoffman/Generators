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
	/// Gets information for all storage containers currently in the system.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFStorageContainers")]
	public class ListStorageContainers : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of storage containers to get
		/// </summary>
		private Guid[] _storageContainerIDs;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "List of storage containers to get")]
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
			var request = new ListStorageContainersRequest();
			request.StorageContainerIDs = _storageContainerIDs;
			var objsFromAPI = SendRequest<ListStorageContainersResult>("ListStorageContainers", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		