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
	/// The TestPing API method is used to validate the connection to all nodes in the cluster on both 1G and 10G interfaces using ICMP packets. The test uses the appropriate MTU sizes for each packet based on the MTU settings in the network configuration.
	/// <br/><b>Note</b>: This method is available only through the per-node API endpoint 5.0 or later.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Test, "SFPing")]
	public class TestPing : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specifies the number of times the system should repeat the test ping. Default is 5.
		/// </summary>
		private Int64 _attempts;
		/// <summary>
		/// Specify address or hostnames of devices to ping.
		/// </summary>
		private string _hosts;
		/// <summary>
		/// Specifies the length of time the ping should wait for a system response before issuing the next ping attempt or ending the process.
		/// </summary>
		private Int64 _totalTimeoutSec;
		/// <summary>
		/// Specify the number of bytes to send in the ICMP packet sent to each IP. Number be less than the maximum MTU specified in the network configuration.
		/// </summary>
		private Int64 _packetSize;
		/// <summary>
		/// Specify the number of milliseconds to wait for each individual ping response. Default is 500ms.
		/// </summary>
		private Int64 _pingTimeoutMsec;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Specifies the number of times the system should repeat the test ping. Default is 5.")]
		public Int64 Attempts
		{
			get
			{
				return _attempts;
			}
			set
			{
				_attempts = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Specify address or hostnames of devices to ping.")]
		public string Hosts
		{
			get
			{
				return _hosts;
			}
			set
			{
				_hosts = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Specifies the length of time the ping should wait for a system response before issuing the next ping attempt or ending the process.")]
		public Int64 TotalTimeoutSec
		{
			get
			{
				return _totalTimeoutSec;
			}
			set
			{
				_totalTimeoutSec = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Specify the number of bytes to send in the ICMP packet sent to each IP. Number be less than the maximum MTU specified in the network configuration.")]
		public Int64 PacketSize
		{
			get
			{
				return _packetSize;
			}
			set
			{
				_packetSize = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Specify the number of milliseconds to wait for each individual ping response. Default is 500ms.")]
		public Int64 PingTimeoutMsec
		{
			get
			{
				return _pingTimeoutMsec;
			}
			set
			{
				_pingTimeoutMsec = value;
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
			var request = new TestPingRequest();
			request.Attempts = _attempts;
			request.Hosts = _hosts;
			request.TotalTimeoutSec = _totalTimeoutSec;
			request.PacketSize = _packetSize;
			request.PingTimeoutMsec = _pingTimeoutMsec;
			var objsFromAPI = SendRequest<TestPingResult>("TestPing", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		