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
	/// ClearClusterFaults is used to clear information about both current faults that are resolved as well as faults that were previously detected and resolved can be cleared.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Clear, "SFClusterFaults")]
	public class ClearClusterFaults : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Determines the types of faults cleared:<br/>
		/// <b>current</b>: Faults that are currently detected and have not been resolved.<br/>
		/// <b>resolved</b>: Faults that were previously detected and resolved.<br/>
		/// <b>all</b>: Both current and resolved faults are cleared. The fault status can be determined by the "resolved" field of the fault object.
		/// </summary>
		private string _faultTypes;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "[u'Determines the types of faults cleared:<br/>', u'<b>current</b>: Faults that are currently detected and have not been resolved.<br/>', u'<b>resolved</b>: Faults that were previously detected and resolved.<br/>', u'<b>all</b>: Both current and resolved faults are cleared. The fault status can be determined by the \"resolved\" field of the fault object.']")]
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
			var request = new ClearClusterFaultsRequest();
			request.FaultTypes = _faultTypes;
			var objsFromAPI = SendRequest<ClearClusterFaultsResult>("ClearClusterFaults", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		