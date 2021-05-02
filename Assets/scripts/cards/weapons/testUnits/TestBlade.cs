using System.Collections.Generic;
using cards;
using cards.weapons;

namespace Blade.cards.weapons.testUnits {
    public class TestBlade : AbstractUnit{
        public TestBlade() : base(6, 0, 8, WeaponUnit.Blade, AbstractCard.CardRarity.Starter) { }
    }
}