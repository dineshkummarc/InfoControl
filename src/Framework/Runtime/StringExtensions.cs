using System;
using System.Globalization;
using System.IO;
using System.Text;
using InfoControl.Runtime;

namespace InfoControl
{
    public static partial class StringExtensions
    {
        public static object Deserialize(this string graph)
        {
            return SerializationHelper.Deserialize(graph);
        }

        public static T Deserialize<T>(this string graph)
        {
            return SerializationHelper.Deserialize<T>(graph);
        }

        public static object DeserializeFromJson(this string graph)
        {
            return SerializationHelper.DeserializeFromJson(graph);
        }

        public static T DeserializeFromJson<T>(this string graph)
        {
            return SerializationHelper.DeserializeFromJson<T>(graph);
        }

        public static T DeserializeFromWcfJson<T>(this string graph)
        {
            return SerializationHelper.DeserializeFromWcfJson<T>(graph);
        }

        public static object DeserializeFromXml(this string graph, Type type)
        {
            return SerializationHelper.DeserializeFromXml(graph, type);
        }

        public static T DeserializeFromXml<T>(this string graph)
        {
            return SerializationHelper.DeserializeFromXml<T>(graph);
        }



    }
}