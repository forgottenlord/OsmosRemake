using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace OsmosRemake.IO
{
    public class Configurator
    {
        public static Config Load(string serverConfigFilePath)
        {
            if (!File.Exists(serverConfigFilePath))
            {
                SaveSample(serverConfigFilePath);
                Debug.LogError("Config not found!");
                Debug.Log("Created sample config file.");
                Debug.Log("Fill config file then restart!");
                return null;
            }
            else
            {
                using (StreamReader sr = new StreamReader(File.OpenRead(serverConfigFilePath)))
                {
                    return JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
                }
            }
        }
        public static void SaveSample(string serverConfigFilePath)
        {
            using (StreamWriter sw = new StreamWriter(File.OpenWrite(serverConfigFilePath)))
            {
            }
        }
    }
}
