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
	/// SetDefaultQoS enables you to configure the default Quality of Service (QoS) values (measured in inputs and outputs per second, or IOPS) for a volume. For more information on QoS in a  cluster, see the . 
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Set, "SFDefaultQoS")]
	public class SetDefaultQoS : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The minimum number of sustained IOPS that are provided by the cluster to a volume.
		/// </summary>
		private Int64 _minIOPS;
		/// <summary>
		/// The maximum number of sustained IOPS that are provided by the cluster to a volume.
		/// </summary>
		private Int64 _maxIOPS;
		/// <summary>
		/// The maximum number of IOPS allowed in a short burst scenario.
		/// </summary>
		private Int64 _burstIOPS;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "The minimum number of sustained IOPS that are provided by the cluster to a volume.")]
		public Int64 MinIOPS
		{
			get
			{
				return _minIOPS;
			}
			set
			{
				_minIOPS = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The maximum number of sustained IOPS that are provided by the cluster to a volume.")]
		public Int64 MaxIOPS
		{
			get
			{
				return _maxIOPS;
			}
			set
			{
				_maxIOPS = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The maximum number of IOPS allowed in a short burst scenario.")]
		public Int64 BurstIOPS
		{
			get
			{
				return _burstIOPS;
			}
			set
			{
				_burstIOPS = value;
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
			var request = new SetDefaultQoSRequest();
			request.MinIOPS = _minIOPS;
			request.MaxIOPS = _maxIOPS;
			request.BurstIOPS = _burstIOPS;
			var objsFromAPI = SendRequest<SetDefaultQoSResult>("SetDefaultQoS", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		