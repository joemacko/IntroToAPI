using IntroToAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI
{
    class SWAPIService
    {
        // The class SWAPIService doesn't need anyone outside to see the HttpClient, so we made it private. 
            // In other words, want someone who's accessing SWAPIService to access our HttpClient.
        // Making something readonly means later on in the code you can't accidentally change or destroy the reference
        private readonly HttpClient _httpClient = new HttpClient();

        // Async Method - async is a keyword modifier we can add
            // async methods return tasks and should have "Async" suffix at the end of the method name
            // passing in a string and url in the method so program knows which person to get
        public async Task<Person> GetPersonAsync(string url)
        {
            //// Get request
            //HttpResponseMessage response = await _httpClient.GetAsync(url);

            //if (response.IsSuccessStatusCode)
            //{
            //    // Was a success
            //    Person person = await response.Content.ReadAsAsync<Person>();
            //    return person;
            //}

            //// Was not a success
            //return null;

            // Here we are using the "GetAsync" generic method as a helper within the more specific method
                // of "GetPersonAsync". This makes the methods easier to read and understand throughout the code.
            return await GetAsync<Person>(url);
        }

        // Making another "public" "async" method that returns a "Task" of "Vehicle". For async methods, the return type
            // ends up being whatever is located within the angled brackets of the "Task" class, not a "Task" itself.
            // We're passing in a "string" and "url" again this time to access a "Vehicle" object from the "swapi.dev" API site.
        public async Task<Vehicle> GetVehicleAsync(string url)
        {
            // The "var" recognizes the type from the other end of the equation (in this case, the "HttpResponseMessage" class)
                // and automatically assigns it to the "response" variable.
            // We're accessing the "_httpClient" field through the "GetAsync" method and passing in the "url". 
            // We don't have to use the ".Response" property because we have the "await" keyword.
            var response = await _httpClient.GetAsync(url);
            // Using a "ternary" instead of an if statement to check a boolean
                // Basically, the method says, if the "HttpResponseMessage" "IsSuccessStatusCode" property is true, then
                // return the "Vehicle" object using the "ReadAsAsync" method through the ".Content" property.
                // Otherwise, return null
            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<Vehicle>()
                : null;            
        }

        // This is called a "generic method". A generic method takes in a type, but it won't know which type until you
            // implement that class.
        // Here we are creating another "public" "async" method that has a return type of "T", which is the generic return
            // type. The return type is then defined within the angle brackets of the "GetAsync" method title (still generic "T").
        // We again pass in a string url
        // "where T: class" is a method constraint saying that the type "T" generic has to be a "class"
            // Method constraints give more information about the generic, limits what programmers can do, and helps us
            // write better code that can't be misused and break as easily.
        public async Task<T> GetAsync<T>(string url) where T: class
        {
            // This is the normal workflow from the earlier methods above.
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                T content = await response.Content.ReadAsAsync<T>();
                return content;
            }

            // Without the "where T: class" constraint of code in the method declaration, we couldn't
                // have returned null because it would not know what type to return. The return type could be 
                // "not nullable", like an integer, which wouldn't work, so we would've gotten an error.
                // However, we could use "return default" without the constraint.
            return null;
        }

        // Here we're creating another "public" "async" method that returns a type "SearchResult" that has a type of "Person".
        // We want to pass in a "string" of query to the "GetPersonSearchAsync" method because that will allow us 
            // to search for whatever the user wants.
        public async Task<SearchResult<Person>> GetPersonSearchAsync(string query)
        {
            // Everything here is similar to above except we're using the search url and string concatenation to add the 
                // query (search item) to the end of the search url.
            HttpResponseMessage response = await _httpClient.GetAsync("https://swapi.dev/api/people?search=" + query);

            // Same "if" statement as the "GetAsync" method, but in one fewer line of code
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<SearchResult<Person>>();
            }

            return null;
        }

        // Adding a generic search method. It's a "public" "async" method again that has a return type of "SearchResult"
            // with the generic return type "T". We define the generic type "T" again within the angled brackets of the 
            // "GetSearchAsync" method name. This time we pass in a "string" query and "string" category because we need to
            // know what kind of object we're searching for (e.g., vehicles, people, starships, etc.)
        public async Task<SearchResult<T>> GetSearchAsync<T>(string query, string category)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://swapi.dev/api/{category}?search=" + query);

            return response.IsSuccessStatusCode
                ? await response.Content.ReadAsAsync<SearchResult<T>>()
                : default;
        }

        // Simplified version of specific method "GetVehicleSearchAsync" using the "GetSearchAsync" as a helper.
            // Similar to "GetPersonAsync" method above.
            // You don't have to build a vehicle search async method this way. It's just an option to streamline things.
        public async Task<SearchResult<Vehicle>> GetVehicleSearchAsync(string query)
        {
            return await GetSearchAsync<Vehicle>(query, "vehicles");
        }
    }
}
