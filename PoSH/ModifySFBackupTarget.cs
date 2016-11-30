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
	/// ModifyBackupTarget is used to change attributes of a backup target.
	/// </summary>
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon.Modify, "SFBackupTarget")]
	public class ModifyBackupTarget : SFCmdlet
	{
		#region Private Data
		/// <summary>
		/// Unique identifier assigned to the backup target.
		/// </summary>
		private Int64 _backupTargetID;
		/// <summary>
		/// Name for the backup target.
		/// </summary>
		private string _name;
		/// <summary>
		/// List of Name/Value pairs in JSON object format.
		/// </summary>
		private hashtable _attributes;
		#endregion
		
		#region Parameters
		[Parameter(HelpMessage = "[u'Unique identifier assigned to the backup target.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "[u'Name for the backup target.']")]
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
		[Parameter(Mandatory = false, HelpMessage = "List of Name/Value pairs in JSON object format.")]
		public hashtable Attributes
		{
			get
			{
				return _attributes;
			}
			set
			{
				_attributes = value;
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
			var request = new ModifyBackupTargetRequest();
			request.BackupTargetID = _backupTargetID;
			request.Name = _name;
			request.Attributes = _attributes;
			var objsFromAPI = SendRequest<ModifyBackupTargetResult>("ModifyBackupTarget", request);
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
		}
		#endregion
	}
}
		