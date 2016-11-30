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
	/// GetSchedule is used to return information about a scheduled snapshot that has been created. You can see information about a specified schedule if there are many snapshot schedules in the system. You can include more than one schedule with this method by specifying additional scheduleIDs to the parameter.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFSchedule")]
	public class GetSchedule : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique ID of the schedule or multiple schedules to display
		/// </summary>
		private Int64 _scheduleID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Unique ID of the schedule or multiple schedules to display']")]
		public Int64 ScheduleID
		{
			get
			{
				return _scheduleID;
			}
			set
			{
				_scheduleID = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(8);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new GetScheduleRequest();
			request.ScheduleID = _scheduleID;
			var objsFromAPI = SendRequest<GetScheduleResult>("GetSchedule", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		