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
	/// RemoveBackupTarget allows you to delete backup targets.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Remove, "SFBackupTarget")]
	public class RemoveBackupTarget : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique target ID of the target to remove.
		/// </summary>
		private Int64 _backupTargetID;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Unique target ID of the target to remove.']")]
		public Int64 BackupTargetID
		{
			get
			{
				return _backupTargetID;
			}
			set
			{
				_backupTargetID = value;
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
			var request = new RemoveBackupTargetRequest();
			request.BackupTargetID = _backupTargetID;
			var objsFromAPI = SendRequest<RemoveBackupTargetResult>("RemoveBackupTarget", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		