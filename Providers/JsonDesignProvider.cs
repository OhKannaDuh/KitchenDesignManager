
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace KitchenDesignManager.Providers
{
    public class JsonDesignProvider : IDesignProvider
    {
        private readonly string Folder = "";

        public JsonDesignProvider()
        {
            this.Folder = Application.persistentDataPath + "/UserData/KitchdenDesignManager";
            if (!Directory.Exists(this.Folder))
            {              
                Directory.CreateDirectory(this.Folder);
            }

            string path = this.Folder + "/default.json";
            if (!File.Exists(path))
            {
               Dictionary<string, string> designs = [];

                designs.Add("Running Sushi", "1NTowOjIsMjsxLDM6MTgsMTI6Mi4zOSwxLjYsMi42LDEuNiwyLjYsMS42LDIuNiwxLjYsMi42LDEuNiwyLjYsMS42LDIuNiwxLjYsMi42LDEuNiwyLjYsMS42LDIuNiwxLjYsMi42LDEuNiwyLjYsMS42LDIuMzk6Miw3LDMsNywyOzIsNiwzLDYsMjsyLDUsMyw1LDI7Miw0LDMsNCwyOzEsLTEsMSwwLDM7NSw4LDUsOSwyOzQsOCw0LDksMjs2LDgsNiw5LDI7OCw4LDgsOSwyOzksOCw5LDksMjs3LDgsNyw5LDI7MTAsOCwxMCw5LDI7MTEsOCwxMSw5LDI7MTIsOCwxMiw5LDI7MTMsOCwxMyw5LDI7MTQsNywxNSw3LDI7MTQsNiwxNSw2LDI7MTQsNCwxNSw0LDI7MTMsMiwxMywzLDI7MTIsMiwxMiwzLDI7MTEsMiwxMSwzLDI7MTAsMiwxMCwzLDI7OCwyLDgsMywyOzcsMiw3LDMsMjs1LDIsNSwzLDI7NCwyLDQsMywyOzE0LDUsMTUsNSwxOzYsMiw2LDMsMTs5LDIsOSwzLDI7Miw4LDMsOCwyOzMsOCwzLDksMjsxNCw4LDE0LDksMjsxNCw4LDE1LDgsMjsxNCwzLDE1LDMsMjsxNCwyLDE0LDMsMjsyLDMsMywzLDI7MywyLDMsMywyOg==");
                designs.Add("Creative Mode Map", "1NTowOjIsMjsxLDM6MjEsMTI6Mi4xMjAsMS4xMzI6NCwtMSw0LDAsMzs5LDAsMTAsMCwyOzksMiwxMCwyLDI7OSwzLDEwLDMsMjs5LDQsMTAsNCwyOzksNSwxMCw1LDI7OSw3LDEwLDcsMjs5LDYsMTAsNiwyOzksOCwxMCw4LDI7OSw5LDEwLDksMjs5LDEwLDEwLDEwLDI7OSwxMSwxMCwxMSwyOzksMSwxMCwxLDE6");

                System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(designs));
            }
        }

        public Dictionary<string, string> GetDesigns()
        {
            Dictionary<string, string> entries = [];

            IEnumerable<string> files = Directory.EnumerateFiles(this.Folder, "*.json");
            foreach (string file in files)
            {
                Debug.Log("KDM: " + "Loading file: " + file);
                FileInfo fileInfo = new(file);
                string data = File.ReadAllText(file);
                foreach (KeyValuePair<string, string> entry in JsonConvert.DeserializeObject<Dictionary<string, string>>(data))
                {
                    entries.Add(entry.Key, entry.Value);
                }
            }
            return entries;
        }
    }
}