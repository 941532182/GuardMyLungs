using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LitJson;

namespace Data
{
    public static class DatabaseDriver
    {

        public static void Setup()
        {
            var path_head = "TextAssets";
            var appconfig_json = Resources.Load<TextAsset>($"{path_head}/appconfig").text;
            var appconfig = JsonMapper.ToObject<Appconfig>(appconfig_json);

            // 将外存的数据全部插入内存
            foreach (var type_name in appconfig.PersistantTypes)
            {
                var json = Resources.Load<TextAsset>($"{path_head}/{type_name.ToLower()}").text;
                var type = Type.GetType($"Data.{type_name}");
                var items = JsonMapper.ToObject(json, type.MakeArrayType()) as Array;
                foreach (var item in items)
                {
                    Database.Insert(type, item);
                }
            }

            // 依赖注入
            Database.Inject();

            Debug.Log("内存数据库初始化完毕!");
        }

    }
}
