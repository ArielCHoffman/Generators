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
	/// ModifyClusterFullThreshold is used to change the level at which an event is generated when the storage cluster approaches the capacity utilization requested. The number entered in this setting is used to indicate the number of node failures the system is required to recover from. For example, on a 10 node cluster, if you want to be alerted when the system cannot recover from 3 nodes failures, enter the value of "3". When this number is reached, a message alert is sent to the Event Log in the Cluster Management Console.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFClusterFullThreshold")]
	public class ModifyClusterFullThreshold : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Number of nodes worth of capacity remaining on the cluster that triggers a notification.
		/// </summary>
		private Int64 _stage2AwareThreshold;
		/// <summary>
		/// Percent below "Error" state to raise a cluster "Warning" alert.
		/// </summary>
		private Int64 _stage3BlockThresholdPercent;
		/// <summary>
		/// A value representative of the number of times metadata space can be over provisioned relative to the amount of space available. For example, if there was enough metadata space to store 100 TiB of volumes and this number was set to 5, then 500 TiB worth of volumes could be created.
		/// </summary>
		private Int64 _maxMetadataOverProvisionFactor;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Number of nodes worth of capacity remaining on the cluster that triggers a notification.")]
		public Int64 Stage2AwareThreshold
		{
			get
			{
				return _stage2AwareThreshold;
			}
			set
			{
				_stage2AwareThreshold = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Percent below \"Error\" state to raise a cluster \"Warning\" alert.")]
		public Int64 Stage3BlockThresholdPercent
		{
			get
			{
				return _stage3BlockThresholdPercent;
			}
			set
			{
				_stage3BlockThresholdPercent = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "A value representative of the number of times metadata space can be over provisioned relative to the amount of space available. For example, if there was enough metadata space to store 100 TiB of volumes and this number was set to 5, then 500 TiB worth of volumes could be created.")]
		public Int64 MaxMetadataOverProvisionFactor
		{
			get
			{
				return _maxMetadataOverProvisionFactor;
			}
			set
			{
				_maxMetadataOverProvisionFactor = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ModifyClusterFullThresholdRequest();
			request.Stage2AwareThreshold = _stage2AwareThreshold;
			request.Stage3BlockThresholdPercent = _stage3BlockThresholdPercent;
			request.MaxMetadataOverProvisionFactor = _maxMetadataOverProvisionFactor;
			var objsFromAPI = SendRequest<ModifyClusterFullThresholdResult>("ModifyClusterFullThreshold", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		