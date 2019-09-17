using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoEBrowser.Models
{
    public class SkillGems
    {
        public SkillGems()
        {
            DexSkillGems = new List<SkillGem>();
            IntSkillGems = new List<SkillGem>();
            StrSkillGems = new List<SkillGem>();
            OtherSkillGems = new List<SkillGem>();
        }

        public List<SkillGem> DexSkillGems { get; set; }
        public List<SkillGem> IntSkillGems { get; set; }
        public List<SkillGem> StrSkillGems { get; set; }
        public List<SkillGem> OtherSkillGems { get; set; }
    }
}
