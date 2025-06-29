using ScoreBoard.data.character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard.data.artifact
{
    public class Artifact
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual void Equip(CorpsMember member) { }
        public virtual void Unequip(CorpsMember member) { }

        private enum ArtifactType
        {
            Weapon = 0,
            Armour = 1,
            Accessory = 2
        }
    }
}
