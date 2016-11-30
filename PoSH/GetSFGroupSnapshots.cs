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
	/// ListGroupSnapshots is used to return information about all group snapshots that have been created.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFGroupSnapshots")]
	public class ListGroupSnapshots : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// An array of unique volume IDs to query.
		/// If this parameter is not specified, all group snapshots on the cluster will be included.
		/// </summary>
		private Int64 _volumeID;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "[u'An array of unique volume IDs to query.', u'If this parameter is not specified, all group snapshots on the cluster will be included.']")]
		public Int64 VolumeID
		{
			get
			{
				return _volumeID;
			}
			set
			{
				_volumeID = value;
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
			var request = new ListGroupSnapshotsRequest();
			request.VolumeID = _volumeID;
			var objsFromAPI = SendRequest<ListGroupSnapshotsResult>("ListGroupSnapshots", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		