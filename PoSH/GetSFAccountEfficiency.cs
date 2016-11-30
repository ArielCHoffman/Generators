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
	/// GetAccountEfficiency is used to retrieve information about a volume account. Only the account given as a parameter in this API method is used to compute the capacity.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFAccountEfficiency")]
	public class GetAccountEfficiency : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Specifies the volume account for which capacity is computed.
		/// </summary>
		private Int64 _accountID;
		private boolean _force;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "Specifies the volume account for which capacity is computed.")]
		public Int64 AccountID
		{
			get
			{
				return _accountID;
			}
			set
			{
				_accountID = value;
			}
		}
		[Parameter(Mandatory = false)]
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
			CheckConnection(6);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new GetAccountEfficiencyRequest();
			request.AccountID = _accountID;
			request.Force = _force;
			var objsFromAPI = SendRequest<GetAccountEfficiencyResult>("GetAccountEfficiency", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		