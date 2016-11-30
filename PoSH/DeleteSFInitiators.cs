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
	/// DeleteInitiators enables you to delete one or more initiators from the system (and from any associated volumes or volume access groups).
	/// If DeleteInitiators fails to delete one of the initiators provided in the parameter, the system returns an error and does not delete any initiators (no partial completion is possible).
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Delete, "SFInitiators")]
	public class DeleteInitiators : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// An array of IDs of initiators to delete.
		/// </summary>
		private Int64[] _initiators;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'An array of IDs of initiators to delete.']")]
		public Int64[] Initiators
		{
			get
			{
				return _initiators;
			}
			set
			{
				_initiators = value;
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
			var request = new DeleteInitiatorsRequest();
			request.Initiators = _initiators;
			var objsFromAPI = SendRequest<DeleteInitiatorsResult>("DeleteInitiators", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		