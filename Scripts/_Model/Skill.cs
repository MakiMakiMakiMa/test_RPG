using UnityEngine;
using System.Collections;
using Global;
namespace Model
{
    public class Skill
    {

        #region//技能字段:ID 名称  图标  玩家类型
        private int id; //技能唯一ID
        private string name;//技能名称
        private string icon;//技能图标
        private PlayerType playerType;//技能持有者玩家类型
        private SkillType skillType;//攻击类 基础和技能
        private SkillPosType posType;//技能位置类型 
        private int coldTime;//技能冷却时间
        private int damage;//技能伤害
        private int level;//技能等级
        #endregion
        #region//技能属性
        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }

        }
        public string ICON
        {
            get
            {
                return this.icon;
            }
            set
            {
                this.icon = value;
            }
        }
        public PlayerType PlayerTYPE
        {
            get
            {
                return this.playerType;
            }
            set
            {
                this.playerType = value;
            }
        }
        public SkillType Skilltype
        {
            get
            {
                return this.skillType;
            }
            set
            {
                this.skillType = value;
            }
        }
        public SkillPosType Postype
        {
            get
            {
                return this.posType;
            }
            set
            {
                this.posType = value;
            }
        }
        public int ColdTime
        {
            get
            {
                return this.coldTime;
            }
            set
            {
                this.coldTime = value;
            }
        }
        public int Damage
        {
            get
            {
                return this.damage;
            }
            set
            {
                this.damage = value;
            }
        }
        public int Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }
        #endregion

    }
}
