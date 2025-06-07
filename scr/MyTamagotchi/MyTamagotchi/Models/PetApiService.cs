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

        public static async Task<bool> SavePetAsync(Pet pet)
        {
            string petJson = JsonSerializer.Serialize(new
            {
                name = pet.Name,
                hunger = pet.Hunger,
                energy = pet.Energy,
                mood = pet.Mood,
                ownerid = pet.OwnerId,
                imagepath = pet.ImagePath
            });

            StringContent content = new StringContent(petJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            if (pet.Id == 0)
            {
                // neues Tier anlegen
                response = await client.PostAsync("http://localhost:5000/pets", content);
            }
            else
            {
                // bestehendes Tier updaten
                response = await client.PutAsync($"http://localhost:5000/pets/{pet.Id}", content);
            }
            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeletePetAsync(int petId)
        {
            HttpResponseMessage response = await client.DeleteAsync($"http://localhost:5000/pets/{petId}");
            return response.IsSuccessStatusCode;
        }

        public static async Task<User> GetUserid(string username, string password)
        {
            string userJson = JsonSerializer.Serialize(new
            {
                username = username,
                password = password
            });
            StringContent content = new StringContent(userJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"http://localhost:5000/auth/login", content);

            string json = await response.Content.ReadAsStringAsync();
            User user = JsonSerializer.Deserialize<User>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return user;
        }
    }
}
