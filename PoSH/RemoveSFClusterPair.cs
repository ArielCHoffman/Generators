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
	/// You can use the RemoveClusterPair method to close the open connections between two paired clusters.<br/>
	/// <b>Note</b>: Before you remove a cluster pair, you must first remove all volume pairing to the clusters with the "RemoveVolumePair" API method.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFClusterPair")]
	public class RemoveClusterPair : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique identifier used to pair two clusters.
		/// </summary>
		private Int64 _clusterPairID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Unique identifier used to pair two clusters.")]
		public Int64 ClusterPairID
		{
			get
			{
				return _clusterPairID;
			}
			set
			{
				_clusterPairID = value;
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
			var request = new RemoveClusterPairRequest();
			request.ClusterPairID = _clusterPairID;
			var objsFromAPI = SendRequest<RemoveClusterPairResult>("RemoveClusterPair", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		