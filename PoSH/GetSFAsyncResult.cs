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
	/// Used to retrieve the result of asynchronous method calls.
	/// Some method calls are long running and do not complete when the initial response is sent.
	/// To obtain the result of the method call, polling with GetAsyncResult is required.
	/// <br/><br/>
	/// GetAsyncResult returns the overall status of the operation (in progress, completed, or error) in a standard fashion,
	/// but the actual data returned for the operation depends on the original method call and the return data is documented with each method.
	/// <br/><br/>
	/// The result for a completed asynchronous method call can only be retrieved once.
	/// Once the final result has been returned, later attempts returns an error.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Get, "SFAsyncResult")]
	public class GetAsyncResult : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// A value that was returned from the original asynchronous method call.
		/// </summary>
		private Int64 _asyncHandle;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "A value that was returned from the original asynchronous method call.")]
		public Int64 AsyncHandle
		{
			get
			{
				return _asyncHandle;
			}
			set
			{
				_asyncHandle = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(1);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new GetAsyncResultRequest();
			request.AsyncHandle = _asyncHandle;
			var objsFromAPI = SendRequest<GetAsyncResultResult>("GetAsyncResult", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		