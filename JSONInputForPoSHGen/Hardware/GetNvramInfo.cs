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
	/// GetNvramInfo allows you to retrieve information from each node about the NVRAM card.  
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "GeNvramInfo")]
	public class GetNvramInfo : SFCmdlet
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
			var request = new GetNvramInfoRequest();
			var objsFromAPI = SendRequest<GetNvramInfoResult>("GetNvramInfo", request);
			
			WriteObject(objsFromAPI.Result, true);
		}
		#endregion
    }
}
		