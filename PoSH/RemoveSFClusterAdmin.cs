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
	/// RemoveClusterAdmin is used to remove a Cluster Admin. The "admin" Cluster Admin cannot be removed.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFClusterAdmin")]
	public class RemoveClusterAdmin : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// ClusterAdminID for the Cluster Admin to remove.
		/// </summary>
		private Int64 _clusterAdminID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "ClusterAdminID for the Cluster Admin to remove.")]
		public Int64 ClusterAdminID
		{
			get
			{
				return _clusterAdminID;
			}
			set
			{
				_clusterAdminID = value;
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
			var request = new RemoveClusterAdminRequest();
			request.ClusterAdminID = _clusterAdminID;
			var objsFromAPI = SendRequest<RemoveClusterAdminResult>("RemoveClusterAdmin", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		