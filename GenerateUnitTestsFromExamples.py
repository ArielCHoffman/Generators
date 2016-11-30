import json
import os
import glob

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

def makeAssert(result, assertList, keyPath):
	if(type(result) is unicode):
		return assertList + ["Assert.IsTrue(result"+keyPath+" == \""+result+"\", \"Died on "+keyPath+"\");\n\t\t\t"]
	if(type(result) is bool or type(result) is int):
		return assertList + ["Assert.IsTrue(result"+keyPath+" == "+str(result)+", \"Died on "+keyPath+"\");\n\t\t\t"]
	if(type(result) is dict):
		val = []
		for key in result.keys():
			val = val + makeAssert(result[key], assertList, keyPath+"."+key[0].upper()+key[1:])
		return val + assertList
	if(type(result) is list):
		val = []
		for i in range(0,len(result)):
			val = makeAssert(result[i], assertList, keyPath+"["+str(i)+"]") + val
		return val + assertList
	if(result is None):
		return assertList
	return assertList

	
for root, subFolders, files in os.walk("responseExamples"):
	for folder in subFolders:
		unitTestOutput = open("responseExamples\\"+folder+".cs", 'w');
		subFolderPath = os.path.join(root, folder)
		files = glob.glob(subFolderPath+"\*")
		fileHeader = """
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SolidFire.Element.Api;
using SolidFire.Core;
using Moq;
using System.Linq;
using SolidFire.SDK.Adaptor;

namespace Element.Tests
{
    [TestClass]
    public class """+folder+"Tests"+"""
    {"""
		unitTestOutput.write(fileHeader)
		for file in files:
			fileName = file.split("\\")[-1]
			methodHeader = """
        [TestMethod]
        public void Test"""+fileName.split('.')[0]+"""()
        {
            var mock = new Mock<IRpcRequestDispatcher>();
            mock.Setup(m => m.Dispatch(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(Properties.Resources.RESP_"""+fileName.split('.')[0]+"""_v9);
            SolidFireElement sfe = new SolidFireElement(mock.Object);

            // Act
            """+fileName.split('.')[0]+"""Result result = sfe."""+fileName.split('.')[0]+"""();
			"""
			unitTestOutput.write(methodHeader)
			with open( file, 'r' ) as f:
				data = f.read()
			try:
				object = json.loads(data)
			except:
				print(file+" could not be turned into json.")
			#types = getTypes(object, "ExampleName")
			#print(json.dumps(types, indent=4))

			asserts = makeAssert(object["result"], [], "")
			for a in asserts:
				unitTestOutput.write(a)
			methodCloser = """
        }"""
			unitTestOutput.write(methodCloser)
		fileCloser = """
    }
}
"""
		unitTestOutput.write(fileCloser)