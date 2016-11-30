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
	/// CreateInitiators enables you to create multiple new initiator IQNs or World Wide Port Names (WWPNs) and optionally assign them aliases and attributes. When you use CreateInitiators to create new initiators, you can also add them to volume access groups.
	/// If CreateInitiators fails to create one of the initiators provided in the parameter, the method returns an error and does not create any initiators (no partial completion is possible).
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFInitiators")]
	public class CreateInitiators : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A list of Initiator objects containing characteristics of each new initiator
		/// </summary>
		private CreateInitiator[] _initiators;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'A list of Initiator objects containing characteristics of each new initiator']")]
		public CreateInitiator[] Initiators
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
			var request = new CreateInitiatorsRequest();
			request.Initiators = _initiators;
			var objsFromAPI = SendRequest<CreateInitiatorsResult>("CreateInitiators", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		