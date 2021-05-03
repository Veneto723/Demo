using System.Collections.Generic;
using characters;

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
            }
            else {
                Enemy.CostRecover();
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