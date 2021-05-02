using System;
using cards;
using characters;
using characters.buffs;

namespace actions {
    public class BuffAction : AbstractAction {
        private readonly Buff _buff;
        private readonly bool _isTakeBuff;

        /// <summary>
        /// Buff Action 构造函数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="buff">Buff</param>
        /// <param name="isTakeBuff">是否是施加Buff，true：施加，false:消除</param>
        /// <exception cref="ArgumentNullException"></exception>
        public BuffAction(AbstractCharacter source, AbstractCharacter target, Buff buff, AbstractCard card, bool isTakeBuff) : base(source,
            target, card) {
            _buff = buff ?? throw new ArgumentNullException(nameof(buff));
            _isTakeBuff = isTakeBuff;
        }

        public override void OnAct() {
            if (_isTakeBuff) {
                Target.TakeBuff(_buff);
            }
            else {
                Target.DispelBuff(_buff);
            }
        }
    }
}