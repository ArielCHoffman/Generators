from HTMLParser import HTMLParser
import os
import json
import glob
import copy
import re

# create a subclass and override the handler methods
class MyHTMLParser(HTMLParser):
	def __init__(self):
		HTMLParser.__init__(self)
		self.method = dict()
		self.nextData = "name"
		self.columnCounter = 0
		self.columnOrientation = dict()
		self.tableName = "defaultTableName"
		self.lastHeader = ""
	
	def handle_starttag(self, tag, attrs):
		if(tag == "h1"):
			self.nextData = "name"
		if(tag == "h2"):
			self.nextData = "header"
		if(tag == "thead"):
			self.nextData = "columnName"
			self.method[self.lastHeader] = []
		if(tag == "tbody"):
			if(self.lastHeader != ""):
				self.nextData = "columnContent"
		if(tag == "tr" and self.nextData == "columnContent"):
			self.method[self.lastHeader] = self.method.get(self.lastHeader, []) + [dict()]
		
	def handle_endtag(self, tag):
		if(tag == "td" or tag == "th"):
			self.columnCounter += 1
		if(tag == "tr"):
			self.columnCounter = 0
		if(tag == "tbody"):
			self.nextData = ""
		pass

	def handle_data(self, data):
		if(data.strip() == ""):
			return
		# If we have a given title, we know what is coming next:
		if(self.nextData == "header"):
			if(data!=""):
				self.lastHeader = data
				self.nextData = "somethingElse"
			return
		if(self.nextData == "name"):
			self.method["name"] = data;
			self.nextData = "documentation"
			return
		if(self.nextData == "documentation"):
			self.method["documentation"] = self.method.get("documentation", "") + data;
			return
		if(self.nextData == "columnName"):
			self.columnOrientation[self.columnCounter] = data
			return
		if(self.nextData == "columnContent"):
			try:
				self.method[self.lastHeader][-1][self.columnOrientation[self.columnCounter]] = self.method[self.lastHeader][-1].get(self.columnOrientation[self.columnCounter], "") + data
			except:
				print(data)
				print(self.method.keys())
				print(self.lastHeader)
				print("Something went wrong!")
			
			return

def fixParameter(parameter):
	# Add the optional/required issues in
	paramType = dict()
	paramType["name"] = parameter.get("Type", "")
	parameter["type"] = paramType
	if("Type" in parameter.keys()):
		del parameter["Type"]
	if("optional" in parameter.get("Default Value", "").lower()):
		del parameter["Default Value"]
		paramType["optional"] = True
	if("required" in parameter.get("Default Value", "").lower()):
		del parameter["Default Value"]
		paramType["optional"] = False
	
	# Adjust the capitalization at a parameter level
	if("Name" in parameter.keys()):
		parameter["name"] = parameter.pop("Name")
	if("name" in parameter.keys()):
		parameter["name"] = parameter["name"][:1].lower() + parameter["name"][1:]
	if("Description" in parameter.keys()):
		parameter["documentation"] = parameter.pop("Description")
		parameter["documentation"] = re.sub(r'[\t\n\r]', '', parameter["documentation"])
	if("Type" in parameter.keys()):
		parameter["type"] = parameter.pop("Type")
		
	# Fix the parameter type name
	parameter["type"]["name"] = fixParameterTypeName(parameter["type"]["name"])

def fixParameterTypeName(name):
	if(name.lower() in docTypeToJSONTypeMap.keys()):
		name = docTypeToJSONTypeMap[name.lower()]
	if(name.lower() in lowerCaseTypes):
		name = name.lower()
	if("array of" in name.lower()):
		name = [fixParameterTypeName(re.sub("array of", "", name, flags=re.I).strip()[:-1])]
	return name

# Steps:
# 1. Import the existing JSON files.
# 2. Make a compiled hash of types and methods
# 3. Start reading through each of the files and generate the method or object. 
# 4. Compare the method key parameters (i.e. everythign that's not description) and if they're not identical understand why.
#		Put non-identical ones on list for MK.
# 5. For the ones which exist in the docs, but not the json files, dump them in an autogenerated file.

# Step 1-2:
# Manually Maintained Files
manuallyMaintainedPath = "C:\\Projects2016\\solidfire-sdk-dotnet\\solidfire-api-descriptors\\src\\main\\resources\\element"
files = glob.glob(manuallyMaintainedPath+"\*")
aggregatedTypes = []
aggregatedMethods = []
for file in files:
	path = os.path.join(manuallyMaintainedPath, file)
	with open(path, 'rb') as input:
	  data = input.read()
	importedJson = json.loads(data)
	aggregatedTypes += importedJson.get("types", "")
	aggregatedMethods += importedJson.get("methods", "")

# Step 3:
# Read through the files and make a method/object
myAggregatedTypes = []
myAggregatedMethods = []
docTypeToJSONTypeMap = {'json object': "Attributes", 'json':"Attributes", "iscsi session object": "ISCSISession", "Cluster Admin Object": "ClusterAdmin", "JSON Obejct":"Attributes",
	"hardwareinfo object":"hardwareInfo", "hardware object":"hardware", "long":"uint64", "drive stats objec":"driveStats", "fibre channel sessions object": "fibreChannelSession",
	"volumestats object":"VolumeStats", "float":"float", "virtual volume binding object object":"bindingVV"}
lowerCaseTypes = ["integer", "string", "boolean"]

rootdir = "C:\\Projects2016\\API_ElementOS_9.0\\Content\\00_API Reference Guide"
for root, subFolders, files in os.walk(rootdir):
	for folder in subFolders:
		subFolderPath = os.path.join(root, folder)
		files = glob.glob(subFolderPath+"\*")
		for file in files:
			data = ""
			filePath = os.path.join(subFolderPath, file)
			with open( filePath, 'r' ) as f:
				data = f.read()
			data = str.join("\n",data.split("\n")[1:])
			# instantiate the parser and fed it some HTML
			parser = MyHTMLParser()
			parser.feed(data)
			
			# First, handle the objects...
			if("Common Objects" in subFolderPath):
				object = copy.deepcopy(parser.method)
				if("Object Members" in object.keys()):
					object["members"] = object.pop("Object Members")
				else:
					object["members"] = []
				
				# Add the optional/required issues in
				for parameter in object["members"]:
					fixParameter(parameter)
				myAggregatedTypes += [object]
			if("Methods" in subFolderPath):
				method = copy.deepcopy(parser.method)
				parser.method = dict()
				myAggregatedMethods += [method]
				parser.reset()
				
				# Step 3.5:
				# Postprocess the methods so that they include necessary info
				
				parameters = method.get("Parameters", [])
				# Process the parameters
				for parameter in parameters:
					fixParameter(parameter)
				if "Parameters" in method.keys():
					method["params"] = method.pop("Parameters")
				
				method["release"] = "Private"
				if("documentation" in method.keys()):
					method["documentation"] = re.sub(r'[\t\n\r]', '', method["documentation"])
				
				# Now set up associated types
				type = dict()
				type["name"] = method["name"] + "Result"			
				returnInfoNames = ["returnInfo", "Return Value"]
				for returnInfoName in returnInfoNames:
					if returnInfoName in method.keys():
						# Check all the different places the return info could be stored...
						for member in method[returnInfoName]:
							fixParameter(member)
							if(member["type"]["name"] == "HardwareInfo object"):
								print("CALLED")
						type["members"] = method.pop(returnInfoName)
				myAggregatedTypes += [type]
				method["returnInfo"] = dict()
				method["returnInfo"]["type"] = type["name"]

	
# Step 4:
# Comparison of the of the manually maintained dicts vs the generated ones
methods = dict()
objects = dict()
methods["manual"] = dict()
methods["automatic"] = dict()
objects["manual"] = dict()
objects["automatic"] = dict()
for method in aggregatedMethods:
	methods["manual"][method["name"]] = method
for method in myAggregatedMethods:
	methods["automatic"][method["name"]] = method
for object in aggregatedTypes:
	objects["manual"][object["name"]] = object
for object in myAggregatedTypes:
	objects["automatic"][object["name"]] = object

desiredMethodGroup = dict()
desiredMethodGroup["Hardware"] = ["GetClusterHardwareInfo","GetHardwareConfig","GetNodeHardwareInfo","GetNvramInfo"]
desiredMethodGroup["VirtualVolume"] = ["ListVirtualVolumes","GetVirtualVolumeCount","ListVirtualVolumeBindings","ListVirtualVolumeHosts","ListVirtualVolumeTasks","ListVolumeStatsByVirtualVolume"]
desiredMethodGroup["Features"] = ["EnableFeature","GetFeatureStatus"]
desiredMethodGroup["StorageContainers"] = ["CreateStorageContainer","GetStorageContainer","DeleteStorageContainer","ListStorageContainers","GetStorageContainerEfficiency"]
desiredMethodGroup["Initiators"] = ["CreateInitiators","ListInitiators","DeleteInitiators","ModifyInitiators"]
desiredMethodGroup["Volumes"] = ["DeleteVolumes","ModifyVolumes","PurgeDeletedVolumes","SetDefaultQoS","ModifyVolumeAccessGroup","CreateVolumeAccessGroup","ListVolumeAccessGroups"]
desiredMethodGroup["Clones"] = ["ListAsyncResults","CancelClone","CancelGroupClone","CopyVolume"]
desiredMethodGroup["Other"] = ["GetHardwareInfo","ListDriveStats","ListVolumeStats","ListISCSISessions","ListFibreChannelSessions","GetLimits"]
desiredMethodGroup["Current"] = ["ListVolumeStatsByVirtualVolume", "GetHardwareInfo", "ListDriveStats", "ListVolumeStats", "ListISCSISessions", "ListFibreChannelSessions", "GetLimits", "GetNetworkConfig"]

for key in desiredMethodGroup.keys():
	output= dict()
	output["types"] = []
	output["methods"] = []
	for method in desiredMethodGroup[key]:
		print(method)
		# Collect the return object
		# Also collect any objects that are of a certain type. 
		if(method not in methods["automatic"]):
			print(method+" is not documented!! This will not be generated.")
			continue
		output["methods"] += [methods["automatic"][method]]
		objectName = methods["automatic"][method]["returnInfo"]["type"]
		output["types"] += [objects["automatic"][objectName]]
	
	with open("NewJSON/"+key+".json", 'w') as f:
		json.dump(output, f, indent=4)
	
while(1):
	objectOrMethod = raw_input("Are you looking for an object (o) or a method (m)?")
	if(objectOrMethod == "e"):
		break
	autoOrMan = raw_input("Do you want the json for the automatic generator (a), manual (m), or both (b)?")
	if(autoOrMan == "e"):
		break
	name = raw_input("What is the thing called?")
	if(name == "e"):
		break
	if(objectOrMethod == "o"):
		if(autoOrMan == "a"):
			print("AUTOMATIC")
			if(name in objects["automatic"].keys()):
				print(json.dumps(objects["automatic"][name], indent=4))
		if(autoOrMan == "m"):
			print("MANUAL")
			if(name in objects["manual"].keys()):
				print(json.dumps(objects["manual"][name], indent=4))
		if(autoOrMan == "b"):
			print("AUTOMATIC")
			if(name in objects["automatic"].keys()):
				print(json.dumps(objects["automatic"][name], indent=4))
			print("MANUAL")
			if(name in objects["manual"].keys()):
				print(json.dumps(objects["manual"][name], indent=4))			
	if(objectOrMethod == "m"):
		if(autoOrMan == "a"):
			print("AUTOMATIC")
			if(name in methods["automatic"].keys()):
				print(json.dumps(methods["automatic"][name], indent=4))
		if(autoOrMan == "m"):
			print("MANUAL")
			if(name in methods["manual"].keys()):
				print(json.dumps(methods["manual"][name], indent=4))
		if(autoOrMan == "b"):
			print("AUTOMATIC")
			if(name in methods["automatic"].keys()):
				print(json.dumps(methods["automatic"][name], indent=4))
			print("MANUAL")
			if(name in methods["manual"].keys()):
				print(json.dumps(methods["manual"][name], indent=4))
	

	
	
for key in methods["automatic"].keys():
	if(key in methods["manual"].keys()):
		print(json.dumps(methods["automatic"][key], indent=4))
		print(json.dumps(objects["automatic"][methods["automatic"][key]["returnInfo"]["type"]], indent=4))

		print(json.dumps(methods["manual"][key], indent=4))
		print(json.dumps(objects["manual"][methods["manual"][key]["returnInfo"]["type"]], indent=4))
		v = raw_input()