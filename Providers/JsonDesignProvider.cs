
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace KitchenDesignManager.Providers
{
    public class JsonDesignProvider : IProvidesKitchenDesigns
    {
        private readonly string Folder = "";

        public JsonDesignProvider()
        {
            this.Folder = Application.persistentDataPath + "/UserData/KitchdenDesignManager";
            if (!Directory.Exists(this.Folder))
            {
                Log.Info("Creating config folder: " + this.Folder);
                Directory.CreateDirectory(this.Folder);
            }

            string path = this.Folder + "/default.json";
            if (!File.Exists(path))
            {
                Log.Info("Creating desfault file: " + path);

                List<Entry> entries =
                [
                    new Entry("Running Sushi", "1NTowOjIsMjsxLDM6MTgsMTI6Mi4zOSwxLjYsMi42LDEuNiwyLjYsMS42LDIuNiwxLjYsMi42LDEuNiwyLjYsMS42LDIuNiwxLjYsMi42LDEuNiwyLjYsMS42LDIuNiwxLjYsMi42LDEuNiwyLjYsMS42LDIuMzk6Miw3LDMsNywyOzIsNiwzLDYsMjsyLDUsMyw1LDI7Miw0LDMsNCwyOzEsLTEsMSwwLDM7NSw4LDUsOSwyOzQsOCw0LDksMjs2LDgsNiw5LDI7OCw4LDgsOSwyOzksOCw5LDksMjs3LDgsNyw5LDI7MTAsOCwxMCw5LDI7MTEsOCwxMSw5LDI7MTIsOCwxMiw5LDI7MTMsOCwxMyw5LDI7MTQsNywxNSw3LDI7MTQsNiwxNSw2LDI7MTQsNCwxNSw0LDI7MTMsMiwxMywzLDI7MTIsMiwxMiwzLDI7MTEsMiwxMSwzLDI7MTAsMiwxMCwzLDI7OCwyLDgsMywyOzcsMiw3LDMsMjs1LDIsNSwzLDI7NCwyLDQsMywyOzE0LDUsMTUsNSwxOzYsMiw2LDMsMTs5LDIsOSwzLDI7Miw4LDMsOCwyOzMsOCwzLDksMjsxNCw4LDE0LDksMjsxNCw4LDE1LDgsMjsxNCwzLDE1LDMsMjsxNCwyLDE0LDMsMjsyLDMsMywzLDI7MywyLDMsMywyOg=="),
                    new Entry("Creative Mode Map", "1NTowOjIsMjsxLDM6MjEsMTI6Mi4xMjAsMS4xMzI6NCwtMSw0LDAsMzs5LDAsMTAsMCwyOzksMiwxMCwyLDI7OSwzLDEwLDMsMjs5LDQsMTAsNCwyOzksNSwxMCw1LDI7OSw3LDEwLDcsMjs5LDYsMTAsNiwyOzksOCwxMCw4LDI7OSw5LDEwLDksMjs5LDEwLDEwLDEwLDI7OSwxMSwxMCwxMSwyOzksMSwxMCwxLDE6"),
                    new Entry("Kitchen And Garden", "2NTU3OTQzMTU1OjQ0NzQzNzE2MzoyLDI7Myw0OzEsMzoyMiwxMToyLjMzLDMuMiwxLjksMy4yLDEuOSwzLjIsMS45LDMuMiwxLjksMy4yLDEuOSwzLjIsMS45LDMuMiwxLjksMy4yLDEuOSwzLjIsMS45LDMuMiwxLjksMy4yLDEuOSwzLjIsMS45LDMuMiwxLjksMy4yLDEuOSwzLjIsMS45LDMuMiwxLjksMy4yLDEuOSwzLjIsMS45LDMuMiwxLjk6MywxLDMsMiwyOzQsMSw0LDIsMjs1LDEsNSwyLDI7NiwxLDYsMiwyOzgsMSw4LDIsMjs3LDEsNywyLDI7OSwxLDksMiwyOzEwLDEsMTAsMiwyOzEyLDEsMTIsMiwyOzExLDEsMTEsMiwyOzEzLDEsMTMsMiwyOzE0LDEsMTQsMiwyOzE1LDEsMTUsMiwyOzE3LDEsMTcsMiwyOzE2LDEsMTYsMiwyOzE5LDEsMTksMiwyOzE4LDEsMTgsMiwyOzIwLDEsMjAsMiwyOzIxLDEsMjEsMiwyOzMsLTEsMywwLDI7NCwtMSw0LDAsMjs1LC0xLDUsMCwyOzYsLTEsNiwwLDI7NywtMSw3LDAsMjs4LC0xLDgsMCwyOzksLTEsOSwwLDI7MTAsLTEsMTAsMCwyOzExLC0xLDExLDAsMjsxMiwtMSwxMiwwLDI7MTMsLTEsMTMsMCwyOzE0LC0xLDE0LDAsMjsxNSwtMSwxNSwwLDI7MTYsLTEsMTYsMCwyOzE3LC0xLDE3LDAsMjsxOCwtMSwxOCwwLDI7MTksLTEsMTksMCwyOzIwLC0xLDIwLDAsMjsyMSwtMSwyMSwwLDI7Miw5LDMsOSwxOzEsLTEsMSwwLDM7MiwwLDMsMCwxOg=="),
                    new Entry("Private Seating", "1MTowOjEsMzsyLDI7MywxOjIxLDEzOjIuNCwzLjEsMS44LDIuNCwzLjEsMS44LDIuNCwzLjEsMS44LDMuNiwyLjQsMS4zLDMuNiwyLjQsMS4zLDIuNCwzLjIsMi40LDEuMywyLjQsMy4xLDEuOCwyLjQsMy4yLDIuNCwxLjMsMy42LDIuNCwxLjMsMy42LDIuNCwxLjMsMy41LDEuOCwzLjYsMi40LDEuMywzLjYsMi40LDEuMywyLjQsMy4yLDIuNCwxLjMsMi40LDMuMSwxLjgsMi40LDMuMiwyLjQsMS4zLDMuNiwyLjQsMS4zLDMuNiwyLjQsMS4zLDIuNCwzLjEsMS44LDIuNCwzLjEsMS44LDIuNCwzLjEsMS44OjUsNSw2LDUsMjs4LDUsOCw2LDE7MTQsNSwxNSw1LDI7MiwyLDMsMiwxOzcsMiw4LDIsMTsxMiwyLDEzLDIsMTsxNywyLDE4LDIsMTsxNiw1LDE2LDYsMTs0LDUsNCw2LDE7Niw0LDYsNSwyOzYsNSw3LDUsMjs5LDUsMTAsNSwyOzEwLDUsMTEsNSwyOzEwLDQsMTAsNSwyOzEyLDUsMTIsNiwxOzE0LDQsMTQsNSwyOzEzLDUsMTQsNSwyOzEsNCwxLDUsMTsxOSw0LDE5LDUsMTs2LC0xLDYsMCwzOg=="),
                    new Entry("Booths", "1MTowOjIsMjszLDE7MSwzOjE5LDk6My4yNywyLjUsMS40LDIuNSwxLjQsMi41LDEuNCwzLjIsMi4zLDMuMiwxLjIsMi41LDEuNCwyLjUsMS40LDIuNSwxLjQsMy4yLDIuMywzLjIsMS4yLDIuNSwxLjQsMi41LDEuNCwyLjUsMS40LDMuMiwyLjMsMy4yLDEuMiwyLjUsMS40LDIuNSwxLjQsMi41LDEuNCwyLjUsMS40OjE3LC0xLDE3LDAsMzsxNyw0LDE3LDUsMTsyLDYsMyw2LDE7MiwzLDMsMywxOzUsMCw2LDAsMjs1LDEsNiwxLDI7NiwxLDYsMiwyOzYsMSw3LDEsMjs2LDAsNywwLDI7OSwwLDEwLDAsMjs5LDEsMTAsMSwyOzEwLDEsMTAsMiwyOzEwLDEsMTEsMSwyOzEwLDAsMTEsMCwyOzEzLDAsMTQsMCwyOzEzLDEsMTQsMSwyOzE0LDEsMTQsMiwyOzE0LDEsMTUsMSwyOzE0LDAsMTUsMCwyOzUsNSw2LDUsMjs1LDYsNiw2LDI7Niw2LDYsNywyOzYsNiw3LDYsMjs2LDUsNyw1LDI7OSw1LDEwLDUsMjs5LDYsMTAsNiwyOzEwLDYsMTAsNywyOzEwLDYsMTEsNiwyOzEwLDUsMTEsNSwyOzEzLDUsMTQsNSwyOzEzLDYsMTQsNiwyOzE0LDYsMTQsNywyOzE0LDYsMTUsNiwyOzE0LDUsMTUsNSwyOzEyLDQsMTIsNSwyOzgsNCw4LDUsMjs0LDQsNCw1LDI6"),
                ];

                System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(entries, Formatting.Indented));
            }
        }

        public List<Entry> GetDesigns()
        {
            List<Entry> entries = new List<Entry>();

            IEnumerable<string> files = Directory.EnumerateFiles(this.Folder, "*.json");
            foreach (string file in files)
            {
                Log.Info("Loading designs from: " + file);
                string data = File.ReadAllText(file);
                foreach (Entry entry in JsonConvert.DeserializeObject<List<Entry>>(data))
                {
                    entries.Add(entry);
                }
            }

            return entries;
        }
    }
}