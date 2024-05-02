using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using CharacterCreator_Project.Models;


namespace CharacterCreator_Project.Pages {
    public partial class Tables: ComponentBase {
        [Inject]
        public HttpClient? HttpClient { get; set; }

        private List<Characters>? Characters { get; set; }
        private List<Relationships>? Relationships { get; set; }

        public async Task RetrieveCharacters() {
            this.Characters = await HttpClient.GetFromJsonAsync<List<Characters>>("http://localhost:5041/characters");
            Console.WriteLine(this.Characters);

            StateHasChanged();
        }

        public async Task RetrieveRelationships() {
            this.Relationships = await HttpClient.GetFromJsonAsync<List<Relationships>>("http://localhost:5041/relationships");
        }

        protected override async Task OnInitializedAsync() {
            RetrieveCharacters();
        }
        public string GetImagePath(string favoriteColor)
        {
            return string.IsNullOrEmpty(favoriteColor) ? "pfp/PFP.png" : $"pfp/PFP_{favoriteColor}.png";
        }
    }
}