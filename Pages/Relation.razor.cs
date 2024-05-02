using Microsoft.AspNetCore.Components;
using CharacterCreator_Project.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CharacterCreator_Project.Pages {
    public partial class Relation : ComponentBase {
        [Inject]
        public HttpClient? HttpClient { get; set; }
        public Relationships RelationModel { get; set; } = new Relationships();
        public string Message { get; set; } = "";

        public async Task AddRelationship() {
            if (HttpClient == null) return;

            var response = await HttpClient.PostAsJsonAsync("http://localhost:5041/relationships", RelationModel);
            if (response.IsSuccessStatusCode) {
                Message = "Relationship added successfully";
                RelationModel = new Relationships(); // Reset the model with a new instance
            } else {
                Message = "Failed to add relationship: " + await response.Content.ReadAsStringAsync();
            }
        }

        public async Task FetchCharacterDetails(int characterId, bool isFirstCharacter) {
            try {
                var response = await HttpClient.GetFromJsonAsync<Characters>($"http://localhost:5041/characters/{characterId}");
                if (response != null) {
                    if (isFirstCharacter) {
                        RelationModel.CharacterName1 = response.Name;
                        RelationModel.CharacterImage1 = GetImagePath(response.FavoriteColor);
                    } else {
                        RelationModel.CharacterName2 = response.Name;
                        RelationModel.CharacterImage2 = GetImagePath(response.FavoriteColor);
                    }
                    StateHasChanged(); // Ensure the UI updates with new data
                } else {
                    Message = "Character not found.";
                }
            } catch (Exception ex) {
                Message = "Error fetching character details: " + ex.Message;
            }
        }

        private string GetImagePath(string favoriteColor) {
            return string.IsNullOrEmpty(favoriteColor) ? "pfp/PFP.png" : $"pfp/PFP_{favoriteColor}.png";
        }

        public async Task TryFetchCharacterDetails(string input, bool isFirstCharacter)
        {
            if (int.TryParse(input, out int characterId))
            {
                await FetchCharacterDetails(characterId, isFirstCharacter);
            }
            else
            {
                Message = "Please enter a valid ID number.";
            }
        }
    }
}