using UnityEngine;
using System.Collections;
using View;
using Model;
using Global;
using Controller;
using System.Collections.Generic;
namespace Kernal
{
    public class EquipItemUIManager : MonoBehaviour
    {

        private EquipItemUI HelmItemUI;                     //头盔
        private EquipItemUI ClothItemUI;                    //胸甲
        private EquipItemUI NecklaceItemUI;                 //项链
        private EquipItemUI ShoesItemUI;                    //鞋子
        private EquipItemUI RingItemUI;                     //手环
        private EquipItemUI BraceleItemUI;                  //手镯
        private EquipItemUI WeaponItemUI;                   //武器
        private EquipItemUI WingItemUI;                     //翅膀
        private EquipItemUI JadePendantLeftItemUI;          //左边玉佩
        private EquipItemUI JadePendantRightItemUI;          //右边玉佩

        private EquipInfoPopUpUI euqipPopouUI;              
        private InventoryUIManager inventoryUIManager;          //两者间相互传值


        private InventoryItem itemSave;
        private InventoryItemUI itemUISave;
        void Start()
        {
           // item = new InventoryItem();
           // item.Inventory = new Inventory(10, 2000, 1500, 400, 200, 30, 20, 40, 1001, "乾坤戒", "48", InventoryType.Equip, 100, 100, "什么鸡巴玩意",EquipType.Weapon);
            GetItems();
        }



        /// <summary>
        /// 获取装备GIrd的各个Item
        /// </summary>
       private void GetItems()
        {
            HelmItemUI = transform.Find("Equip/Btn_Equip_Helm").GetComponent<EquipItemUI>();
            ClothItemUI = transform.Find("Equip/Btn_Equip_Cloth").GetComponent<EquipItemUI>();
            NecklaceItemUI = transform.Find("Equip/Btn_Equip_Necklace").GetComponent<EquipItemUI>();
            ShoesItemUI = transform.Find("Equip/Btn_Equip_Shoes").GetComponent<EquipItemUI>();
            RingItemUI = transform.Find("Equip/Btn_Equip_Ring").GetComponent<EquipItemUI>();
            BraceleItemUI = transform.Find("Equip/Btn_Equip_Bracelet").GetComponent<EquipItemUI>();
            WeaponItemUI = transform.Find("Equip/Btn_Equip_Weapon").GetComponent<EquipItemUI>();
            WingItemUI = transform.Find("Equip/Btn_Equip_Wing").GetComponent<EquipItemUI>();
            JadePendantLeftItemUI = transform.Find("Equip/Btn_Equip_JadePendantLeft").GetComponent<EquipItemUI>();
            JadePendantRightItemUI = transform.Find("Equip/Btn_Equip_JadePendantRight").GetComponent<EquipItemUI>();
            //查找到弹框
            euqipPopouUI = transform.Find("EquipInfoPopUp").GetComponent<EquipInfoPopUpUI>();
            euqipPopouUI.gameObject.SetActive(!euqipPopouUI.gameObject.active);
            inventoryUIManager = transform.parent.Find("KnapcapPopUpBar").GetComponent<InventoryUIManager>();
        }
        /// <summary>
        /// 背包系统点击装备后传过来的值
        /// </summary>
        /// <param name="item"></param>
      public void OnKanapcapInventoryItemClick(InventoryItem item,InventoryItemUI itemUI)
      {
          InventoryItem itemClick =item;
          //需要将当前穿好的装备保存下来 以便于脱下来
          InventoryItem itemDressed = null;
          //需要判定装备类型，执行一些特殊操作
          switch (itemClick.Inventory.EquipTYPE)
          {
              case EquipType.Helm:
                  if (HelmItemUI.IsDressed)
                  {
                      itemDressed = HelmItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  HelmItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.Cloth:
                  if (ClothItemUI.IsDressed)
                  {
                      itemDressed = ClothItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  ClothItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.Weapon:
                  if (WeaponItemUI.IsDressed)
                  {
                      itemDressed = WeaponItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  WeaponItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.Shoes:
                  if (ShoesItemUI.IsDressed )
                  {
                      itemDressed = ShoesItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  ShoesItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.Necklace:
                  if (NecklaceItemUI.IsDressed)
                  {
                      itemDressed = NecklaceItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  NecklaceItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.Bracelet:
                  if (BraceleItemUI.IsDressed)
                  {
                      itemDressed = BraceleItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  BraceleItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.Ring:
                  if (RingItemUI.IsDressed)
                  {
                      itemDressed = RingItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  RingItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.Wing:
                  if (WingItemUI.IsDressed)
                  {
                      itemDressed = WingItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  WingItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.JadePendantLeft:
                  if (JadePendantLeftItemUI.IsDressed)
                  {
                      itemDressed = JadePendantLeftItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  JadePendantLeftItemUI.SetCurrentInventory(itemClick);
                  break;
              case EquipType.JadePendantRight:
                  if (JadePendantRightItemUI.IsDressed )
                  {
                      itemDressed = JadePendantRightItemUI.CurrentInventory;
                      itemDressed.IsDressed = false;//表示当前装备为未穿上的标记
                  }
                  JadePendantRightItemUI.SetCurrentInventory(itemClick);
                  break;
              default:
                  break;
          }
          //玩家装备上装备属性增加
          PlayerInfo.Instance.EquipDressOn(item);
          if (itemDressed == null)//如果为空表示当前没有穿上任何东西
          {
              itemUI.Clear();
          }
          else//当前已经穿上装备，把脱下的装备放到背包里
          {
              itemUI.SetCurrentInventory(itemDressed);
              //玩家脱下上装备属性减少
              PlayerInfo.Instance.EquipDressOn(itemDressed);
          }

         }
 
        /// <summary>
        /// 任务信息面板的装备点击后传过来的值:中间传递作用
        /// </summary>
        /// <param name="messeage"></param>
      public void OnEquipItemClick(object[] messeage)
      {
          InventoryItem itemClick = (InventoryItem)messeage[0];
          EquipItemUI equipItemUIClick = (EquipItemUI)messeage[1];
          //需要判定装备类型，执行一些特殊操作
          euqipPopouUI.SetCurrentInventoryItem(itemClick, equipItemUIClick);
      }
     /// <summary>
     /// 脱下装备
     /// </summary>
     /// <param name="obj"></param>
      public void OnEquipItemTakeOffClick(object[] obj)
      {
          itemSave = (InventoryItem)obj[0];
          inventoryUIManager.itemUIManager.SetKnapcapInventoryItemUI(itemSave);
      }
    }
}