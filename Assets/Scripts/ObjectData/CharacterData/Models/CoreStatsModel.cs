
namespace ObjectData.CharacterData.Models
{
    ///<summary>Core stats are primary stats governing a character</summary>
    public class CoreStatsModel
    {
        ///<summary>Strength governs character's damage and ability to wear heavier gear</summary>
        public int Strength { get; set; }
        ///<summary>Dexterity governs character's ability to dodge, sneak, and land counter attacks</summary>
        public int Dexterity { get; set; }
        ///<summary>Vitality governs character's overall health and ability to regenerate health</summary>
        public int Vitality { get; set; }
        ///<summary>Wisdom governs character's spell damage (revise)</summary>
        public int Wisdom { get; set; }
        ///<summary>Intellect governs character's spell damage (revise)</summary>
        public int intellect { get; set; }
    }
}
