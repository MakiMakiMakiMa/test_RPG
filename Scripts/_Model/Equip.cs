using UnityEngine;
using System.Collections;

namespace Model
{
    public class Equip 
    {
        //角色身上装备信息 
        private InventoryItem _helmItem;
        private InventoryItem _clothItem;
        private InventoryItem _weaponItem;
        private InventoryItem _shoesItem;
        private InventoryItem _necklaceItem;
        private InventoryItem _braceletItem;
        private InventoryItem _ringItem;
        private InventoryItem _wingItem;
        //玩家装备ID属性
        /// <summary>头盔 </summary>
        public InventoryItem HelmItem
        {
            get
            {
                return this._helmItem;
            }
            set
            {
                this._helmItem = value;
            }
        }
        /// <summary>胸甲 </summary>
        public InventoryItem ClothItem
        {
            get
            {
                return this._clothItem;
            }
            set
            {
                this._clothItem = value;
            }

        }
        /// <summary>武器 </summary>
        public InventoryItem WeaponItem
        {
            get
            {
                return this._weaponItem;
            }
            set
            {
                this._weaponItem = value;
            }
        }
        /// <summary>靴子 </summary>
        public InventoryItem ShoesItem
        {
            get
            {
                return this._shoesItem;
            }
            set
            {
                this._shoesItem = value;
            }
        }
        /// <summary>项链 </summary>
        public InventoryItem NecklaceItem
        {
            get
            {
                return this._necklaceItem;
            }
            set
            {
                this._necklaceItem = value;
            }
        }
        /// <summary>手镯 </summary>
        public InventoryItem BraceletItem
        {
            get
            {
                return this._braceletItem;
            }
            set
            {
                this._braceletItem = value;
            }
        }
        /// <summary>指环</summary>
        public InventoryItem RingItem
        {
            get
            {
                return this._ringItem;
            }
            set
            {
                this._ringItem = value;
            }
        }
        /// <summary>翅膀</summary>
        public InventoryItem WingItem
        {
            get
            {
                return this._wingItem;
            }
            set
            {
                this._wingItem = value;
            }
        }
    }
}
