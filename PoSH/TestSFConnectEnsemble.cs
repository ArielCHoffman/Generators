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
	/// The TestConnectEnsemble API method is used to verify connectivity with a sepcified database ensemble. By default it uses the ensemble for the cluster the node is associated with. Alternatively you can provide a different ensemble to test connectivity with.
	/// <br/><b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Test, "SFConnectEnsemble")]
	public class TestConnectEnsemble : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A comma-separated list of ensemble node CIPs for connectivity testing
		/// </summary>
		private string _ensemble;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "A comma-separated list of ensemble node CIPs for connectivity testing")]
		public string Ensemble
		{
			get
			{
				return _ensemble;
			}
			set
			{
				_ensemble = value;
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
			var request = new TestConnectEnsembleRequest();
			request.Ensemble = _ensemble;
			var objsFromAPI = SendRequest<TestConnectEnsembleResult>("TestConnectEnsemble", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		