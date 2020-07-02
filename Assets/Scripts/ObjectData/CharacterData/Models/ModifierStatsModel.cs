namespace ObjectData.CharacterData.Models
{
    public class ModifierStatsModel
    {
        public int _DodgeChanceMax { get; } = 35;
        public int _CounterChanceMax { get; } = 35;
        public int _BlockChanceMax{ get; } = 35;

        public int DodgeChance { get; set;}
        public int CounterChance { get; set; }
        public int BlockChance { get; set; }

        public float AttackSpeed { get; set; }
        public int MovementSpeed { get; set; }
        public int BaseMeleeDamage { get; set; }
        public int BaseSpellDamage { get; set; }

        public int Armor { get; set;}
    }
}