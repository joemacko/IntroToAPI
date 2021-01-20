using IntroToAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntroToAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setting up our environment to communicate with http, which will allow us to communicate with an API
            // After typing HttpClient, had to do "Ctrl + ." and click "using System.Net.Http;" for HttpClient to be recognized. 
            // Then newed up an instance of HttpClient called httpClient.
            HttpClient httpClient = new HttpClient();

            // Invoking the "GetAsync" method, which takes in a string and sends a GET request to the specified Uri.
                // That method has a return type of "Task", which is something that needs to be completed.
            // When you have an asynchronous task, it has to be executed at some point, but it won't be done until you tell the program to await it or get a result.
            // The ".Result" property gets the result value of the task and returns it, so that property will get you the HttpResponseMessage itself.
            // The "Wait" method invokes the task and waits for it to be completed, but it has a return type of void. This can be used for tasks that don't return anything.
            HttpResponseMessage response = httpClient.GetAsync("https://swapi.dev/api/people/1").Result;

            // The ".IsSuccessStatusCode" property is a boolean than returns true if the HttpResponseMessage was in the range 200-299 (non-error codes)
                // We put it within an if statement to execute further code if we successfully receive a response
            if (response.IsSuccessStatusCode)
            {
                // Here we're retrieving the content from the get request by creating a string variable and using the "Content" property on our response variable,
                    // the "ReadAsStringAsync()" method, and the ".Result" property
                // If you put a break point above this line and step into the line below, you can hover over "content" and click the magnifying glass to see the Json object better.
                // Right now, "content" is stored as a string, but we need to convert it to a C# object. Check the steps in the "Person" class to see how to do that.
                var content = response.Content.ReadAsStringAsync().Result;
                
                // Creating a "Person" object called "person" then using the "JsonConvert" class (need to "Ctrl + ." and click "using Newtonsoft.Json")
                    // and the "DeserializeObject" method with a "Person" object (need to "Ctrl + ." and click "using IntroToAPI.Models") and pass in a string
                    // (we used "content").
                // We later installed the "Microsoft.AspNet.WebApi.Client" package by right clicking on the "IntroToAPI" assembly and clicking "Manage NuGet Packages". 
                    // We then clicked on "Browse" and searched for "Microsoft.AspNet.WebApi.Client" and installed it.
             // var person = JsonConvert.DeserializeObject<Person>(content);

                // Installing the "Microsoft.AspNet.WebApi.Client" package allows us to use the "ReadAsAsync" method to yield the object of the specified type automatically
                    // instead of reading the content as a string and then converting it to a C# object, which requires more method calls.
                Person luke = response.Content.ReadAsAsync<Person>().Result;
                Console.WriteLine(luke.Name);

                // Writing a foreach loop to write all of Luke's vehicles to the console. The vehicles are passed into C# as strings, not objects,
                    // so we need to do another get request.
                foreach(string vehicleUrl in luke.Vehicles)
                {
                    // Writing another "HttpResponseMessage" to get the result. We are just able to pass in the variable name this time because
                        // C# knows it corresponds to a url (Json key/value pair).
                    HttpResponseMessage vehicleResponse = httpClient.GetAsync(vehicleUrl).Result;
                 //Console.WriteLine(vehicleResponse.Content.ReadAsStringAsync().Result);

                    // We need to create a "Vehicle" class to store the "vehicleResponse" as a C# object. After doing that,
                        // we used the same code as the "Person luke..." line to yield each C# vehicle object.
                    Vehicle vehicle = vehicleResponse.Content.ReadAsAsync<Vehicle>().Result;
                    Console.WriteLine(vehicle.Name);
                }
            }

            // The main method cannot be asynchronous
            // You can't call async methods within methods that aren't async unless you use "await" or ".Result"

            Console.WriteLine();

            // Creating a new SWAPIService instance
            SWAPIService service = new SWAPIService();
            // No "Person" object in this scope yet, so we're creating one here and setting it equal to whatever
                // the "SWAPIService" object returns using the "GetPersonAsync" method, a url, and the ".Result" property.
            Person person = service.GetPersonAsync("https://swapi.dev/api/people/11").Result;
            // Making sure the "person" object isn't null with an if loop
            if (person != null)
            {
                // Writing the "person" object's ".Name" property to the console for proof of concept
                Console.WriteLine(person.Name);
                // Using a nested "foreach" loop to go through each "Person" object's ".Vehicles" property within the vehicleUrl
                foreach(var vehicleUrl in person.Vehicles)
                {
                    // Getting the "Vehicle" objects by using the "SWAPIService" and accessing the "vehicles" through the
                        // "GetVehicleAsync" method and passing in the "vehicleUrl" string and finally using the ".Result" operator
                    var vehicle = service.GetVehicleAsync(vehicleUrl).Result;
                    // Writing out each vehicle name to the console for proof of concept
                    Console.WriteLine(vehicle.Name);
                }
            }

            Console.WriteLine();

            // Using the generic "GetAsync" method to obtain a "Vehicle" object (code very similar to above)
            var genericResponse = service.GetAsync<Vehicle>("https://swapi.dev/api/vehicles/4").Result;
            if(genericResponse != null)
            {
                Console.WriteLine(genericResponse.Name);
            }
            else
            {
                Console.WriteLine("Targeted object does not exist.");
            }

            Console.WriteLine();

            // Passing in the "Person" class to the "SearchResult" class to obtain all the "Person" objects (which we call
                // "skywalkers") with the last name of "skywalker" in a search.
            SearchResult<Person> skywalkers = service.GetPersonSearchAsync("skywalker").Result;
            foreach(Person p in skywalkers.Results)
            {
                Console.WriteLine(p.Name);
            }

            // Searching for a vehicle in two different ways
            var genericSearch = service.GetSearchAsync<Vehicle>("speeder", "vehicles").Result;
            var vehicleSearch = service.GetVehicleSearchAsync("speeder").Result;

            Console.ReadKey();
        }
    }
}
