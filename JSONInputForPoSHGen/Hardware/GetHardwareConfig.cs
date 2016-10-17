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
using System.ComponentModel;
using System.Management.Automation;
using SolidFire.Core.Validation;
using SolidFire.Core;
using SolidFire.Element.Api;
#endregion

namespace SolidFire.Hardware
{
	/// <summary>
	/// GetHardwareConfig enables you to display the hardware configuration information for a node. NOTE: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "GeHardwareConfig")]
	public class GetHardwareConfig : SFCmdlet
	{
		#region Private Data
		#endregion
		
		#region Parameters
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection();
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new GetHardwareConfigRequest();
			var objsFromAPI = SendRequest<GetHardwareConfigResult>("GetHardwareConfig", request);
			
			WriteObject(objsFromAPI.Result, true);
		}
		#endregion
    }
}
		