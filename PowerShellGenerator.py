import os
import json
import glob
import copy
import re

def getCmdletName(jsonName):
	return getCmdletVerb(jsonName)+getCmdletObject(jsonName)
	
def getClassName(jsonName):
	return jsonName

def getCmdletVerb(jsonName):
	verb = re.sub('(.)[A-Z].*', r'\1', jsonName, count=1)
	if(verb == "List"):
		return "Get"
	else:
		return verb
	
def getCmdletObject(jsonName):
	return "SF"+re.sub('^.+?([A-Z].*)', r'\1', jsonName, count=1)

def getCmdletAttribute(cmdletName):
	verbAndObject = cmdletName.split("-")
	return "[Cmdlet(VerbsCommon."+verbAndObject[0]+", \""+verbAndObject[1]+"\")]"
	
def getPrivateParameterName(paramName):
	print("_"+paramName[0].lower()+paramName[1:])
	return "_"+paramName[0].lower()+paramName[1:]
	
def getPublicParameterName(paramName):
	return paramName[0].capitalize()+paramName[1:]

def getPrivateParameterType(paramType, types):
	if(type(paramType) is dict):
		return convertTypeToPSType(paramType["name"], types)
	else:
		return convertTypeToPSType(paramType, types)
		
def getDocumentation(method):
	print(method["documentation"])
	if(type(method["documentation"]) is list):
		return method["documentation"]
	return [method["documentation"]]

def isRequest(method):
	if("params" in method.keys() and len(method["params"])!=0):
		return True
	else:
		return False
		
def convertTypeToPSType(paramType, types):
	if(type(paramType) is list):
		return convertTypeToPSType(paramType[0], types)+"[]"
	if(paramType in types.keys()):
		if("alias" in types[paramType]):
			return convertTypeToPSType(types[paramType]["alias"], types)
	if(paramType == "integer"):
		return "Int64"
	if(paramType == "UUID"):
		return "Guid"
	return paramType
	
def getNamespace(file):
	return "SolidFire."+file.split(".")[0]
	
# Each of these corresponds to a parameter
def generatePrivateDataRegion(method, types):
	pass
	
def getParameterAttributeOptions(parameter):
	parameterAttributeOptions = []
	if(type(parameter["type"]) is dict and "optional" in parameter["type"].keys()):
		parameterAttributeOptions += ["Mandatory = " + str(not bool(str(parameter["type"]["optional"]))).lower()]
	if("documentation" in parameter.keys()):
		parameterAttributeOptions += ["HelpMessage = \"" + re.sub('"', r'\"', str(parameter["documentation"])) + "\""]
		print(parameterAttributeOptions)
	return parameterAttributeOptions

def generatePowerShellCommandFromMethod(method, file, types):
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
using System.Linq;
using System.ComponentModel;
using System.Management.Automation;
using SolidFire.Core;
using SolidFire.Element.Api;
#endregion

namespace """ + getNamespace(file) + """
{"""
	if("documentation" in method.keys()):
		PSCommand += """
	/// <summary>
	/// """+"\n\t/// ".join(getDocumentation(method))+"""
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
		/// """+"\n\t\t/// ".join(getDocumentation(parameter))+"""
		/// </summary>"""
			PSCommand += """
		private """+getPrivateParameterType(parameter["type"], types)+""" """+getPrivateParameterName(parameter["name"])+""";"""
	PSCommand += """
		#endregion
		
		#region Parameters"""
	if("params" in method.keys()):
		for parameter in method["params"]:
			PSCommand += """
		[Parameter("""+", ".join(getParameterAttributeOptions(parameter))+""")]
		public """+getPrivateParameterType(parameter["type"], types)+""" """+getPublicParameterName(parameter["name"])+"""
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
			CheckConnection("""
	if("since" in method.keys()):
		PSCommand += method["since"].split('.')[0]
	PSCommand += """);
		}

		protected override void ProcessRecord()
		{
			base.ProcessRecord();"""
	if(isRequest(method)):
		PSCommand += """
			var request = new """+getClassName(method["name"])+"""Request();"""
	if("params" in method.keys()):
		for parameter in method["params"]:
			PSCommand += """
			request."""+getPublicParameterName(parameter["name"])+""" = """+getPrivateParameterName(parameter["name"])+""";"""
	PSCommand += """
			var objsFromAPI = SendRequest<"""+getClassName(method["name"])+"""Result>(\""""+getClassName(method["name"])+"""\""""+(", request" if isRequest(method) else "") +""");
			WriteObject(objsFromAPI.Select(obj => obj.Result), true);
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
		#newFilePath = "C:\\Projects2016\\PowerShell\\SolidFire\\"+file.split('.')[0]+"\\"+getCmdletName(method["name"])+".cs"
		newFilePath = "PoSH\\"+getCmdletName(method["name"])+".cs"
		print(newFilePath)
		with open(newFilePath, 'w') as f:
			print("Writing, "+newFilePath)
			f.write(generatePowerShellCommandFromMethod(method, file, types))

rootdir = "JSONInputForPoSHGen"
for root, subFolders, files in os.walk(rootdir):
	for file in files:
		if(file.endswith(".json")):
			filePath = os.path.join(rootdir, file)
			with open( filePath, 'r' ) as f:
				data = f.read()
			fileJson = json.loads(data)
			generatePowerShellCommandsFromJSON(fileJson, file)
		
		