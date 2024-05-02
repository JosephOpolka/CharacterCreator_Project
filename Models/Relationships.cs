using System.Data.Common;

namespace CharacterCreator_Project.Models {
    public class Relationships {
        public int RelationshipId { get; set; }
        public int CharacterId1 { get; set; }
        public int CharacterId2 { get; set; }
        public string RelationshipStatus { get; set; }
        public string CharacterName1 { get; set; }
        public string CharacterName2 { get; set; }
        public string CharacterImage1 { get; set; }
        public string CharacterImage2 { get; set; }
    }
}