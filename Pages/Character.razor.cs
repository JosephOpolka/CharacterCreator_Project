using Microsoft.AspNetCore.Components;
using CharacterCreator_Project.Models;
using System.Net.Http.Json;

namespace CharacterCreator_Project.Pages {
    public partial class Character : ComponentBase {
        [Inject]
        public HttpClient? HttpClient { get; set; }

        public Characters character = new Characters();
        public string Message { get; set; } = "";

        public async Task AddCharacter() {
            if (HttpClient == null) return;

            var response = await HttpClient.PostAsJsonAsync("http://localhost:5041/characters", character);
            if (response.IsSuccessStatusCode) {
                Message = "Character added successfully";
                character = new Characters(); // Reset the model
            } else {
                Message = "Failed to add character";
            }
        }
    }
}