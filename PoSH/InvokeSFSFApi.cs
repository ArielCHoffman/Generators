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
	/// This will invoke any API method supported by the SolidFire API for the version and port the connection is using.
	/// Returns a nested hashtable of key/value pairs that contain the result of the invoked method.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Invoke, "SFSFApi")]
	public class InvokeSFApi : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// The name of the method to invoke. This is case sensitive.
		/// </summary>
		private string _method;
		/// <summary>
		/// An object, normally a dictionary or hashtable of the key/value pairs, to be passed as the params for the method being invoked.
		/// </summary>
		private object _parameters;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "The name of the method to invoke. This is case sensitive.")]
		public string Method
		{
			get
			{
				return _method;
			}
			set
			{
				_method = value;
			}
		}
		[Parameter(Mandatory = false, HelpMessage = "[u'An object, normally a dictionary or hashtable of the key/value pairs, to be passed as the params for the method being invoked.']")]
		public object Parameters
		{
			get
			{
				return _parameters;
			}
			set
			{
				_parameters = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection();
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new InvokeSFApiRequest();
			request.Method = _method;
			request.Parameters = _parameters;
			var objsFromAPI = SendRequest<InvokeSFApiResult>("InvokeSFApi", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		