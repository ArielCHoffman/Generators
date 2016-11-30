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
	/// The TestConnectMvip API method is used to test the management connection to the cluster. The test pings the MVIP and executes a simple API method to verify connectivity.
	/// <br/><b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Test, "SFConnectMvip")]
	public class TestConnectMvip : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Optionally, use to test the management connection of a different MVIP. This is not needed to test the connection to the target cluster.
		/// </summary>
		private string _mvip;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Optionally, use to test the management connection of a different MVIP. This is not needed to test the connection to the target cluster.")]
		public string Mvip
		{
			get
			{
				return _mvip;
			}
			set
			{
				_mvip = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(5);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new TestConnectMvipRequest();
			request.Mvip = _mvip;
			var objsFromAPI = SendRequest<TestConnectMvipResult>("TestConnectMvip", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		