using UnityEngine;
using System.Collections;
using Global;

namespace Model
{
    public class BaseInventory
    {
        #region//变量：ID 名称 图标 类型  等级 个数() 售价 星级 品质 伤害 生命 战斗力 作用类 作用值 描述
        private InventoryType _inventoryType;

        private int _id;
        private string _name;
        private string _icon;

        private int _priceSale = 0;
        private int _priceBuy = 0;

        private string _des;
        #endregion
        #region//属性：ID Name Icon  类型  Level Count PriceSale StarLevle Quality Damage HP Power Infotype ApplyValue Des
        public int ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
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
        public string ICON
        {
            get
            {
                return this._icon;
            }
            set
            {
                this._icon = value;
            }
        }
        public InventoryType InventoryTYPE
        {
            get
            {
                return this._inventoryType;
            }
            set
            {
                this._inventoryType = value;
            }
        }
        public int PriceSale
        {
            get
            {
                return this._priceSale;
            }
            set
            {
                this._priceSale = value;
            }
        }
        public int PriceBuy
        {
            get { return _priceBuy; }
            set { _priceBuy = value; }
        }
        public string Des
        {
            get
            {
                return this._des;
            }
            set
            {
                this._des = value;

            }
        }     
        #endregion
        /// <summary>
        /// 物品基类：包括物品ID,Name,ICON,Name,InventoryTYPE,PriceSale,PriceBuy,Des
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="icon"></param>
        /// <param name="inventoryType"></param>
        /// <param name="priceSale"></param>
        /// <param name="priceBuy"></param>
        /// <param name="des"></param>
        public BaseInventory(int id, string name, string icon, InventoryType inventoryType, int priceSale, int priceBuy, string des)
        {
            this._id = id;
            this._name = name;
            this._icon = icon;
            this._inventoryType = inventoryType;
            this._priceSale = priceSale;
            this._priceBuy = priceBuy;
            this._des = des;
        }

    }
}
