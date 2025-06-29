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
        public string Id { get; set; } = string.Empty; // 유물 Id: [타입]_[번호]_[이름] (예: "0_01_DingDingSword")
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ArtifactType Type { get; set; } = ArtifactType.Weapon;
        public ArtifactRarity Rarity { get; set; } = ArtifactRarity.Normal; // 유물의 희귀도 (Normal, Rare, Legendary)

        public virtual void Equip(CorpsMember member) { }
        public virtual void Unequip(CorpsMember member) { }

        public enum ArtifactRarity
        {
            Normal = 0,
            Rare = 1,
            Legendary = 2
        }

        public enum ArtifactType
        {
            Weapon = 0,
            Armour = 1,
            Accessory = 2
        }
    }
}
