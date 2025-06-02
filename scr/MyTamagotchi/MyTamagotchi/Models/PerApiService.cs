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

        // Holt ALLE Haustiere von der API
        public static async Task<List<Pet>> GetPetsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/pets");
                string json = await response.Content.ReadAsStringAsync();

                List<Pet> pets = JsonSerializer.Deserialize<List<Pet>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return pets ?? new List<Pet>();
            }
            catch (Exception)
            {
                return new List<Pet>();
            }
        }

        // Holt NUR die Haustiere eines bestimmten Users
        public static async Task<List<Pet>> GetOwnerPets(int ownerid)
        {
            HttpResponseMessage response = await client.GetAsync($"http://localhost:5000/pets?ownerid={ownerid}");
            string json = await response.Content.ReadAsStringAsync();

            List<Pet> pets = JsonSerializer.Deserialize<List<Pet>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return pets ?? new List<Pet>();
        }


        // Speichert geändertes Tier in der Datenbank
        public static async Task<bool> UpdatePets(Pet updatePet)
        {
            string updatedpets = JsonSerializer.Serialize(new
            {
                name = updatePet.Name,
                hunger = updatePet.Hunger,
                energy = updatePet.Energy,
                mood = updatePet.Mood,
                imagepath = updatePet.ImagePath  // WICHTIG!
            });

            StringContent content = new StringContent(updatedpets, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(
                $"http://localhost:5000/pets/{updatePet.Id}",
                content
            );

            return response.IsSuccessStatusCode;
        }
    }
}
