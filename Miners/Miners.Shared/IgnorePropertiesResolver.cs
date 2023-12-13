using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace Miners.Shared
{
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly HashSet<string> _propertiesToIgnore;

        public IgnorePropertiesResolver(params string[] propertiesToIgnore)
        {
            _propertiesToIgnore = new HashSet<string>(propertiesToIgnore, StringComparer.OrdinalIgnoreCase);
        }

        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (_propertiesToIgnore.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }

            return property;
        }
    }
}