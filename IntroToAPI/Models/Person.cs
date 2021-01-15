using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI.Models
{
    // The way we converted our "Person" Json object to a C# object is by taking the following steps:
        // 1. Created the "Models" folder and the "Person" class within it
        // 2. Go the "Program.cs" file and put a break point around the "content" variable
        // 3. Step into the code until the "content" variable isn't null
        // 4. Click on the magnifying glass to see the Json object in a different window
        // 5. Copy the Json object
        // 6. Find a Json to C# converter online (we used json2csharp.com)
        // 7. Paste the Json object into the Json section
        // 8. Click "Use Pascal Case" and "Add JsonProperty Attributes in the "Settings section
        // 9. Click "Convert"
        // 10. Copy all the code within the C# class that was created
        // 11. Paste the C# code within the "Person" class below
            // Note: there will be error messages under all the "JsonProperty" attributes
        // 12. Click on one of the "JsonProperty" attributes and click "Ctrl + ."
        // 13. Click "Install package 'Newtonsoft.Json'" then click "Find and install latest version"

    class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("mass")]
        public string Mass { get; set; }

        [JsonProperty("hair_color")]
        public string HairColor { get; set; }

        [JsonProperty("skin_color")]
        public string SkinColor { get; set; }

        [JsonProperty("eye_color")]
        public string EyeColor { get; set; }

        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("homeworld")]
        public string Homeworld { get; set; }

        [JsonProperty("films")]
        public List<string> Films { get; set; }

        [JsonProperty("species")]
        public List<object> Species { get; set; }

        [JsonProperty("vehicles")]
        public List<string> Vehicles { get; set; }

        [JsonProperty("starships")]
        public List<string> Starships { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("edited")]
        public DateTime Edited { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
