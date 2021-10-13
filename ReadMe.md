<h1>JDCConvert</h1>
The JDConvert project was designed to replace the Newtonsoft.JsonConverter for complex rest api responses. When dealing with Autodesk Forge there is an abundency of data that with Newtonsoft should be modelled in a complex set of classes. 
The JDConvert library will reverse the way the deserializing acts by having the attributes taking control of the source of their data.

The JDConvert class exposes several methods for deserializing an object:

-  `public static T Deserialize<T>(JObject jObject) where T : class`

-  `public object Deserialize(Type modelType, JObject jObject)`

 - `public T DeserializeObject<T>(JObject jObject) where T : class`

<h2>Internals</h2>

- Creating a serializer for a new type

```
public sealed class JDTokenGuidDeserializer : IJDDeserializer
{
    public object Convert(JToken jToken)
    {
        string guidValue = jToken.Value<string>();
        bool parsed = Guid.TryParse(guidValue, out Guid guid);

        if (parsed)
        {
            return guid;
        }

        return Guid.Empty;
    }
}
```
Register serializers in **JDDeserializerManager**** 

- Adding a value converter
While the Deserializer handles passing data from json to the Model, the value converter matches different types but neet to be parsed.

For example in json you can have a boolean value, or a string with a boolean value inside. For the later, there is a need for a value converter.

The value converters are stored in the ValueConverters folder.

  - Create the converter clas
 
```
public class BoolConverter : IValueConvert
    {
        public object Convert(Type targetType, object sourceValue)
        {
            if (sourceValue is bool)
            {
                return sourceValue;
            }
            if (sourceValue is string stringValue)
            {
                if (stringValue.ToLower().Trim() == "false")
                {
                    return false;
                }
                else if (stringValue.ToLower().Trim() == "true")
                {
                    return true;
                }
            }
            if (sourceValue is int intValue)
            {
                //0 is false
                return 0 != intValue;
            }

            return null;
        }

        public bool ValidConverter(Type targetType)
        {
            return (targetType.Equals(typeof(bool)));
        }
    }
```
  - Registering the converter:

```
public class JDValueConverterManager
{
      public JDValueConverterManager()
      {
            this.Converters = new List<IValueConvert>()
            {
                new BoolConverter(),
                new EnumValueConverter()
            };
        }

        public List<IValueConvert> Converters { get; }

        public object Convert(Type targetType, object sourceValue)
        {
            foreach(var converter in this.Converters)
            {
                if (converter.ValidConverter(targetType))
                {
                    return converter.Convert(targetType, sourceValue);
                }
            }

            return sourceValue;
        }
}
```

<h2>1. Mapping Primitives - First Level Mapping</h2>

**Json Object**

```
{
  "intvalue": 1,
  "intfromstring": "1",
  "floatvalue": "1.0",
  "doublevalue": "1.0",
  "boolvalue": true,
  "boolvaluefromstring": "true",
  "stringvalue": "foobar",
}
```
**Mapping**

```
public class OM_1_SimplePrimitives
{
   [JsonPropertyPath("intvalue")]
   public int IntValue { get; set; }

   [JsonPropertyPath("intfromstring")]
   public int IntFromString { get; set; }

   [JsonPropertyPath("floatvalue")]
   public float FloatValue { get; set; }

   [JsonPropertyPath("doublevalue")]
   public double DoubleValue { get; set; }

   [JsonPropertyPath("boolvaluefromstring")]
   public bool BoolValueFromString { get; set; }

   [JsonPropertyPath("boolvalue")]
   public bool BoolValue { get; set; }

   [JsonPropertyPath("stringvalue")]
   public string StringValue { get; set; }
    }
```

<h2>2. Mapping Enums DateTimes Guid</h2>

**Json Object**

```
{
  "enumValue": "yes",
  "enumValueWithNoAttribute": "Ok",
  "datetimeoffset": "2016-04-01T11:09:03.000Z",
  "datetime": "2016-04-01T11:09:03.000Z",
  "guidvalue": "3c3373d1-90a2-411c-8a2f-823d1535f614",
}
```
**Mapping**


```
public class OM_2_EnumsBasicStructs
   {
   [JsonPropertyPath("enumValue")]
   public YesNo YesNo { get; set; }

   [JsonPropertyPath("enumValueWithNoAttribute")]
   public OkNotOk OkNotOk { get; set; }

   [JsonPropertyPath("datetimeoffset")]
   public DateTimeOffset   DateTimeOffset{ get; set; }

   [JsonPropertyPath("datetime")]
   public DateTime DateTime { get; set; }

   [JsonPropertyPath("guidvalue")]
   public Guid Guid { get; set; }
}
```

<h2>3. Nested Path</h2>

**Json Object**

```
{
  "stringvalue": "foobar",
  "attributes": {
    "displayname": "myfile",
    "metadata": {
      "filetype": "jpg"
    }
}
```
**Mapping**

```
public class OM_3_NestedPaths
   {
   [JsonPropertyPath("attributes.displayname")]
   public string DisplayName { get; set; }
   
   [JsonPropertyPath("attributes.metadata.filetype")]
   public string FileType { get; set; }

   [JsonPropertyPath("stringvalue")]
   public string StringValue { get; set; }
}
```

<h2>4. Wrong Paths</h2>

**Json Object**

```
{
  "intvalue": 1,
  "intfromstring": "1",
  "floatvalue": "1.0",
  "doublevalue": "1.0",
  "boolvalue": true,
  "boolvaluefromstring": "true",
  "stringvalue": "foobar"
}
```
**Mapping**
In the case of a bad mapping, the converter will not find any value at the path and no value will be provided, meaning that the property will have the default value.
```
public class OM_4_WrongPaths
   {
   [JsonPropertyPath("abc")]
   public string StringValue { get; set; }
}
```

<h2>5. Child Object</h2>

With JdConverd you can provide a relative path to a child object. There is no difference in defining the path. On the property that represents the child object, the converter will navigate through the path and JsonObject found there will be passed on to the next child.

**Json Object**

```
{
  "stringvalue": "foobar",
  "attributes": {
    "displayname": "myfile",
    "metadata": {
      "filetype": "jpg"
    }
  }
}
```

**Mapping**

```
public class OM_5_Object
{
    [JsonPropertyPath("stringvalue")]
    public string StringValue { get; set; }

    [JsonPropertyPath("attributes")]
    public OM_5_Child ObjectAttributes { get; set; }
}
```

```
public class OM_5_Child
{
    [JsonPropertyPath("displayname")]
    public string DisplayName { get; set; }
    
    [JsonPropertyPath("metadata.filetype")]
    public string FileType { get; set; }
}
```

<h2>6. ChildObject - Bridging</h2>
   
With JDConvert you can simply pass a value from the parent to the object. 

If the path on the parent mapping model reflects a string, but the child property is an object containing that string, you can achieve this by bridging.

**Json Object**

```
{
  "attributes": {
    "displayname": "myfile",
    "metadata": {
      "filetype": "jpg"
    }
  }
}
```

**Mapping**

```
public class OM_16_SendValueToClass
{
    [JsonPropertyPath("attributes.displayname")]
    public OM_16_DisplayName DisplayName { get; set; }
}

public class OM_16_DisplayName
{
    [JsonPropertyPath("")]
    public string DisplayName { get; set; }
}
```

<h2>7. Child Object - Passing</h2>


With JDConvert you can pass a full set of data to a child object on the next level by using the passing technique:

**Json Object**


```
{
  "intvalue": 1,
  "intfromstring": "1",
  "floatvalue": "1.0",
  "doublevalue": "1.0",
  "boolvalue": true,
  "boolvaluefromstring": "true",
  "stringvalue": "foobar"
}
```

**Mapping**
 
```
public class OM_10_PassOnPath
{
    [JsonPropertyPath("")]
    public OM_10_Values Values { get; set; }
}

public class OM_10_Values
{
    [JsonPropertyPath("intvalue")]
    public int IntValue { get; set; }

    [JsonPropertyPath("intfromstring")]
    public int IntFromString { get; set; }

    [JsonPropertyPath("floatvalue")]
    public float FloatValue { get; set; }

    [JsonPropertyPath("doublevalue")]
    public double DoubleValue { get; set; }

    [JsonPropertyPath("boolvaluefromstring")]
    public bool BoolValueFromString { get; set; }

    [JsonPropertyPath("boolvalue")]
    public bool BoolValue { get; set; }

    [JsonPropertyPath("stringvalue")]
    public string StringValue { get; set; }
}
```

<h2>8. Lists</h2>

**Json Object**


```
{
"data": {
    "versions": [
      {
        "type": "newfile",
        "number": 1
      },
      {
        "type": "updatedfile",
        "number": 2
      }
    ],
}
```


**Mapping**

```
public class OM_6_Lists
{
   [JsonPropertyPath("data")]
   public OM_6_ObjWithList OMWithLists { get; set; }
}
```

```
public class OM_6_ObjWithList
{
   [JsonPropertyPath("versions")]
   public List<OM_6_Version> Versions { get; set; }
}
```

```
public class OM_6_Version
{
   [JsonPropertyPath("type")]
   public FileVersionType VersionType { get; set; }

   [JsonPropertyPath("number")]
   public int VersionNumber { get; set; }
}
```

<h2>9. ArrayList</h2>

ArrayLists work only for primary values because the type of the item stored in the array will be determine by the type of the value stored in json. If the object is not a basic value, the converter has no way of knowing what it should map.

**Json Object**

```
{
  "data": {
    "values": [ 1, 2, 3 ],
    "names": [ "john", "maria" ]
   }
}
```

**Mapping**

```
public class OM_7_HasArrayList
{
    [JsonPropertyPath("data.values")]
    public ArrayList Values { get; set; }

    [JsonPropertyPath("data.names")]
    public ArrayList Names { get; set; }
}
```

<h2>10. Lists of complex types</h2>

**Json Object**

```
{
   data": {
    "versions": [
      {
        "type": "newfile",
        "number": 1
      },
      {
        "type": "updatedfile",
        "number": 2
      }
    ]
   }
}
```

**Mapping**

```
public class OM_8_ListOfComplexType
{
    [JsonPropertyPath("data.versions")]
    public List<OM_8_Version> Versions { get; set; }
}
```

```
public class OM_8_Version
{
    [JsonPropertyPath("type")]
    public FileVersionType VersionType { get; set; }

    [JsonPropertyPath("number")]
    public int VersionNumber { get; set; }
}
```

<h2>11. Lists of primaries, enums, Guids, objects</h2>


JDConvert will deserialize all types of primary values and enumes, and it has support for several built-in structs (Guid / DateTime). If a list is a list of objects, the same principle is considered as in the case of an array list. The json must provide primary values in order for the JDCOnvert to determine the type to deserialize.

**Json Object**

```
{
    "values": [ 1, 2, 3 ],
    "names": [ "john", "maria" ],
    "filetypes": [ "newfile", "updatedfile" ],
    "guids": [
      "c7a9d299-417c-4646-b23f-0b528adb95e4",
      "3fe55490-e5ac-4a7a-91c3-c9a3de02c73c",
      "79cf9b94-855d-449f-ad5a-2329badc026a"
    ]
}
```
**Mapping**

```
public class OM_9_ListOfBasicAndEnum
{
    [JsonPropertyPath("data.values")]
    public List<int> Values { get; set; }

    [JsonPropertyPath("data.values")]
    public List<object> ObjValues { get; set; }

    [JsonPropertyPath("data.names")]
    public List<string> Names { get; set; }

    [JsonPropertyPath("data.filetypes")]
    public List<FileVersionType> FileTypes { get; set; }

    [JsonPropertyPath("data.guids")]
    public List<Guid> Guids { get; set; }
}
```

<h2>12. Getting a list item by the index</h2>

**Json Object**


```
{
   data": {
    "versions": [
      {
        "type": "newfile",
        "number": 1
      },
      {
        "type": "updatedfile",
        "number": 2
      }
    ]
   }
}
```


**Mapping**

```
public class OM_11_ArrayValueAtIndex
{
   [JsonPropertyPath("data.versions[1]")]
   public OM_11_Version VersionAtIndex { get; set; }

   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[10]")]
   public OM_11_Version VersionInexistingIndex { get; set; }
   
   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[-1]")]
   public OM_11_Version VersionNegativeIndex { get; set; }

   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[a]")]
   public OM_11_Version VersionInvalidIndex { get; set; }

   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[]")]
   public OM_11_Version VersionInvalidIndex2 { get; set; }

   [JsonPropertyPath("data.versions[1].type")]
   public FileVersionType VersionTypeAtIndex { get; set; }
   
   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[10].type")]
   public FileVersionType VersionTypeInexistingIndex { get; set; }

   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[-1].type")]
   public FileVersionType VersionTypeNegativeIndex { get; set; }

   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[a].type")]
   public FileVersionType  VersionTypeInvalidIndex { get; set; }

   //Will not throw an exception, it will not provide a value (be null)
   [JsonPropertyPath("data.versions[].type")]
   public FileVersionType VersionTypeInvalidIndex2 { get; set; }
}

public class OM_11_Version
{
   [JsonPropertyPath("type")]
   public FileVersionType FileVersionType { get; set; }

   [JsonPropertyPath("number")]
   public int VersionNumber { get; set; }
}
```

<h2>13. Geting a list item at an index, that is determined by a search condition</h2>
**Json Object**

```
{
  data": {
    "versions": [
      {
        "type": "newfile",
        "number": 1
      },
      {
        "type": "updatedfile",
        "number": 2
      }
    ]
  }
}
```

**Mapping**

The index is determined by the IndexOfIf function.

The arguments
- arg0 : the array it will search in. The function will iterate through each item
- arg1 : the array item property it will get the value and check against
- arg2 : the value it's compared to

E.G. in the function `IndexOfIf(data.versions, type, updatedfile)` the index will be of the item from the table `data.versions` where the property `type` has the value `updatedfile` and the result will be 1. So the final path will be `[JsonPropertyPath("data.versions[{0}] = [JsonPropertyPath("data.versions[1]`


```
public class OM_12_ArrayWithPathArguments
{
   [JsonPropertyPath("data.versions[{0}]", "{IndexOfIf(data.versions, type, updatedfile)}")]
   public OM_12_Version VersionAtIndex { get; set; }

   [JsonPropertyPath("data.versions[{0}]", "{IndexOfIf(data.versions, type, abc)}")]
   public OM_12_Version VersionAtNoIndex { get; set; }

   [JsonPropertyPath("data.versions[{0}].type", "{IndexOfIf(data.versions, type, updatedfile)}")]
   public FileVersionType VersionTypeAtIndex { get; set; }

   [JsonPropertyPath("data.versions[{0}].type", "{IndexOfIf(data.versions, type, abc)}")]
   public FileVersionType VersionTypeAtNoIndex { get; set; }
}
```

```
public class OM_12_Version
{
   [JsonPropertyPath("type")]
   public FileVersionType FileVersionType { get; set; }

   [JsonPropertyPath("number")]
   public int VersionNumber { get; set; }
}
```
<h2>14. Multiple Path Definitions</h2>

The JsonPropertyPath allow multiple definitions. The deserializers will search each attribute until it finds a valid path, then it stops.
This is usefull if you have a common definition for multiple rest objects that have different json property names.
**Json Object**

```
{
  "intvalue": 1,
  "intfromstring": "1",
  "floatvalue": "1.0",
  "doublevalue": "1.0",
  "boolvalue": true,
  "boolvaluefromstring": "true",
  "stringvalue": "foobar"
}
```

**Mapping**

```
public class OM_15_MultiplePathDefinitions
{
   [JsonPropertyPath("hintvalue")]
   [JsonPropertyPath("intvalue")]
   [JsonPropertyPath("mintvalue")]
   public int IntValue{ get; set; }
}
```


<h2>15. Custom Deserializer</h2>
The JDDeserializer attribute allows you to define a custom deserializer that is applied using an attribute decorating the property.

**Json Object**

```
{
   "valueobject" :  "johndoe"
}
```


**Mapping**

```
public class StringValueObject
{
    public string Value { get; private set; }
    private StringValueObject() { }

    public static StringValueObject Create(string value)
    {
        return new StringValueObject { Value = value };
    }
}
```

```
public class OM_14_CustomSerializer : IJDDeserializer
{
    public object Convert(JToken jToken)
    {
        string value = jToken.Value<string>();

        return StringValueObject.Create(value ?? "");
    }
}
```

```
public class OM_14_PropertyWithSerializer
{
    [JsonPropertyPath("valueobject")]
    [JDDeserializer(typeof(OM_14_CustomSerializer))]
    public StringValueObject StringValueObject { get; set; }
}
```

