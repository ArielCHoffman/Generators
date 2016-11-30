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
	/// Creates a new VVols storage container.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Create, "SFStorageContainer")]
	public class CreateStorageContainer : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Name of the storage container.
		/// </summary>
		private string _name;
		/// <summary>
		/// The secret for CHAP authentication for the initiator
		/// </summary>
		private string _initiatorSecret;
		/// <summary>
		/// The secret for CHAP authentication for the target
		/// </summary>
		private string _targetSecret;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Name of the storage container.")]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The secret for CHAP authentication for the initiator")]
		public string InitiatorSecret
		{
			get
			{
				return _initiatorSecret;
			}
			set
			{
				_initiatorSecret = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "The secret for CHAP authentication for the target")]
		public string TargetSecret
		{
			get
			{
				return _targetSecret;
			}
			set
			{
				_targetSecret = value;
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
			var request = new CreateStorageContainerRequest();
			request.Name = _name;
			request.InitiatorSecret = _initiatorSecret;
			request.TargetSecret = _targetSecret;
			var objsFromAPI = SendRequest<CreateStorageContainerResult>("CreateStorageContainer", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		