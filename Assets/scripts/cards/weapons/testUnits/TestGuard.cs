using cards;
using cards.weapons;

namespace Blade.cards.weapons.testUnits {
    public class TestGuard : AbstractUnit{
        public TestGuard() : base(0, 4, 2, WeaponUnit.Guard, AbstractCard.CardRarity.Starter) { }
    }
}