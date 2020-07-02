using ObjectData.CharacterData.Models;
using ObjectData.CharacterData.Types;

namespace ObjectData.CharacterData
{
    public class Character
    {
        public string Name { get; set; }

        public int SkillPoints { get; set; }

        public CharacterType CharClass { get; set; }

       //public LevelType Level { get; set; }

        public int Level { get; set; }
        public BarStatsModel BarStats { get; set; }
        public CoreStatsModel CoreStats { get; set; }
        public ModifierStatsModel ModifierStats { get; set; }

        public Character(string name, int skillPoints, int level, BarStatsModel barStats, CoreStatsModel coreStats, ModifierStatsModel modifierStats)
        {
            Name = name;
            SkillPoints = skillPoints;
            Level = level;
            BarStats = barStats;
            CoreStats = coreStats;
            ModifierStats = modifierStats;
        }

        public Character(RaceType race)
        {
            Level = 1;
            SkillPoints = 10;
            CoreStats = new CoreStatsModel();
            BarStats = new BarStatsModel();
            ModifierStats = new ModifierStatsModel();

            switch(race)
            {
                // * Distribute 25 pts TOTAL

                case RaceType.Human: 
                    CoreStats.Strength += 5;
                    CoreStats.Dexterity += 5;
                    CoreStats.Vitality  += 5;
                    CoreStats.intellect += 5;
                    CoreStats.Wisdom += 5;
                    break;
            }

        }

        public void SetClass(CharacterType charClass)
        {
            CharClass = charClass;
            // * Distribute 5 pts TOTAL

            switch(charClass)
            {
                case CharacterType.Warrior: 
                    CoreStats.Strength += 2;
                    CoreStats.Dexterity += 1;
                    CoreStats.Vitality  += 2;
                    break;
                case CharacterType.Rogue: 
                    CoreStats.Strength += 1;
                    CoreStats.Dexterity += 2;
                    CoreStats.Vitality  += 2;
                    break;
                case CharacterType.Wizard: 
                    CoreStats.Vitality  += 1;
                    CoreStats.intellect += 2;
                    CoreStats.Wisdom += 2;
                    break;
            }
        }

        public void DistributeSkillPoint(CoreStatType stat)
        {
            // If there are skillpoints to distribute...
            // If _Recalculate is passed, this will be skipped and calc func will run solely
            if(SkillPoints > 0 && stat != CoreStatType._Recalculate)
            {
                switch(stat)
                {
                    case CoreStatType.Strength: 
                        CoreStats.Strength += 1;
                        break;
                    case CoreStatType.Dexterity: 
                        CoreStats.Dexterity += 1;
                        break;
                    case CoreStatType.Vitality: 
                        CoreStats.Vitality += 1;
                        break;
                    case CoreStatType.Wisdom: 
                        CoreStats.Wisdom += 1;
                        break;
                    case CoreStatType.intellect: 
                        CoreStats.intellect += 1;
                        break;
                }
                SkillPoints -= 1;
            }

            // Omnipotent call to set stats (can be used to reset stats on adjustment?)
            CalculateStats();
        }

        private void CalculateStats()
        {
            switch(CharClass)
            {
                case CharacterType.Warrior: 
                    ModifierStats.AttackSpeed     = (CoreStats.Dexterity/3) + 1;
                    ModifierStats.BaseMeleeDamage = (CoreStats.Strength /2) + Level     + 2;
                    ModifierStats.BaseSpellDamage = (CoreStats.Wisdom   /3) + (Level/2) + 1;
                    ModifierStats.CounterChance   = (CoreStats.Dexterity/2) + 2;
                    ModifierStats.DodgeChance     = (CoreStats.Dexterity/3) + 1;
                    ModifierStats.MovementSpeed   = (CoreStats.Dexterity/2) + 1;
                    BarStats._HealthMax           = (CoreStats.Vitality *2) * Level     + 20;
                    BarStats._MagicMax            = (CoreStats.intellect)   * Level;
                    break;
                case CharacterType.Rogue: 
                    ModifierStats.AttackSpeed     = (CoreStats.Dexterity/2) + 3;
                    ModifierStats.BaseMeleeDamage = (CoreStats.Strength /2) + Level + 1;
                    ModifierStats.BaseSpellDamage = (CoreStats.Wisdom   /3) + Level + 1;
                    ModifierStats.CounterChance   = (CoreStats.Dexterity/2) + 2;
                    ModifierStats.DodgeChance     = (CoreStats.Dexterity/2) + 2;
                    ModifierStats.MovementSpeed   = (CoreStats.Dexterity/2) + 2;
                    BarStats._HealthMax           = (CoreStats.Vitality)    * Level + 15;
                    BarStats._MagicMax            = (CoreStats.intellect)   * Level + 2;
                    break;
                case CharacterType.Wizard: 
                    ModifierStats.AttackSpeed     = (CoreStats.Dexterity/3) + 1;
                    ModifierStats.BaseMeleeDamage = (CoreStats.Strength /3) + Level + 1;
                    ModifierStats.BaseSpellDamage = (CoreStats.Wisdom   /2) + Level + 2;
                    ModifierStats.CounterChance   = (CoreStats.Dexterity/3) + 1;
                    ModifierStats.DodgeChance     = (CoreStats.Dexterity/3) + 1;
                    ModifierStats.MovementSpeed   = (CoreStats.Dexterity/3) + 1;
                    BarStats._HealthMax           = (CoreStats.Vitality)    * Level + 5;
                    BarStats._MagicMax            = (CoreStats.intellect)   * Level + 20;
                    break;
            }

            BarStats.Health           = BarStats._HealthMax;
            BarStats.Magic            = BarStats._MagicMax;
            ModifierStats.Armor       = 0;
            ModifierStats.BlockChance = 0;
        }

    }
}
