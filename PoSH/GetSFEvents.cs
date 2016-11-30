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
	/// ListEvents returns events detected on the cluster, sorted from oldest to newest.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFEvents")]
	public class ListEvents : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specifies the maximum number of events to return.
		/// </summary>
		private Int64 _maxEvents;
		/// <summary>
		/// Identifies the beginning of a range of events to return.
		/// </summary>
		private Int64 _startEventID;
		/// <summary>
		/// Identifies the end of a range of events to return.
		/// </summary>
		private Int64 _endEventID;
		private string _eventQueueType;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "Specifies the maximum number of events to return.")]
		public Int64 MaxEvents
		{
			get
			{
				return _maxEvents;
			}
			set
			{
				_maxEvents = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Identifies the beginning of a range of events to return.")]
		public Int64 StartEventID
		{
			get
			{
				return _startEventID;
			}
			set
			{
				_startEventID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "Identifies the end of a range of events to return.")]
		public Int64 EndEventID
		{
			get
			{
				return _endEventID;
			}
			set
			{
				_endEventID = value;
			}
		}
		[Parameter(Mandatory = false)]
		public string EventQueueType
		{
			get
			{
				return _eventQueueType;
			}
			set
			{
				_eventQueueType = value;
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
			var request = new ListEventsRequest();
			request.MaxEvents = _maxEvents;
			request.StartEventID = _startEventID;
			request.EndEventID = _endEventID;
			request.EventQueueType = _eventQueueType;
			var objsFromAPI = SendRequest<ListEventsResult>("ListEvents", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		