using UnityEngine;
using System.Collections;
using Global;
namespace Model
{
/// <summary>
/// Inventory 这里unity里的物品类一定不能继承 MonoBehaviour 
/// 否者在遍历数组的时候所有调用这个类的都会为空值
/// </summary>
    public class Inventory:BaseInventory
    {
        #region//变量：ID 名称 图标 类型  等级 个数() 售价 星级 品质 伤害 生命 战斗力 作用类 作用值 描述


        //附加：物品的作用值属性
        private int _level = 0;

        private int _hp = 0;
        private int _mp = 0;
        private int _atk = 0;
        private int _def = 0;
        private int _dex = 0;
        private int _ctx = 0;
        private int _ctk = 0;


        //复杂属性--暂不做
        private int _starLevel = 1;
        private int _quality = 1;
        private int _damage = 0;
        private int _power = 0;
        #endregion
        #region//属性：ID Name Icon  类型  Level Count PriceSale StarLevle Quality Damage HP Power Infotype ApplyValue Des 
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
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
        public int MP
        {
            get { return _mp; }
            set { _mp = value; }
        }
        public int ATK
        {
            get { return _atk; }
            set { _atk = value; }
        }
        public int DEF
        {
            get { return _def; }
            set { _def = value; }
        }
        public int DEX
        {
            get { return _dex; }
            set { _dex = value; }
        }
        public int CTX
        {
            get { return _ctx; }
            set { _ctx = value; }
        }
        public int CTK
        {
            get { return _ctk; }
            set { _ctk = value; }
        }


        public int StarLevle
        {
            get
            {
                return this._starLevel;
            }
            set
            {
                this._starLevel = value;
            }
        }
        public int QuaLity
        {
            get
            {
                return this._quality;
            }
            set
            {
                this._quality = value;
            }
        }
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

        private EquipType _equipType;
        public EquipType EquipTYPE
        {
            get { return _equipType; }
            set { _equipType = value; }
        }
        #endregion
        /// <summary>
        /// 物品子类
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="mp"></param>
        /// <param name="atk"></param>
        /// <param name="def"></param>
        /// <param name="dex"></param>
        /// <param name="ctx"></param>
        /// <param name="ctk"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="icon"></param>
        /// <param name="inventoryType"></param>
        /// <param name="priceSale"></param>
        /// <param name="priceBuy"></param>
        /// <param name="des"></param>
        public Inventory(int level,int hp, int mp, int atk, int def, int dex, int ctx, int ctk,
    int id, string name, string icon, InventoryType inventoryType, int priceSale, int priceBuy, string des,EquipType equipType)
            : base(id, name, icon, inventoryType, priceSale, priceBuy, des)
        {
            this._level = level;
            this._hp = hp;
            this._mp = mp;
            this._atk = atk;
            this._def = def;
            this._dex = dex;
            this._ctx = ctx;
            this._ctk = ctk;
            this._equipType = equipType;
        }

    }
}
