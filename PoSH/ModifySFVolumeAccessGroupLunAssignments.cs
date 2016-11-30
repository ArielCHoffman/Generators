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
	/// The ModifytVolumeAccessGroupLunAssignments is used to define custom LUN assignments for specific volumes. Only LUN values set on the lunAssignments parameter will be changed in the volume access group. All other LUN assignments will remain unchanged.
	/// <br/><br/>
	/// LUN assignment values must be unique for volumes in a volume access group. An exception will be seen if LUN assignments are duplicated in a volume access group. However, the same LUN values can be used again in different volume access groups.
	/// <br/><br/>
	/// <b>Note:</b> Correct LUN values are 0 - 16383. An exception will be seen if an incorrect LUN value is passed. None of the specified LUN assignments will be modified if there is an exception.
	/// <br/><br/>
	/// <b>Caution:</b> If a LUN assignment is changed for a volume with active I/O, the I/O could be disrupted. Changes to the server configuration may be required in order to change volume LUN assignments.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFVolumeAccessGroupLunAssignments")]
	public class ModifyVolumeAccessGroupLunAssignments : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique volume access group ID for which the LUN assignments will be modified.
		/// </summary>
		private Int64 _volumeAccessGroupID;
		/// <summary>
		/// The volume IDs with new assigned LUN values.
		/// </summary>
		private LunAssignment[] _lunAssignments;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Unique volume access group ID for which the LUN assignments will be modified.")]
		public Int64 VolumeAccessGroupID
		{
			get
			{
				return _volumeAccessGroupID;
			}
			set
			{
				_volumeAccessGroupID = value;
			}
		}
		[Parameter(HelpMessage = "The volume IDs with new assigned LUN values.")]
		public LunAssignment[] LunAssignments
		{
			get
			{
				return _lunAssignments;
			}
			set
			{
				_lunAssignments = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(7);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new ModifyVolumeAccessGroupLunAssignmentsRequest();
			request.VolumeAccessGroupID = _volumeAccessGroupID;
			request.LunAssignments = _lunAssignments;
			var objsFromAPI = SendRequest<ModifyVolumeAccessGroupLunAssignmentsResult>("ModifyVolumeAccessGroupLunAssignments", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		