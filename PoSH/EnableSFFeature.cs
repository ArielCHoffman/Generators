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
	/// EnableFeature allows you to enable cluster features that are disabled by default.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Enable, "SFFeature")]
	public class EnableFeature : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Valid values: vvols: Enable the Virtual Volumes (VVOLs) cluster feature.
		/// </summary>
		private string _feature;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Valid values: vvols: Enable the Virtual Volumes (VVOLs) cluster feature.']")]
		public string Feature
		{
			get
			{
				return _feature;
			}
			set
			{
				_feature = value;
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
			var request = new EnableFeatureRequest();
			request.Feature = _feature;
			var objsFromAPI = SendRequest<EnableFeatureResult>("EnableFeature", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		