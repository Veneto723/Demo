using System.Collections.Generic;
using characters;
using exceptions;

namespace cards.testCards {
    public class Uppercut : AbstractCard{
        
        public Uppercut() : base("Uppercut", 2, 4, 1, 1, 4,
            1, "", "", CardType.Attack, new List<CardModifier>() { },
            CardRarity.Default, CardTarget.Enemy) { }

        protected override void OnUse(AbstractCharacter source, AbstractCharacter target) {
            // 获取玩家能造成的总伤害
            // TODO 伤害增益百分比
            var damage = source.Weapon.Damage + Damage; // 角色攻击力+卡牌伤害
            // 执行相应动画

            // 造成伤害
            target.TakeDamage(damage, 0);
        }
        
        
        protected override void AfterUse(AbstractCharacter target, AbstractCharacter source) {
            try {
                source.Deck.Draw();
            }
            catch (EmptyDeckException _) {
                source.Grave.ShuffleToDeck(source.Deck);
                source.Deck.Draw();
            }
        }
    }
}