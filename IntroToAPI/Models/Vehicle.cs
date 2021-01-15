using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI.Models
{
    // We took the following steps to get the properties for this class:
        // 1. Visited the "swapi.dev/documentation" site and clicked on "Vehicles" under "Resources"
        // 2. Copied all the code from the "Example response" section
        // 3. Find a Json to C# converter online (we used json2csharp.com)
        // 4. Paste the Json object into the Json section
        // 5. Click "Use Pascal Case" and "Add JsonProperty Attributes in the "Settings section
        // 6. Click "Convert"
        // 7. Copy all the code within the C# class that was created
        // 8. Paste the C# code within the "Person" class below
            // Note: there will be error messages under all the "JsonProperty" attributes
        // 9. Click on one of the "JsonProperty" attributes and click "Ctrl + ."
        // 10. Click "Install package 'Newtonsoft.Json'"
    class Vehicle
    {
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }

        [JsonProperty("consumables")]
        public string Consumables { get; set; }

        [JsonProperty("cost_in_credits")]
        public string CostInCredits { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("crew")]
        public string Crew { get; set; }

        [JsonProperty("edited")]
        public DateTime Edited { get; set; }

        [JsonProperty("length")]
        public string Length { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("passengers")]
        public string Passengers { get; set; }

        [JsonProperty("pilots")]
        public List<object> Pilots { get; set; }

        [JsonProperty("films")]
        public List<string> Films { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("vehicle_class")]
        public string VehicleClass { get; set; }
    }
}
