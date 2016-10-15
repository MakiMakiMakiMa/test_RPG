using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model;

namespace View
{
    public class InventoryItemUIManager : MonoBehaviour
    {
        public List<InventoryItemUI> InventoryItemUIList;

        private static InventoryItemUIManager _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static InventoryItemUIManager Instance
        {
            get
            {
                // if (_instance == null)
                //{
                //    _instance = new GameObject("EnemyPropertyManager").AddComponent<EnemyPropertyManager>();
                //}
                return InventoryItemUIManager._instance;
            }
            set { _instance = value; }
        }
        void Awake()
        {
            _instance = this;
        }
        /// <summary>
        /// 初始化背包信息
        /// </summary>
        public void InitKanpcapUI(List<InventoryItem> itmesList)
        {
            for (int i = 0; i < itmesList.Count; i++)
            {
                InventoryItemUIList[i].SetCurrentInventory(itmesList[i]);
            }
            for (int j = itmesList.Count; j < InventoryItemUIList.Count; j++)
            { 
                //设置为空格
               
            }
        }
        /// <summary>
        /// 初始化背包信息
        /// </summary>
        public void SetKnapcapInventoryItemUI(InventoryItem item)
        {
            for (int i = 0; i < InventoryItemUIList.Count; i++)
            {
                if (InventoryItemUIList[i].CurrentInventory == null)
                {
                    InventoryItemUIList[i].SetCurrentInventory(item);
                    break;
                }      
            }
        }

        /// <summary>
        /// 整理装备
        /// </summary>
        public void Clear()
        {
            List<InventoryItem> itemListClear = new List<InventoryItem>();
            //把所有元素放入新集合
            for (int i = 0; i < InventoryItemUIList.Count; i++)
            {           
                if (InventoryItemUIList[i].CurrentInventory != null)
                {
                    itemListClear.Add(InventoryItemUIList[i].CurrentInventory);  
                    //把原来的元素清空
                    InventoryItemUIList[i].Clear();
                }
            }
            //重置原集合
            for (int i = 0; i < itemListClear.Count; i++)
            {
                InitKanpcapUI(itemListClear);
            }
        }

    }
}
