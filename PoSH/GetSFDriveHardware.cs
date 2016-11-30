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
	/// ListDriveHardware returns all the drives connected to a node. Use this method on the cluster to return drive hardware information for all the drives on all nodes.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFDriveHardware")]
	public class ListDriveHardware : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// This must be set to true in order to retrieve the drive hardware stats from the cluster.
		/// </summary>
		private boolean _force;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "This must be set to true in order to retrieve the drive hardware stats from the cluster.")]
		public boolean Force
		{
			get
			{
				return _force;
			}
			set
			{
				_force = value;
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
			var request = new ListDriveHardwareRequest();
			request.Force = _force;
			var objsFromAPI = SendRequest<ListDriveHardwareResult>("ListDriveHardware", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		