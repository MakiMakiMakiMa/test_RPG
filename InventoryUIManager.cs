using UnityEngine;
using System.Collections;
using View;
using Model;
using Global;
using System.Collections.Generic;

namespace Kernal
{
    /// <summary>
    /// 处理背包所有UI物品的点击时间
    /// </summary>
    public class InventoryUIManager : MonoBehaviour
    {
        private static InventoryUIManager _instance;
        /// <summary>
        /// 玩家信息单例
        /// </summary>
        public static InventoryUIManager Instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("InventoryUIManager").AddComponent<InventoryUIManager>();
                }
                return InventoryUIManager._instance;
            }
        }
        private InventoryInfoPopUpUI inventoryInfoUI;
        private EquipItemUIManager equipItemUIManager;
        public InventoryItemUIManager itemUIManager;


        public TextAsset inventoryText;
        private List<InventoryItem> inventoryItemList = new List<InventoryItem>();  //本地物品信息列表

        public List<InventoryItem> InventoryItemList
        {
            get { return inventoryItemList; }
        }
        private Dictionary<int, InventoryItem> inventoryItemDic = new Dictionary<int, InventoryItem>();//物品信息字典

        private InventoryItem itemSave;
        private InventoryItemUI itemUISave;

        public InventoryItemUI ItemUISave
        {
            get { return itemUISave; }
            set { itemUISave = value; }
        }
        void Awake()
        {
            _instance = this;
        }
        void Start()
        {
            inventoryInfoUI=transform.Find("InventoryInfoPopUp").GetComponent<InventoryInfoPopUpUI>();
            inventoryInfoUI.gameObject.SetActive(false);

            equipItemUIManager = transform.parent.Find("PlayerInfoPopUpBar").GetComponent<EquipItemUIManager>();
            itemUIManager = transform.Find("Gird").GetComponent<InventoryItemUIManager>();

            ReadLocalInventoryText();
            InventoryItemUIManager.Instance.InitKanpcapUI(inventoryItemList);
        }
        /// <summary>
        /// 读取本地物品信息
        /// </summary>
        public void ReadLocalInventoryText()
        {
            string allStr = inventoryText.ToString();
            string[] itemStrArray = allStr.Split('\n');
            EquipType equipType = EquipType.Weapon;
            InventoryType inventoryType = InventoryType.Equip;
            foreach (string itemStr in itemStrArray)
            {
                string[] proArray = itemStr.Split('|');
                InventoryItem item = new InventoryItem();
                switch (proArray[11])
                {
                    case "equip":
                        inventoryType = InventoryType.Equip;
                        break;
                    default:
                        break;
                }
               // Debug.Log(proArray[15]);
                switch (proArray[12])
                {
                    case "Helm":
                        equipType = EquipType.Helm;
                        Debug.Log(equipType);
                        break;
                    case "Cloth":
                        equipType = EquipType.Cloth;
                        break;
                    case "Necklace":
                        equipType = EquipType.Necklace;
                        break;
                    case "Shoes":
                        equipType = EquipType.Shoes;
                        break;
                    case "Ring":
                        equipType = EquipType.Ring;
                        break;
                    case "Bracelet":
                        equipType = EquipType.Bracelet;
                        break;
                    case "Weapon":
                        equipType = EquipType.Weapon;
                        break;
                    case "Wing":
                        equipType = EquipType.Wing;
                        break;
                    case "JadePendantLeft":
                        equipType = EquipType.JadePendantLeft;
                        break;
                    case "JadePendantRight":
                        equipType = EquipType.JadePendantRight;
                        break;
                    default:
                        break;
                }
                Debug.Log(equipType);
                item.Inventory = new Inventory(int.Parse(proArray[0]), int.Parse(proArray[1]), int.Parse(proArray[2]), int.Parse(proArray[3]),
                    int.Parse(proArray[4]), int.Parse(proArray[5]), int.Parse(proArray[6]), int.Parse(proArray[7]), int.Parse(proArray[8]),
                    proArray[9], proArray[10], inventoryType, int.Parse(proArray[13]), int.Parse(proArray[14]), proArray[15],equipType);

                inventoryItemList.Add(item);
                inventoryItemDic.Add(int.Parse(proArray[8]), item);
            }
        }
        public void OnInventoryClick(object[] obj)
        {
            itemSave = (InventoryItem)obj[0];
            itemUISave = (InventoryItemUI)obj[1];
            //显示物品信息框
            inventoryInfoUI.gameObject.SetActive(!inventoryInfoUI.gameObject.active);
            //显示数据
            inventoryInfoUI.SetCurrentInventoryItem(itemSave, itemUISave);
            this.GetComponent<View_SceneInventoryPopUp>().SetSacleText(itemSave.Inventory.PriceSale);

        }
       /// <summary>
       /// 统一处理点击装备属性的传值
       /// </summary>
       /// <param name="item"></param>
        public void OnInventoryDressUpClick(object[] obj)
        {
            itemSave = (InventoryItem)obj[0];
            itemUISave = (InventoryItemUI)obj[1];
            equipItemUIManager.OnKanapcapInventoryItemClick(itemSave, itemUISave);

        }

    }
}
