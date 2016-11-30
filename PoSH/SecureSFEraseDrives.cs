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
	/// SecureEraseDrives is used to remove any residual data from drives that have a status of "available." For example, when replacing a drive at its end-of-life that contained sensitive data.
	/// It uses a Security Erase Unit command to write a predetermined pattern to the drive and resets the encryption key on the drive. The method may take up to two minutes to complete, so it is an asynchronous method.
	/// The GetAsyncResult method can be used to check on the status of the secure erase operation.
	/// <br/><br/>
	/// Use the "ListDrives" method to obtain the driveIDs for the drives you want to secure erase.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Secure, "SFEraseDrives")]
	public class SecureEraseDrives : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// List of driveIDs to secure erase.
		/// </summary>
		private Int64[] _drives;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "List of driveIDs to secure erase.")]
		public Int64[] Drives
		{
			get
			{
				return _drives;
			}
			set
			{
				_drives = value;
			}
		}
		#endregion
		
		#region Cmdlet Overrides
		protected override void BeginProcessing()
		{
			base.BeginProcessing();
			CheckConnection(5);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();
			var request = new SecureEraseDrivesRequest();
			request.Drives = _drives;
			var objsFromAPI = SendRequest<SecureEraseDrivesResult>("SecureEraseDrives", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		