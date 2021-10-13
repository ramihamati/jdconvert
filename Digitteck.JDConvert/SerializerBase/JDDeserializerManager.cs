using Digitteck.JDConverter.Helpers;
using Digitteck.JDConverter.Serializer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Digitteck.JDConverter.SerializerBase
{
    public class JDDeserializerManager
    {
        public JDValueConverterManager ValueConverter { get; }

        private Dictionary<Type, IJDDeserializer> Deserializers;

        public JDDeserializerManager()
        {
            ValueConverter = new JDValueConverterManager();

            Deserializers = new Dictionary<Type, IJDDeserializer>
            {
                {  typeof(int), new JDTokenIntDeserializer() },
                {  typeof(double), new JDTokenDoubleDeserializer() },
                {  typeof(float), new JDTokenFloatDeserializer() },
                {  typeof(DateTimeOffset), new JDTokenDateTimeOffsetDeserializer() },
                {  typeof(string), new JDTokenStringDeserializer() },
                {  typeof(Guid), new JDTokenGuidDeserializer() }
            };
        }

        public object GetValueOf(Type propertyType, JToken jToken)
        {
            if (Deserializers.ContainsKey(propertyType))
            {
                object deserializedPrimitive =  Deserializers[propertyType].Convert(jToken);

                //deserialized values may be not the same as the required type. 
                //a "false" in a string form will be converted to false using a value converter
                return ValueConverter.Convert(propertyType, deserializedPrimitive);
            }

            JDTokenBasicDeserializer reader = new JDTokenBasicDeserializer();

            object basicReaderValue =  reader.Convert(jToken);
            return ValueConverter.Convert(propertyType, basicReaderValue);
        }

        public object GetValueOf(Type propertyType, JValue jToken)
        {
            return GetValueOf(propertyType, (JToken)jToken);
        }
    }
}
