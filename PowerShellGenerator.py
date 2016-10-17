import os
import json
import glob
import copy
import re

def getCmdletName(jsonName):
	return re.sub('(.)([A-Z].*)', r'\1-\2', jsonName, count=1)
	
def getClassName(jsonName):
	return jsonName

def getCmdletVerb(jsonName):
	return re.sub('(.)[A-Z].*', r'\1', jsonName, count=1)
	
def getCmdletObject(jsonName):
	return re.sub('.([A-Z].*)', r'\1', jsonName, count=1)

def getCmdletAttribute(cmdletName):
	verbAndObject = cmdletName.split("-")
	return "[Cmdlet(VerbsCommon."+verbAndObject[0]+", \""+verbAndObject[1]+"\")]"
	
def getPrivateParameterName(paramName):
	return "_"+paramName[0].lower()+paramName[1:]
	
def getPublicParameterName(paramName):
	return paramName[0].capitalize()+paramName[1:]

def getPrivateParameterType(paramType):
	if(type(paramType) is dict):
		return paramType["name"]
	else:
		return paramType
	
def getNamespace(file):
	return "SolidFire."+file.split(".")[0]
	
# Each of these corresponds to a parameter
def generatePrivateDataRegion(method, types):
	pass
	
def getParameterAttributeOptions(parameter):
	parameterAttributeOptions = []
	if("optional" in parameter["type"].keys()):
		parameterAttributeOptions += ["Mandatory = " + str(parameter["type"]["optional"])]
	if("documentation" in parameter.keys()):
		parameterAttributeOptions += ["HelpMessage = \"" + re.sub('"', '\"', str(parameter["documentation"]) + "\"")]
	return parameterAttributeOptions

def generatePowerShellCommandFromMethod(method, file):
	PSCommand = """#region Copyright
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
using System.ComponentModel;
using System.Management.Automation;
using SolidFire.Core.Validation;
using SolidFire.Core;
using SolidFire.Element.Api;
#endregion

namespace """ + getNamespace(file) + """
{"""
	if("documentation" in method.keys()):
		PSCommand += """
	/// <summary>
	/// """+method["documentation"]+"""
	/// </summary>"""
	PSCommand += """
	[RunInstaller(true)]
	[Cmdlet(VerbsCommon."""+getCmdletVerb(method["name"])+""", \""""+getCmdletObject(method["name"])+"""\")]
	public class """+getClassName(method["name"])+""" : SFCmdlet
	{
		#region Private Data"""
	if("params" in method.keys()):
		for parameter in method["params"]:
			if("documentation" in parameter.keys()):
				PSCommand += """
		/// <summary>
		/// """+method["documentation"]+"""
		/// </summary>"""
			PSCommand += """
		private """+getPrivateParameterType(parameter["type"])+""" """+getPrivateParameterName(parameter["name"])
	PSCommand += """
		#endregion
		
		#region Parameters"""
	if("params" in method.keys()):
		for parameter in method["params"]:
			PSCommand += """
		[Parameter("""+",".join(getParameterAttributeOptions(parameter))+""")]
		public """+getPrivateParameterType(parameter["type"])+""" """+getPublicParameterName(parameter["name"])+"""
		{
			get
			{
				return """+getPrivateParameterName(parameter["name"])+""";
			}
			set
			{
				"""+getPrivateParameterName(parameter["name"])+""" = value;
			}
		}"""
	PSCommand += """
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
			var request = new """+getClassName(method["name"])+"""Request();"""
	if("params" in method.keys()):
		for parameter in method["params"]:
			PSCommand += """
			request."""+getPublicParameterName(parameter["name"])+""" = """+getPrivateParameterName(parameter["name"])+""";"""
	PSCommand += """
			var objsFromAPI = SendRequest<"""+getClassName(method["name"])+"""Result>(\""""+getClassName(method["name"])+"""\", request);
			
			WriteObject(objsFromAPI.Result, true);
		}
		#endregion
    }
}
		"""
	return PSCommand

	
def generatePowerShellCommandsFromJSON(json, file):
	types = dict()
	# First, convert the types to a dictionary of types for easy access
	for type in json["types"]:
		types[type["name"]] = type
	
	# Each method is going to get its own file, but only if it is public
	for method in json["methods"]:
		if(u'release' not in method.keys()):
			continue
		if(method[u'release'] != "Public"):
			continue
		cmdletName = getCmdletName(method["name"])
		newFilePath = "JSONInputForPoSHGen\\"+file.split('.')[0]+"\\"+method["name"]+".cs"
		print(newFilePath)
		with open(newFilePath, 'w') as f:
			f.write(generatePowerShellCommandFromMethod(method, file))

rootdir = "JSONInputForPoSHGen"
for root, subFolders, files in os.walk(rootdir):
	for file in files:
		if(file.endswith(".json")):
			filePath = os.path.join(rootdir, file)
			with open( filePath, 'r' ) as f:
				data = f.read()
			fileJson = json.loads(data)
			generatePowerShellCommandsFromJSON(fileJson, file)
			break
		
		