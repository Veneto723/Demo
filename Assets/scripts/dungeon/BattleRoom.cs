using System.Collections.Generic;
using characters;
using characters.buffs;

namespace dungeon {
    public class BattleRoom : AbstractRoom {
        private bool _isPlayerTurn = true;

        public bool IsPlayerTurn { get; }

        public BattleRoom(AbstractMonster enemy) : base(RoomType.Battle) {
            IsPlayerTurn = true;
            Enemy = enemy;
        }

        /// <summary>
        /// 结束并交换回合
        /// </summary>
        public void EndTurn() {
            _isPlayerTurn = !_isPlayerTurn;
            if (_isPlayerTurn) {
                Player.CostRecover();
                Player.DispelBuff(new Combo());
            }
            else {
                Enemy.CostRecover();
                Enemy.DispelBuff(new Combo());
            }
        }

        /// <summary>
        /// 检测战斗结束
        /// </summary>
        /// <returns></returns>
        public bool CheckRoomEnd() {
            return Enemy.Dying() || Player.Dying();
        }
    }
}