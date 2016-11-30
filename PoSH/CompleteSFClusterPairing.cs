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
	/// The CompleteClusterPairing method is the second step in the cluster pairing process.
	/// Use this method with the encoded key received from the "StartClusterPairing" API method to complete the cluster pairing process.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Complete, "SFClusterPairing")]
	public class CompleteClusterPairing : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A string of characters that is returned from the "StartClusterPairing" API method.
		/// </summary>
		private string _clusterPairingKey;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "A string of characters that is returned from the \"StartClusterPairing\" API method.")]
		public string ClusterPairingKey
		{
			get
			{
				return _clusterPairingKey;
			}
			set
			{
				_clusterPairingKey = value;
			}
		}
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
			var request = new CompleteClusterPairingRequest();
			request.ClusterPairingKey = _clusterPairingKey;
			var objsFromAPI = SendRequest<CompleteClusterPairingResult>("CompleteClusterPairing", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		