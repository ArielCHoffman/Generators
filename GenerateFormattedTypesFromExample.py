import json

# It will only come to this if we are definitely not dealing with a dictionary
def convertType(object, key, types):
	typeConversion = {"unicode": "string", "int": "integer"}
	if(type(object).__name__.lower() in typeConversion.keys()):
		return typeConversion[type(object).__name__.lower()]
	if(type(object) is list):
		if(len(object) != 0):
			if(type(object[0]) != dict):
				return [convertType(object[0], key, types)]
			else:
				types += getTypes(object[0], key[0].upper() + key[1:])
				return [key[0].upper() + key[1:]]
		else:
			return []
	return type(object).__name__.lower()

def getTypes(object, objectName):
	# Basically, foreach key, we start constructing the object members.
	# If we find a key that is a dictionary, we call this same method on that key
	# and store the member as an object name. 
	types = []
	thisType = dict()
	thisType["members"] = []
	thisType["name"] = objectName
	for key in object.keys():
		thisMember = dict()
		thisMember["name"] = key
		if(type(object[key]) is not dict):
			thisMember["type"] = convertType(object[key], key, types)
		else:
			thisMember["type"] = key[0].upper() + key[1:]
			types += getTypes(object[key], key[0].upper() + key[1:])
		thisType["members"] += [thisMember]
	types += [thisType]
	return types

with open("responseExamples/hardware.json", 'r' ) as f:
	data = f.read()

object = json.loads(data)
types = getTypes(object, "ExampleName")

print(json.dumps(types, indent=4))