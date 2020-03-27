using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public static class Player
    {

        public static byte CurrentLevel { get; set; }
        public static List<long> Cells { get; set; }

        private static void CreatePlayer()
        {
            var fs = File.Create($"{Application.dataPath}/sav/sav.gml");
            var bw = new BinaryWriter(fs);
            bw.Write((byte) 1); // Level
            bw.Write((byte) 2); // Length of Cells
            bw.Write(1000000L);
            bw.Write(1000001L);
            bw.Close();
            fs.Close();
        }

        public static void Load()
        {
            var data_path = $"{Application.dataPath}/sav";
            var file_path = $"{data_path}/sav.gml";
            if (!Directory.Exists(data_path))
            {
                Debug.Log($"没有检测到存档路径: {data_path}，将创建新路径");
                Directory.CreateDirectory(data_path);
            } else
            {
                Debug.Log($"检测到存档路径: {data_path}，将在该路径中查找存档文件");
            }
            if (!File.Exists(file_path))
            {
                Debug.Log($"没有检测到存档文件: {file_path}，将创建新存档");
                CreatePlayer();
            } else
            {
                Debug.Log($"检测到存档文件: {file_path}，将读取该存档");
            }
            var fs = File.OpenRead(file_path);
            var br = new BinaryReader(fs);
            CurrentLevel = br.ReadByte();
            Debug.Log($"最新关卡: {CurrentLevel}");
            var count = br.ReadByte();
            Cells = new List<long>(count + 5);
            var cells_str = new StringBuilder();
            cells_str.Append("已解锁细胞: [");
            for (int i = 0; i < count; i++)
            {
                Cells.Add(br.ReadInt64());
                cells_str.Append(" ");
                cells_str.Append(Database.Select<Cell>(Cells[i]).Name);
            }
            cells_str.Append(" ]");
            Debug.Log(cells_str);
            br.Close();
            fs.Close();
        }

        public static void Save()
        {
            var fs = File.OpenWrite($"{Application.dataPath}/sav/sav.gml");
            var bw = new BinaryWriter(fs);
            bw.Write(CurrentLevel);
            bw.Write((byte) Cells.Count);
            foreach (var item in Cells)
            {
                bw.Write(item);
            }
            Debug.Log("存档完成");
            bw.Close();
            fs.Close();
        }

    }
}
