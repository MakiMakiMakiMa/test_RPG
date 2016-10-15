using UnityEngine;
using System.Collections;
namespace Model
{
    public class PlayerUpGradeInfo 
    {
        private int _level =1;
        private int _exp = 100;
        private int _hpPlus = 0;
        private int _mpPlus = 0;
        private int _attackPlus = 0;
        private int _defencePlus = 0;
        private int _dexterityPlus = 0;
        private float _criticalTriggerRatePlus = 0f;//暴击率初始值, 按职业不同而不同
        private float _criticalAttackPlus = 0f;//暴击率初始值为自身伤害, 按职业不同而不同
        /// <summary>
        /// 需要升级到的等级
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        /// <summary>
        /// 需要满足的经验值
        /// </summary>
        public int Exp
        {
            get { return _exp; }
            set { _exp = value; }
        }
        /// <summary>
        /// 升级增加的最大血量值的增量
        /// </summary>
        public int HpPlus
        {
            get { return _hpPlus; }
            set { _hpPlus = value; }
        }
        /// <summary>
        /// 升级增加的最大魔法值的增量
        /// </summary>
        public int MpPlus
        {
            get { return _mpPlus; }
            set { _mpPlus = value; }
        }
        /// <summary>
        /// 升级增加的最大攻击值的增量
        /// </summary>
        public int AttackPlus
        {
            get { return _attackPlus; }
            set { _attackPlus = value; }
        }
        /// <summary>
        /// 升级增加的最大防御值的增量
        /// </summary>
        public int DefencePlus
        {
            get { return _defencePlus; }
            set { _defencePlus = value; }
        }
        /// <summary>
        /// 升级增加的最大敏捷值的增量
        /// </summary
        public int DexterityPlus
        {
            get { return _dexterityPlus; }
            set { _dexterityPlus = value; }
        }
        /// <summary>
        /// 升级增加的最大暴击触发率的增量
        /// </summary
        public float CriticalTriggerRatePlus
        {
            get { return _criticalTriggerRatePlus; }
            set { _criticalTriggerRatePlus = value; }
        }
        /// <summary>
        /// 升级增加的最大暴击伤害比例的增量
        /// </summary
        public float CriticalAttackPlus
        {
            get { return _criticalAttackPlus; }
            set { _criticalAttackPlus = value; }
        }

    }
}
