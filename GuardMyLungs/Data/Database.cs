using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public static class Database
    {

        static Database()
        {
            map = new Dictionary<Type, Dictionary<long, IPersistant>>();
            injectionBuffer = new Dictionary<Type, Dictionary<PropertyInfo, PropertyInfo>>();
        }

        private static Dictionary<Type, Dictionary<long, IPersistant>> map;
        private static Dictionary<Type, Dictionary<PropertyInfo, PropertyInfo>> injectionBuffer;

        private static void InitBuffer(Type type)
        {
            injectionBuffer[type] = new Dictionary<PropertyInfo, PropertyInfo>();
            AutowiredAttribute temp_attr;
            foreach (var item in type.GetProperties())
            {
                if ((temp_attr = item.GetCustomAttribute<AutowiredAttribute>()) != null)
                {
                    injectionBuffer[type][item] = type.GetProperty(temp_attr.Key);
                }
            }
        }

        public static void Inject()
        {
            // 遍历每张表
            foreach (var table in map)
            {
                var type = table.Key;
                if (!injectionBuffer.ContainsKey(table.Key))
                {
                    InitBuffer(type);
                }
                // 遍历表中的每条记录
                foreach (var item in map[table.Key])
                {
                    // 遍历记录的每个自动装配项
                    foreach (var prop_pair in injectionBuffer[table.Key])
                    {
                        var id = (long) prop_pair.Value.GetValue(item.Value);
                        var prop_type = prop_pair.Key.PropertyType;
                        if (!map[prop_type].ContainsKey(id))
                        {
                            throw new ApplicationException($"依赖对象{prop_type.Name}.{id}不存在，请检查外键是否正确");
                        }
                        Debug.Log($"注入依赖项{table.Key}.{item.Key}->{prop_type}.{id}...完成");
                        prop_pair.Key.SetValue(item.Value, map[prop_type][id]);
                    }
                }
            }
        }

        public static void Insert(Type type, object item)
        {
            var item_persistant = item as IPersistant;
            var id = item_persistant.Id;
            if (id > 9999999 || id < 1000000)
            {
                Debug.LogWarning($"数据项{type.Name}.{id}的主键不符合规范，建议修改为七位十进制数");
            }
            if (!map.ContainsKey(type))
            {
                Debug.Log($"创建{type.Name}表...完成");
                map[type] = new Dictionary<long, IPersistant>();
            }
            if (map[type].ContainsKey(id))
            {
                throw new ApplicationException($"{type.Name}表主键重复: {id}");
            }
            Debug.Log($"插入数据{type.Name}.{id}...完成");
            map[type][id] = item_persistant;
        }

        public static T Select<T>(long id) where T : IPersistant
        {
            return (T) map[typeof(T)][id];
        }

    }
}