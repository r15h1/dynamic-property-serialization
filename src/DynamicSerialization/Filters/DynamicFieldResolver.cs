using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace DynamicSerialization.Filters
{
    public class DynamicFieldResolver : DefaultContractResolver
    {
        private readonly Dictionary<Type, HashSet<string>> serializedProperties;
        public DynamicFieldResolver() => serializedProperties = new Dictionary<Type, HashSet<string>>();

        public void IncludePropertyForSerialization(Type type, params string[] jsonProperties)
        {
            if (!serializedProperties.ContainsKey(type)) serializedProperties[type] = new HashSet<string>();

            foreach (var prop in jsonProperties)
                if(!string.IsNullOrWhiteSpace(prop))
                    serializedProperties[type].Add(prop);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var type = property.DeclaringType;

            if (!IsPropertySerializable(type, property))
            {
                property.ShouldSerialize = s => false;
                property.Ignored = true;
            }

            return property;
        }

        private bool IsPropertySerializable(Type type, JsonProperty property)
        {
            //serialize all properties if nothing is specified
            if (!serializedProperties.ContainsKey(type))
                return true;
            else if (serializedProperties[type].Count == 0)
                return true;

            //otherwise ignore the ones that are not specified in the list
            return serializedProperties[type].Any(p => p.ToLowerInvariant().Equals(property.PropertyName.ToLowerInvariant()));
        }
    }
}