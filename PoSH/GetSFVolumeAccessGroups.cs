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
	/// ListVolumeAccessGroups is used to return information about the volume access groups that are currently in the system.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFVolumeAccessGroups")]
	public class ListVolumeAccessGroups : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The lowest VolumeAccessGroupID to return.
		/// This can be useful for paging.
		/// If unspecified, there is no lower limit (implicitly 0).
		/// </summary>
		private Int64 _startVolumeAccessGroupID;
		/// <summary>
		/// The maximum number of results to return.
		/// This can be useful for paging.
		/// </summary>
		private Int64 _limit;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "[u'The lowest VolumeAccessGroupID to return.', u'This can be useful for paging.', u'If unspecified, there is no lower limit (implicitly 0).']")]
		public Int64 StartVolumeAccessGroupID
		{
			get
			{
				return _startVolumeAccessGroupID;
			}
			set
			{
				_startVolumeAccessGroupID = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'The maximum number of results to return.', u'This can be useful for paging.']")]
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
			var request = new ListVolumeAccessGroupsRequest();
			request.StartVolumeAccessGroupID = _startVolumeAccessGroupID;
			request.Limit = _limit;
			var objsFromAPI = SendRequest<ListVolumeAccessGroupsResult>("ListVolumeAccessGroups", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		