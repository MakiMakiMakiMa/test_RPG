using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model;
using Global;
public class InventoryManager : MonoBehaviour 
{
    //public static InventoryManager _instance;
    //public TextAsset inventoryListInfo;//Unity里自带的读取文本的类TextAsset
    //public Dictionary<int, Inventory> inventoryDict = new Dictionary<int, Inventory>();//从本地物品清单中取出物品Inventory清单字典,定义一个字典放入物品ID和物品类
    ////public  Dictionary<int, InventoryItem> inventoryItemDict = new Dictionary<int, InventoryItem>();//定义物品等级 数量的字典
    // public List<InventoryItem> inventoryItemList = new List<InventoryItem>();

    // public delegate void OnInventoryChangeEvent();
    // public event OnInventoryChangeEvent OnInventoryChange;

     
    //void Awake()
    //{
    //    _instance = this;
    //    ReadInventoryListInfo();//从本地物品清单中取出物品Inventory清单字典     
    //}
    //void Start()
    //{
    //    //TODO 链接服务器 取得角色当前的物品信息
    //}
    ////两个工具方法
    ///// <summary>
    ///// 从本地物品清单中取出物品Inventory清单字典
    ///// </summary>
    //void ReadInventoryListInfo()//物品信息读取
    //{
       
    //    string allStr=inventoryListInfo.ToString();
    //    string[] itemStrArray = allStr.Split('\n');
    //    //ID 名称 图标 类型（Equip，Drug） 装备类型(Helm,Cloth,Weapon,Shoes,Necklace,Bracelet,Ring,Wing)
    //    //售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
    //    foreach (string itemStr in itemStrArray)
    //    {
    //        string[] proArray = itemStr.Split('|');
    //        Inventory inventory =new Inventory();
    //        inventory.ID = int.Parse(proArray[0]);
    //        inventory.Name = proArray[1];
    //        inventory.ICON = proArray[2];
    //        switch (proArray[3])
    //        { 
    //            case "Equip":
    //                inventory.InventoryTYPE = InventoryType.Equip;
    //                break;
    //            case "Drug":
    //                inventory.InventoryTYPE = InventoryType.Drug;
    //                break;
    //            case "Box":
    //                inventory.InventoryTYPE = InventoryType.Box;
    //                break;
    //        }
    //        //装备类型(Helm,Cloth,Weapon,Shoes,Necklace,Bracelet,Ring,Wing)
    //        if (inventory.InventoryTYPE == InventoryType.Equip)
    //        {
    //            switch (proArray[4])
    //            { 
    //                case "Helm":
    //                    inventory.EquipTYPE=EquipType.Helm;
    //                    break;
    //                case "Cloth":
    //                    inventory.EquipTYPE=EquipType.Cloth;
    //                    break;
    //                case "Weapon":
    //                    inventory.EquipTYPE=EquipType.Weapon;
    //                    break;
    //                case "Shoes":
    //                    inventory.EquipTYPE=EquipType.Shoes;
    //                    break;
    //                case "Necklace":
    //                    inventory .EquipTYPE=EquipType.Necklace;
    //                    break;
    //                case "Ring":
    //                    inventory.EquipTYPE=EquipType.Ring;
    //                    break;
    //                case "Wing":
    //                    inventory.EquipTYPE=EquipType.Wing;
    //                    break;
    //                case "Bracelet":
    //                    inventory.EquipTYPE =EquipType.Bracelet;
    //                    break;
    //            }
    //        }
    //        //售价 星级 品质 伤害 生命 战斗力 作用类型 作用值 描述
    //         inventory.PriceSale =int.Parse(proArray[5]);
    //        if (inventory.InventoryTYPE == InventoryType.Equip)
    //        {
    //            inventory.StarLevle = int.Parse(proArray[6]);
    //            inventory.QuaLity = int.Parse(proArray[7]);
    //            inventory.Damage = int.Parse(proArray[8]);
    //            inventory.HP = int.Parse(proArray[9]);
    //            inventory.Power = int.Parse(proArray[10]);
    //        }
    //        if (inventory.InventoryTYPE == InventoryType.Drug)
    //        {
    //            inventory.ApplyValue = int.Parse(proArray[12]);
    //        }
    //        inventory.Des = proArray[13];
    //        inventoryDict.Add(inventory.ID, inventory);
    //    }
    //}
    ///// <summary>
    ///// 获取服务器物品列表成功时
    ///// </summary>
    ///// <param name="itList"></param> 服务器获取的物品列表
    //void OnGetInventoryItemList(List<InventoryItemDB> itDBList)
    //{
    //    //TODO
    //    //从服务器物品列表信息读取出当前客户端的物品列表
    //    //如果为空列表
    //    if(itDBList==null)
    //    {
    //        inventoryItemList = null;
    //        OnInventoryChange();
    //        return;
    //    }
    //    inventoryItemList.Clear();//先把列表清空,再接受服务器数据
    //    //如果不是空列表
    //    foreach(InventoryItemDB itDB in itDBList)
    //    {
    //       //装备数量永远为1,在向服务器添加的时候必须考虑
    //        InventoryItem item = null;
    //        item=CaseToInventoryItem(itDB);
    //        inventoryItemList.Add(item);         
    //    }
    //    //物品列表改变事件
    //    OnInventoryChange();
    //}    
    ///// <summary>
    ///// 当服务器添加物品成功时,同步更新
    ///// </summary>
    //public void OnAddInventoryItem(InventoryItemDB itemDB)
    //{
    //     //TODO 链接服务器 取得角色当前的物品信息
    //    if (itemDB != null)
    //    {
    //      inventoryItemList.Add(CaseToInventoryItem(itemDB));
    //      OnInventoryChange();
    //    }
    //}
    ///// <summary>
    ///// 当服务器添加物品成功时,同步更新
    ///// </summary>
    //public void OnInventoryItemUpdate()
    //{
    //    //角色当前的物品信息更新成功时
    //    OnInventoryChange();
        
    //}
    ///// <summary>
    ///// 玩家客户端获得物品时,模拟
    ///////如果是物品类型
    ////服务器物品数量加1 发起更新请求
    ////服务器数量大于99的时候不再变化
    ////创建一个新的物品格子,服务器添加新物品
    ////如果是装备类型
    ////服务器添加新物品
    ////发起添加请求
    ///// </summary>
    //void PicUpInventoryItem(InventoryItem itPicup)
    //{
    //    //先判断背包是否存在当前id的物品
    //     //装备类型物品
    //     if (itPicup.Inventory.InventoryTYPE == InventoryType.Equip)
    //     {
    //        inventoryItemDBController.AddInventoryItem(CaseToInventoryItemDB(itPicup));
    //     }
    //     else   //药品,宝箱类
    //     {
    //          bool isExit = false;
    //          foreach (InventoryItem newItem in this.inventoryItemList)
    //          {              
    //             if (newItem.Inventory.ID == itPicup.Inventory.ID)
    //             {
    //                 newItem.Count++;
    //                 itPicup = newItem;
    //                 isExit = true;
    //                 break;
    //             }

    //         }
    //          if (isExit)
    //          {
    //              //向服务器发起物品更新的请求                     
    //              inventoryItemDBController.UpdateInventoryItem(CaseToInventoryItemDB(itPicup));
    //          }
    //          else
    //          {
    //              inventoryItemDBController.AddInventoryItem(CaseToInventoryItemDB(itPicup));
    //          }
    //       }            
    //}
    //void Update()
    //{
    //    //模拟拾取物品
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //            int id =Random.Range(1001,1009);
    //            Inventory i = null;
    //            InventoryItem item = new InventoryItem();
    //            inventoryDict.TryGetValue(id, out i);
    //            item.Inventory =i;
    //            item.Count = 1;
    //            item.Level = 1;
    //            item.IsDressed = false;
    //            PicUpInventoryItem(item);
    //    }
    //}

}
