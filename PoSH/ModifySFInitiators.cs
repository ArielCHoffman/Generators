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
	/// ModifyInitiators enables you to change the attributes of an existing initiator. You cannot change the name of an existing initiator. If you need to change the name of an initiator, delete the existing initiator with DeleteInitiators and create a new one with CreateInitiators.
	/// If ModifyInitiators fails to change one of the initiators provided in the parameter, the method returns an error and does not create any initiators (no partial completion is possible).
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFInitiators")]
	public class ModifyInitiators : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A list of Initiator objects containing characteristics of each initiator to modify.
		/// </summary>
		private ModifyInitiator[] _initiators;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'A list of Initiator objects containing characteristics of each initiator to modify.']")]
		public ModifyInitiator[] Initiators
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
			var request = new ModifyInitiatorsRequest();
			request.Initiators = _initiators;
			var objsFromAPI = SendRequest<ModifyInitiatorsResult>("ModifyInitiators", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		