using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyTamagotchi.Models
{
    public class PetApiService
    {
        private static readonly HttpClient client = new HttpClient();

        // Basis-URL deiner API (später anpassen)
        //private static readonly string baseUrl = "https://example.com/api/pets"; // FAKE-URL

        public static async Task<List<Pet>> GetPetsAsync()
        {
            try
            {
                // WICHTIG: Hier simulieren wir erstmal
                await Task.Delay(1000); // Simulation: Warten auf Antwort
                return new List<Pet> { new Pet("API-Fluffy") };
            }
            catch (Exception)
            {
                return new List<Pet>();
            }
            
        }
        public static async Task SavePetAsync(Pet pet)
        {
            try
            {
                await Task.Delay(1000); // Simulation
                // In echt:
                // await client.PostAsJsonAsync(baseUrl, pet);
            }
            catch (Exception)
            {
                // Fehler ignorieren
            }
        //}
        //public async Task<List<Pet>> GetTestPets()
        //{
        //    HttpResponseMessage response = await client.GetAsync("http://localhost:5000/pets");
        //    string json = await response.Content.ReadAsStringAsync();
        //    List<Pet> pets = JsonSerializer.Deserialize<List<Pet>>(json);
        //    return pets;
        //}

        public static async Task<List<Pet>> GetOwnerPets(int ownerid)
        {
            HttpResponseMessage response = await client.GetAsync($"http://localhost:5000/pets?ownerid={ownerid}");
            string json = await response.Content.ReadAsStringAsync();
            List<Pet> pets = JsonSerializer.Deserialize<List<Pet>>(json);
            return pets;     
        }

        public static async Task<bool> UpdatePets(Pet updatePet )
        {
            string updatedpets = JsonSerializer.Serialize(new
            {
                name = updatePet.Name,
                hunger = updatePet.Hunger,
                energy = updatePet.Energy,
                mood = updatePet.Mood,
            });
            
            StringContent content = new StringContent(updatedpets);
            HttpResponseMessage response = await client.PutAsync($"http://localhost:5000/pets/{updatePet.Id}",content);
            return response.IsSuccessStatusCode ;

        }


    }
}
