using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI.Models
{
    // We took the following steps to get the properties for this class:
        // 1. Visited the "swapi.dev/documentation" site and clicked on "Searching" under "Getting started"
        // 2. Copied the "https://swapi.dev/api/people/?search=" link
        // 3. Went to Postman and pasted the link into a "Get" request and hit "Send"
        // 4. Saw that the response returned "count", "next", "previous", and "results" properties
        // 5. Came into the SearchResult class and manually created the "Count" and "Results" properties
        // 6. Used generic type "T" for the "SearchResult" class and "List" so we can pass other classes
            // ("Person", "Vehicle", etc.) into the "SearchResult" class
    
    class SearchResult<T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("results")]
        public List<T> Results { get; set; }
    }
}
