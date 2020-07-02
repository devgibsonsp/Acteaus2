
namespace ObjectData.CharacterData.Models
{
    ///<summary>Bar stats are stats that are displayed to the character in the form of bars</summary>
    public class BarStatsModel
    {
        public int _HealthMax { get; set;}

        public int _MagicMax { get; set; }
        ///<summary>Max health a character has</summary>
        public int Health { get; set; }
        ///<summary>Max magic a character has</summary>
        public int Magic { get; set; }
        ///<summary>Culmulative experince a player has obtains</summary>
        public int Experience { get; set; }

    }
}
