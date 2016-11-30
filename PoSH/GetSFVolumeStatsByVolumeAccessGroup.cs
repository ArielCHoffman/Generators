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
	/// ListVolumeStatsByVolumeAccessGroup is used to get total activity measurements for all of the volumes that are a member of the specified volume access group(s).
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumeStatsByVolumeAccessGroup")]
	public class ListVolumeStatsByVolumeAccessGroup : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// An array of VolumeAccessGroupIDs for which volume activity is returned.
		/// If no VolumeAccessGroupID is specified, stats for all volume access groups is returned.
		/// </summary>
		private Int64[] _volumeAccessGroups;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "[u'An array of VolumeAccessGroupIDs for which volume activity is returned.', u'If no VolumeAccessGroupID is specified, stats for all volume access groups is returned.']")]
		public Int64[] VolumeAccessGroups
		{
			get
			{
				return _volumeAccessGroups;
			}
			set
			{
				_volumeAccessGroups = value;
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
			var request = new ListVolumeStatsByVolumeAccessGroupRequest();
			request.VolumeAccessGroups = _volumeAccessGroups;
			var objsFromAPI = SendRequest<ListVolumeStatsByVolumeAccessGroupResult>("ListVolumeStatsByVolumeAccessGroup", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		