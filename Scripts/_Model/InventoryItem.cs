using UnityEngine;
using System.Collections;
namespace Model
{
    /// <summary>
    /// InventoryItem   这里unity里的物品类一定不能继承 MonoBehaviour 
    /// 否者在遍历数组的时候所有调用这个类的都会为空值
    /// </summary>
    public class InventoryItem
    {

        private Inventory _inventory;
        private int _levle = 1;
        private int _count = 1;
        private bool _isDressed = false;
        /// <summary>
        /// Inventory 这里unity里的物品类一定不能继承 MonoBehaviour 
        /// 否者在遍历数组的时候所有调用这个类的都会为空值
        /// </summary>
        public Inventory Inventory
        {
            get
            {
                return this._inventory;
            }
            set
            {
                this._inventory = value;
            }
        }
        public int Level
        {
            get
            {
                return this._levle;
            }
            set
            {
                this._levle = value;
            }
        }
        public int Count
        {
            get
            {
                return this._count;
            }
            set
            {
                this._count = value;
            }
        }
        public bool IsDressed
        {
            get
            {
                return _isDressed;
            }
            set
            {
                this._isDressed = value;
            }
        }
    }

}

