using UnityEngine;
using System.Collections;
using Global;
namespace Model
{
    /// <summary>
    /// 怪物属性
    /// </summary>
    public class EnemyProperty 
    {
        //编号|类型|名称|等级|血量|攻击|防御|速度|致命一击|掉落ID|搜索范围|攻击距离
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private EnemyType _type;

        public EnemyType TYPE
        {
            get { return _type; }
            set { _type = value; }
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private int _level = 0;

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
        private int _dropID = 1001;//默认掉落

        public int DropID
        {
            get { return _dropID; }
            set { _dropID = value; }
        }

        private float _canFindPlayer;

        public float CanFindPlayer
        {
            get { return _canFindPlayer; }
            set { _canFindPlayer = value; }
        }
        private float _canAttackplayer;

        public float CanAttackplayer
        {
            get { return _canAttackplayer; }
            set { _canAttackplayer = value; }
        }





        private int _hp = 100;

        public int HP
        {
            get { return _hp; }
            set { _hp = value; }
        }
        private int _mp = 100;

        public int MP
        {
            get { return _mp; }
            set { _mp = value; }
        }
        private int _attack = 10;

        public int Attack
        {
            get { return _attack; }
            set { _attack = value; }
        }
        private int _defence = 5;

        public int Defence
        {
            get { return _defence; }
            set { _defence = value; }
        }
        private int _dexterity = 3;

        public int Dexterity
        {
            get { return _dexterity; }
            set { _dexterity = value; }
        }
        private float _criticalTriggerRate = 0f;//暴击率初始值, 按职业不同而不同

        public float CriticalTriggerRate
        {
            get { return _criticalTriggerRate; }
            set { _criticalTriggerRate = value; }
        }
        private float _criticalAttackRate = 1f;//暴击率初始值为自身伤害, 按怪物不同而不同

        public float CriticalAttackRate
        {
            get { return _criticalAttackRate; }
            set { _criticalAttackRate = value; }
        }
        
    }
}