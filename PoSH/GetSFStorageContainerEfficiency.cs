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
	/// GetStorageContainerEfficiency enables you to retrieve efficiency information about a virtual volume storage container.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFStorageContainerEfficiency")]
	public class GetStorageContainerEfficiency : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The ID of the storage container for which to retrieve efficiency information.
		/// </summary>
		private Guid _storageContainerID;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "The ID of the storage container for which to retrieve efficiency information.")]
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
			var request = new GetStorageContainerEfficiencyRequest();
			request.StorageContainerID = _storageContainerID;
			var objsFromAPI = SendRequest<GetStorageContainerEfficiencyResult>("GetStorageContainerEfficiency", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		