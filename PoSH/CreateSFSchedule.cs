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
	/// CreateSchedule is used to create a schedule that will autonomously make a snapshot of a volume at a defined interval.<br/>
	/// <br/>
	/// The snapshot created can be used later as a backup or rollback to ensure the data on a volume or group of volumes is consistent for the point in time in which the snapshot was created. <br/>
	/// <br/>
	/// <b>Note</b>: Creating a snapshot is allowed if cluster fullness is at stage 2 or 3. Snapshots are not created when cluster fullness is at stage 4 or 5.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFSchedule")]
	public class CreateSchedule : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The "Schedule" object will be used to create a new schedule.<br/>
		/// Do not set ScheduleID property, it will be ignored.<br/>
		/// Frequency property must be of type that inherits from Frequency. Valid types are:<br/>
		/// DaysOfMonthFrequency<br/>
		/// DaysOrWeekFrequency<br/>
		/// TimeIntervalFrequency
		/// </summary>
		private Schedule _schedule;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'The \"Schedule\" object will be used to create a new schedule.<br/>', u'Do not set ScheduleID property, it will be ignored.<br/>', u'Frequency property must be of type that inherits from Frequency. Valid types are:<br/>', u'DaysOfMonthFrequency<br/>', u'DaysOrWeekFrequency<br/>', u'TimeIntervalFrequency']")]
		public Schedule Schedule
		{
			get
			{
				return _schedule;
			}
			set
			{
				_schedule = value;
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
			var request = new CreateScheduleRequest();
			request.Schedule = _schedule;
			var objsFromAPI = SendRequest<CreateScheduleResult>("CreateSchedule", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		