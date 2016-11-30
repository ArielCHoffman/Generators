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
	/// CancelGroupClone enables you to stop an ongoing CloneMultipleVolumes process for a group of clones. When you cancel a group clone operation, the system completes and removes the operation's associated asyncHandle. 
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Cancel, "SFGroupClone")]
	public class CancelGroupClone : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// cloneID for the ongoing clone process.
		/// </summary>
		private Int64 _groupCloneID;
		#endregion
		
		#region Parameters
		[Parameter(Mandatory = false, HelpMessage = "cloneID for the ongoing clone process.")]
		public Int64 GroupCloneID
		{
			get
			{
				return _groupCloneID;
			}
			set
			{
				_groupCloneID = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(9);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new CancelGroupCloneRequest();
			request.GroupCloneID = _groupCloneID;
			var objsFromAPI = SendRequest<CancelGroupCloneResult>("CancelGroupClone", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		