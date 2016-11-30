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
	/// GetCurrentClusterAdmin returns information for the current primary cluster administrator. The primary Cluster Admin was ncreated when the cluster was created.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFCurrentClusterAdmin")]
	public class GetCurrentClusterAdmin : SFCmdlet
	{
		#region Private Data
		#endregion
		
		#region Parameters
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var objsFromAPI = SendRequest<GetCurrentClusterAdminResult>("GetCurrentClusterAdmin");
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		