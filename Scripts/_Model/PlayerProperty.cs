using UnityEngine;
using System.Collections;
using Global;
namespace Model
{
    /// <summary>
    /// 玩家当前属性
    /// </summary>
    public class PlayerProperty
    {
        #region// 玩家信息：角色类型 姓名 头像 等级  经验 钻石 金币 体力 历练      
        private PlayerType _playerType = PlayerType.Warrior;
        private string _name="YingJiu";
        private string _headPortait = "portraits.pvr.ccz_0";
        private int _level=1;
        private int _exp=0;
        private int _expUpGrade = 100;

        private int _diamond=100;
        private int _coin=10000;
        private int _energy=100;
        private int _toughen=50;
        public float energyTime = 0;
        public float toughenTime = 0;
        private int vip = 12;
        #endregion
        #region//补充信息：HP MP 攻击力 防御力 灵敏度  暴击率 暴击攻击 伤害值 战斗力
        private int _hp =1000;
        private int _mp = 100;
        private int _attack = 10;
        private int _defence = 5;
        private int _dexterity = 3;
        private float _criticalTriggerRate = 0f;
        private float _criticalAttackRate = 1f;
        private int _hpMax = 1000;
        private int _mpMax =100;
        private int _attackMax = 10;
        private int _defenceMax =5;
        private int _dexterityMax = 3;
        private float _criticalTriggerRateMax = 0f;//暴击率初始值, 按职业不同而不同
        private float _criticalAttackMax = 1f;//暴击率初始值为自身伤害, 按职业不同而不同

        private int _power = 0;
        private int _damage = 0;
        #endregion
        #region//玩家信息对应属性
        /// <summary>
        /// 玩家职业类型
        /// </summary>
        public PlayerType PlayerTYPE
        {
            get
            {
                return this._playerType;
            }
            set
            {
                this._playerType = value;
            }
        }
        /// <summary>
        /// 玩家姓名
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }
        /// <summary>
        /// 玩家头像
        /// </summary>
        public string HeadPortrait
        {
            get
            {
                return this._headPortait;
            }
            set
            {
                this._headPortait = value;
            }
        }
        /// <summary>
        /// 玩家级别
        /// </summary>
        public int Level
        {
            get
            {
                return this._level;
            }
            set
            {
                this._level = value;
            }
        }
        /// <summary>
        /// 升级经验
        /// </summary>
        public int Exp
        {
            get
            {
                return this._exp;
            }
            set
            {
                this._exp = value;
            }
        }

        public int ExpUpGrade
        {
            get { return _expUpGrade; }
            set { _expUpGrade = value; }
        }
        /// <summary>
        /// 玩家元宝
        /// </summary>
        public int Diamond
        {
            get
            {
                return this._diamond;
            }
            set
            {
                this._diamond = value;
            }
        }
        /// <summary>
        /// 玩家金币
        /// </summary>
        public int Coin
        {
            get
            {
                return this._coin;
            }
            set
            {
                this._coin = value;
            }
        }
        /// <summary>
        /// 体力
        /// </summary>
        public int Energy
        {
            get
            {
                return this._energy;
            }
            set
            {
                this._energy = value;
            }
        }
        /// <summary>
        /// 历练
        /// </summary>
        public int Toughen
        {
            get
            {
                return this._toughen;
            }
            set
            {
                this._toughen = value;
            }
        }
        /// <summary>
        /// Vip等级
        /// </summary>
        public int VIP
        {
            get
            {
                return this.vip;
            }
            set
            {
                this.vip = value;
            }
        }
   
        #endregion
        #region 补充信息对应属性
        /// <summary>
        /// 气血
        /// </summary>
        public int HP
        {
            get
            {
                return this._hp;
            }
            set
            {
                this._hp = value;
            }
        }
        /// <summary>
        /// 魔法
        /// </summary>
        public int MP
        {
            get { return _mp; }
            set { _mp = value; }
        }
        /// <summary>
        /// 攻击里
        /// </summary>
        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }
        /// <summary>
        /// 防御力
        /// </summary>
        public int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }
        /// <summary>
        /// 敏捷度
        /// </summary>
        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; }
        }
        /// <summary>
        /// 暴击率
        /// </summary>
        public float CriticalTriggerRate
        {
            get { return _criticalTriggerRate; }
            set { _criticalTriggerRate = value; }
        }
        /// <summary>
        /// 暴击攻击比率
        /// </summary>
        public float CriticalAttackRate
        {
            get { return _criticalAttackRate; }
            set { _criticalAttackRate = value; }
        }


        /// <summary>
        /// 气血总值(最大血量)
        /// </summary>
        public int HPMax
        {
            get { return _hpMax; }
            set { _hpMax = value; }
        }
        /// <summary>
        /// 魔法总值（最大魔法值）
        /// </summary>
        public int MPMax
        {
            get { return _mpMax; }
            set { _mpMax = value; }
        }
        /// <summary>
        /// 最大攻击力
        /// </summary>
        public int AttackMax
        {
            get { return _attackMax; }
            set { _attackMax = value; }
        }
        /// <summary>
        /// 最大防御值
        /// </summary>
        public int DefenceMax
        {
            get { return _defenceMax; }
            set { _defenceMax = value; }
        }
        /// <summary>
        /// 最大灵敏度
        /// </summary>
        public int DexterityMax
        {
            get { return _dexterityMax; }
            set { _dexterityMax = value; }
        }
        /// <summary>
        /// 最大暴击触发率
        /// </summary>
        public float CriticalTriggerRateMax
        {
            get { return _criticalTriggerRateMax; }
            set { _criticalTriggerRateMax = value; }
        }
        /// <summary>
        /// 暴击攻击比率
        /// </summary>
        public float CriticalAttackMax
        {
            get { return _criticalAttackMax; }
            set { _criticalAttackMax = value; }
        }
        /// <summary>
        /// 玩家伤害
        /// </summary>
        public int Damage
        {
            get
            {
                return this._damage;
            }
            set
            {
                this._damage = value;
            }
        }
        /// <summary>
        /// 玩家战斗力
        /// </summary>
        public int Power
        {
            get
            {
                return this._power;
            }
            set
            {
                this._power = value;
            }
        }
        #endregion
    }
}

