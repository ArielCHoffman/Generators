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
	/// Cancels a currently running clone operation.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Cancel, "SFClone")]
	public class CancelClone : SFCmdlet
	{
		#region Private Data
		private Int64 _cloneID;
		#endregion
		
		#region Parameters
		[Parameter()]
		public Int64 CloneID
		{
			get
			{
				return _cloneID;
			}
			set
			{
				_cloneID = value;
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
			var request = new CancelCloneRequest();
			request.CloneID = _cloneID;
			var objsFromAPI = SendRequest<CancelCloneResult>("CancelClone", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		