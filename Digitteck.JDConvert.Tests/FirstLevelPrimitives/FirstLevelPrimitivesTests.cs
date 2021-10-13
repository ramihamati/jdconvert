using Digitteck.JDConverter;
using Digitteck.JDConverter.Tests.FirstLevelPrimitives.Fixtures;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace Tests
{
    public class DeserializerTests
    {
        private JObject JsonSource;

        [OneTimeSetUp]
        public void Setup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            var resourceName = $"{assemblyName}.JsonTestData.FirstLevelPrimitives.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string result = reader.ReadToEnd();
                    JsonSource = JObject.Parse(result);
                }
            }
        }

        [Test]
        public void FirstLevelJsonAndProperties()
        {
            var model = JDConvert.Deserialize<OM_1_SimplePrimitives>(JsonSource);

            Assert.AreEqual(1, model.IntValue);
            Assert.AreEqual(1, model.IntFromString);
            Assert.AreEqual((float)1, model.FloatValue);
            Assert.AreEqual((double)1, model.DoubleValue);
            Assert.AreEqual(true, model.BoolValue);
            Assert.AreEqual(true, model.BoolValueFromString);
            Assert.AreEqual("foobar", model.StringValue);
            Assert.AreEqual((decimal)123.123, model.DecimalValue);
        }

        [Test]
        public void FirstLevelEnumValues()
        {
            var model = JDConvert.Deserialize<OM_2_EnumsBasicStructs>(JsonSource);

            //this enum has an attribute with the matching to json value
            Assert.AreEqual(YesNo.Yes, model.YesNo);

            //this enum has no attributes on top of fields
            Assert.AreEqual(OkNotOk.Ok, model.OkNotOk);

            Assert.AreEqual("2016", model.DateTime.Year.ToString());
            Assert.AreEqual("2016", model.DateTimeOffset.Year.ToString());
            Assert.AreEqual("3c3373d1-90a2-411c-8a2f-823d1535f614", model.Guid.ToString());
        }

        [Test]
        public void ObjectGetValueFromNestedPaths()
        {
            var model = JDConvert.Deserialize<OM_3_NestedPaths>(JsonSource);

            Assert.AreEqual("foobar", model.StringValue);
            Assert.AreEqual("myfile", model.DisplayName);
            Assert.AreEqual("jpg", model.FileType);
        }

        [Test]
        public void ObjectHasWrongJsonPath()
        {
            var model = JDConvert.Deserialize<OM_4_WrongPaths>(JsonSource);

            Assert.AreEqual(null, model.StringValue);
        }

        [Test]
        public void ObjectWithChildObject()
        {
            var model = JDConvert.Deserialize<OM_5_ChildObject>(JsonSource);

            Assert.AreEqual("foobar", model.StringValue);
            Assert.AreEqual("myfile", model.ObjectAttributes.DisplayName);
            Assert.AreEqual("jpg", model.ObjectAttributes.FileType);
        }

        [Test]
        public void ObjectHasLists()
        {
            var model = JDConvert.Deserialize<OM_8_ListOfComplexType>(JsonSource);

            Assert.AreEqual(FileVersionType.NewFile, model.Versions[0].VersionType);
            Assert.AreEqual(1, model.Versions[0].VersionNumber);

            Assert.AreEqual(FileVersionType.UpdateFile, model.Versions[1].VersionType);
            Assert.AreEqual(2, model.Versions[1].VersionNumber);
        }

        [Test]
        public void ObjectHasChildWithList()
        {
            var model = JDConvert.Deserialize<OM_6_ChildWithLists>(JsonSource);

            Assert.AreEqual(FileVersionType.NewFile, model.OMWithLists.Versions[0].VersionType);
            Assert.AreEqual(1, model.OMWithLists.Versions[0].VersionNumber);

            Assert.AreEqual(FileVersionType.UpdateFile, model.OMWithLists.Versions[1].VersionType);
            Assert.AreEqual(2, model.OMWithLists.Versions[1].VersionNumber);
        }

        [Test]
        public void ObjectHasArrayList()
        {
            var model = JDConvert.Deserialize<OM_7_HasArrayList>(JsonSource);

            Assert.AreEqual(1, model.Values[0]);
            Assert.AreEqual(2, model.Values[1]);
            Assert.AreEqual(3, model.Values[2]);
            Assert.AreEqual("john", model.Names[0]);
            Assert.AreEqual("maria", model.Names[1]);
        }

        [Test]
        public void ListOfPrimitivesAndEnums()
        {
            var model = JDConvert.Deserialize<OM_9_ListOfBasicAndEnum>(JsonSource);

            Assert.AreEqual(1, model.Values[0]);
            Assert.AreEqual(2, model.Values[1]);
            Assert.AreEqual(3, model.Values[2]);

            Assert.AreEqual(1, model.ObjValues[0]);
            Assert.AreEqual(2, model.ObjValues[1]);
            Assert.AreEqual(3, model.ObjValues[2]);

            Assert.AreEqual("john", model.Names[0]);
            Assert.AreEqual("maria", model.Names[1]);

            Assert.AreEqual(FileVersionType.NewFile, model.FileTypes[0]);
            Assert.AreEqual(FileVersionType.UpdateFile, model.FileTypes[1]);

            Assert.AreEqual("c7a9d299-417c-4646-b23f-0b528adb95e4", model.Guids[0].ToString());
            Assert.AreEqual("3fe55490-e5ac-4a7a-91c3-c9a3de02c73c", model.Guids[1].ToString());
            Assert.AreEqual("79cf9b94-855d-449f-ad5a-2329badc026a", model.Guids[2].ToString());
        }

        [Test]
        public void PassOnValues()
        {
            var model = JDConvert.Deserialize<OM_10_PassOnPath>(JsonSource);

            Assert.AreEqual(1, model.Values.IntValue);
            Assert.AreEqual(1, model.Values.IntFromString);
            Assert.AreEqual((float)1, model.Values.FloatValue);
            Assert.AreEqual((double)1, model.Values.DoubleValue);
            Assert.AreEqual(true, model.Values.BoolValue);
            Assert.AreEqual(true, model.Values.BoolValueFromString);
            Assert.AreEqual("foobar", model.Values.StringValue);
        }

        [Test]
        public void ArrayAtIndex()
        {
            var model = JDConvert.Deserialize<OM_11_ArrayValueAtIndex>(JsonSource);

            Assert.AreEqual(FileVersionType.UpdateFile, model.VersionAtIndex.FileVersionType);
            Assert.AreEqual(null, model.VersionInexistingIndex);
            Assert.AreEqual(null, model.VersionNegativeIndex);
            Assert.AreEqual(null, model.VersionInvalidIndex);
            Assert.AreEqual(null, model.VersionInvalidIndex2);

            Assert.AreEqual(2, model.VersionAtIndex.VersionNumber);

            Assert.AreEqual(FileVersionType.UpdateFile, model.VersionTypeAtIndex);
            Assert.AreEqual(FileVersionType.NotDefined, model.VersionTypeInexistingIndex);
            Assert.AreEqual(FileVersionType.NotDefined, model.VersionTypeNegativeIndex);
            Assert.AreEqual(FileVersionType.NotDefined, model.VersionTypeInvalidIndex);
            Assert.AreEqual(FileVersionType.NotDefined, model.VersionTypeInvalidIndex2);
        }

        [Test]
        public void ArrayAtIndexWithPathArguments()
        {
            var model = JDConvert.Deserialize<OM_12_ArrayWithPathArguments>(JsonSource);

            Assert.AreEqual(FileVersionType.UpdateFile, model.VersionTypeAtIndex);
            Assert.AreEqual(FileVersionType.UpdateFile, model.VersionAtIndex.FileVersionType);
            Assert.AreEqual(2, model.VersionAtIndex.VersionNumber);

            Assert.AreEqual(null, model.VersionAtNoIndex);
            Assert.AreEqual(FileVersionType.NotDefined, model.VersionTypeAtNoIndex);
        }

        [Test]
        public void UndefinedNullData()
        {
            var model = JDConvert.Deserialize<OM_13_WrongData>(JsonSource);

            Assert.AreEqual(default(int), model.UndefinedInt);
            Assert.AreEqual(default(int), model.NullInt);
            Assert.AreEqual(null, model.NullObject);
        }

        [Test]
        public void ObjectWithCustomSerializer()
        {
            var model = JDConvert.Deserialize<OM_14_PropertyWithSerializer>(JsonSource);

            Assert.AreEqual("johndoe", model.StringValueObject.Value);
        }

        [Test]
        public void ObjectWithMultipleAttribues()
        {
            var model = JDConvert.Deserialize<OM_15_MultiplePathDefinitions>(JsonSource);

            Assert.AreEqual(1, model.IntValue);
        }

        [Test]
        public void SendValueToClassTest()
        {
            var model = JDConvert.Deserialize<OM_16_SendValueToClass>(JsonSource);

            Assert.AreEqual("myfile", model.DisplayName.DisplayName);
        }
    }
}