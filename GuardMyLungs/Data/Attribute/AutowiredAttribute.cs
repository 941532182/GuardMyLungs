using System;

namespace Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AutowiredAttribute : Attribute
    {
        public string Key { get; }
        public AutowiredAttribute(string key)
        {
            Key = key;
        }
    }
}
