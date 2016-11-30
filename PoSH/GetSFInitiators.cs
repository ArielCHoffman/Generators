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
	/// ListInitiators enables you to list initiator IQNs or World Wide Port Names (WWPNs).
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFInitiators")]
	public class ListInitiators : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The initiator ID at which to begin the listing. You can supply this parameter or the "initiators" parameter, but not both.
		/// </summary>
		private Int64 _startInitiatorID;
		/// <summary>
		/// The maximum number of initiator objects to return.
		/// </summary>
		private Int64 _limit;
		/// <summary>
		/// A list of initiator IDs to retrieve. You can supply this parameter or the "startInitiatorID" parameter, but not both.
		/// </summary>
		private Int64[] _initiators;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "The initiator ID at which to begin the listing. You can supply this parameter or the \"initiators\" parameter, but not both.")]
		public Int64 StartInitiatorID
		{
			get
			{
				return _startInitiatorID;
			}
			set
			{
				_startInitiatorID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The maximum number of initiator objects to return.")]
		public Int64 Limit
		{
			get
			{
				return _limit;
			}
			set
			{
				_limit = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "A list of initiator IDs to retrieve. You can supply this parameter or the \"startInitiatorID\" parameter, but not both.")]
		public Int64[] Initiators
		{
			get
			{
				return _initiators;
			}
			set
			{
				_initiators = value;
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
			var request = new ListInitiatorsRequest();
			request.StartInitiatorID = _startInitiatorID;
			request.Limit = _limit;
			request.Initiators = _initiators;
			var objsFromAPI = SendRequest<ListInitiatorsResult>("ListInitiators", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		