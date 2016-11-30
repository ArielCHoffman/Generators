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
	/// The TestConnectSvip API method is used to test the storage connection to the cluster. The test pings the SVIP using ICMP packets and when successful connects as an iSCSI initiator.
	/// <br/><b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Test, "SFConnectSvip")]
	public class TestConnectSvip : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Optionally, use to test the storage connection of a different SVIP. This is not needed to test the connection to the target cluster.
		/// </summary>
		private string _svip;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Optionally, use to test the storage connection of a different SVIP. This is not needed to test the connection to the target cluster.")]
		public string Svip
		{
			get
			{
				return _svip;
			}
			set
			{
				_svip = value;
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
			var request = new TestConnectSvipRequest();
			request.Svip = _svip;
			var objsFromAPI = SendRequest<TestConnectSvipResult>("TestConnectSvip", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		