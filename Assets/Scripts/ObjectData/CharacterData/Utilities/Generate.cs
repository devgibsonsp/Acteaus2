using ObjectData.CharacterData.Models;

namespace ObjectData.CharacterData.Utilties
{
    public static class Generate
    {
        public static CoreStatsModel CoreStats()
        {
            return new CoreStatsModel
            {
                Strength = 5,
                Dexterity = 5,
                intellect = 5,
                Wisdom = 5,
                Vitality = 5,
            };
        }

        public static ModifierStatsModel ModifierStats()
        {
            return new ModifierStatsModel
            {
                DodgeChance = 5,
                CounterChance = 5,
                BlockChance = 5,
                AttackSpeed = 5,
                MovementSpeed = 5,
                BaseMeleeDamage = 5,
                BaseSpellDamage = 5,
            };
        }

        public static BarStatsModel BarStats()
        {
            return new BarStatsModel
            {
                Health = 50,
                Magic = 50,
                Experience = 0
            };
        }
    }
}