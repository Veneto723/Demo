using Blade.cards.weapons.testUnits;

namespace cards.weapons {
    public class TestWeapon : AbstractWeapon {

        public TestWeapon(string img, string description) : base(img, description) {
            Pommel = new TestPommel();
            Grip = new TestGrip();
            Blade = new TestBlade();
            Guard = new TestGuard();
            Init();
        }
    }
}