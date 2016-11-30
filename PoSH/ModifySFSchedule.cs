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
	/// ModifySchedule is used to change the intervals at which a scheduled snapshot occurs. This allows for adjustment to the snapshot frequency and retention.<br/>
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFSchedule")]
	public class ModifySchedule : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The "Schedule" object will be used to modify an existing schedule.<br/>
		/// The ScheduleID property is required.<br/>
		/// Frequency property must be of type that inherits from Frequency. Valid types are:<br/>
		/// DaysOfMonthFrequency<br/>
		/// DaysOrWeekFrequency<br/>
		/// TimeIntervalFrequency
		/// </summary>
		private Schedule _schedule;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'The \"Schedule\" object will be used to modify an existing schedule.<br/>', u'The ScheduleID property is required.<br/>', u'Frequency property must be of type that inherits from Frequency. Valid types are:<br/>', u'DaysOfMonthFrequency<br/>', u'DaysOrWeekFrequency<br/>', u'TimeIntervalFrequency']")]
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
			var request = new ModifyScheduleRequest();
			request.Schedule = _schedule;
			var objsFromAPI = SendRequest<ModifyScheduleResult>("ModifySchedule", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		