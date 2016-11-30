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
	/// ListClusterFaults is used to retrieve information about any faults detected on the cluster.
	/// With this method, both current and resolved faults can be retrieved. The system caches faults every 30 seconds.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFClusterFaults")]
	public class ListClusterFaults : SFCmdlet
	{
		#region Private Data
		private boolean _exceptions;
		/// <summary>
		/// Include faults triggered by sub-optimal system configuration.
		/// Possible values: true, false
		/// </summary>
		private boolean _bestPractices;
		private boolean _update;
		/// <summary>
		/// Determines the types of faults returned: current: List active, unresolved faults.
		/// <b>resolved</b>: List faults that were previously detected and resolved.
		/// <b>all</b>: (Default) List both current and resolved faults. You can see the fault status in the 'resolved' field of the Cluster Fault object.
		/// </summary>
		private string _faultTypes;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false)]
		public boolean Exceptions
		{
			get
			{
				return _exceptions;
			}
			set
			{
				_exceptions = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Include faults triggered by sub-optimal system configuration.', u'Possible values: true, false']")]
		public boolean BestPractices
		{
			get
			{
				return _bestPractices;
			}
			set
			{
				_bestPractices = value;
			}
		}
		[Parameter(Mandatory = false)]
		public boolean Update
		{
			get
			{
				return _update;
			}
			set
			{
				_update = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'Determines the types of faults returned: current: List active, unresolved faults.', u'<b>resolved</b>: List faults that were previously detected and resolved.', u\"<b>all</b>: (Default) List both current and resolved faults. You can see the fault status in the 'resolved' field of the Cluster Fault object.\"]")]
		public string FaultTypes
		{
			get
			{
				return _faultTypes;
			}
			set
			{
				_faultTypes = value;
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
			var request = new ListClusterFaultsRequest();
			request.Exceptions = _exceptions;
			request.BestPractices = _bestPractices;
			request.Update = _update;
			request.FaultTypes = _faultTypes;
			var objsFromAPI = SendRequest<ListClusterFaultsResult>("ListClusterFaults", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		