using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Model;
using Kernal;

namespace View
{
    /// <summary>
    /// 物品信息弹框
    /// </summary>
    public class InventoryInfoPopUpUI : View_UIBase
    {
        private Image mInventoryIcon;
        private Text mLevelText;    
        private Text mNameText;
        private Text mHPText;
        private Text mMPText;
        private Text mATKText;
        private Text mDEFText;
        private Text mDEXText;
        private Text mCTXText;      //暴击率
        private Text mCTKText;      //暴伤率
        private Text mDesText;

        private Model.InventoryItem currentInventory;   //当前显示的物品信息
        private InventoryItemUI currentInventoryUI;      //当前提供物品信息的格子(药对她进行清空行为)

        //查找当前元素




        //实现父类
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            mInventoryIcon = transform.Find("InventoryIcon/Image").GetComponent<Image>();
            mLevelText = transform.Find("Level").GetComponent<Text>();
            mNameText = transform.Find("Name").GetComponent<Text>();
            mHPText = transform.Find("ApplyValue/HP/Text").GetComponent<Text>();
            mMPText = transform.Find("ApplyValue/MP/Text").GetComponent<Text>();
            mATKText = transform.Find("ApplyValue/ATK/Text").GetComponent<Text>();
            mDEFText = transform.Find("ApplyValue/DEF/Text").GetComponent<Text>();
            mDEXText = transform.Find("ApplyValue/DEX/Text").GetComponent<Text>();
            mCTXText = transform.Find("ApplyValue/CTR/Text").GetComponent<Text>();
            mCTKText = transform.Find("ApplyValue/CTK/Text").GetComponent<Text>();
            mDesText = transform.Find("Des").GetComponent<Text>();
        }

             /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_Close"))
            {
                this.gameObject.SetActive(!this.gameObject.active);
            }
            if (click.gameObject.name.Equals("Btn_Dress"))
            {
                //控制层调用玩家穿上装备
                object[] obj = new object[2];
                obj[0] = currentInventory;
                obj[1] = currentInventoryUI;
                transform.parent.SendMessage("OnInventoryDressUpClick", obj);
                this.gameObject.SetActive(false);

                //控制层调用玩家吃药或者打开宝箱
            }
        }
        /// <summary>
        /// 设置当前item
        /// </summary>
        /// <param name="item"></param>
        public void SetCurrentInventoryItem(InventoryItem item, InventoryItemUI itemUI)
        {
            if (item == null) return;
            this.currentInventory = item;
            this.currentInventoryUI = itemUI;
            DisPlayInventoryInfo(currentInventory);
        }
        /// <summary>
        /// 显示物品信息
        /// </summary>
        void DisPlayInventoryInfo(InventoryItem item)
        {

            this.gameObject.SetActive(true);

            mInventoryIcon.sprite = TextureAltlers.Instance.textureArray[int.Parse(item.Inventory.ICON)]; ;   //物品图标名称
            mNameText.text = item.Inventory.Name;               //物品名称
            mLevelText.text ="Lv."+ item.Inventory.Level.ToString();               //物品名称
            mHPText.text = "+" + item.Inventory.HP.ToString();               //HP
            mMPText.text = "+" +item.Inventory.MP.ToString();               //MP
            mATKText.text ="+" + item.Inventory.ATK.ToString();               //ATK
            mDEFText.text ="+" + item.Inventory.DEF.ToString();               //DEF
            mDEXText.text ="+" + item.Inventory.DEX.ToString();               //DEX
            mCTXText.text ="+" + item.Inventory.CTX.ToString()+"%";               //CTX
            mCTKText.text = "+" + item.Inventory.CTK.ToString() + "%";               //CTK
            mDesText.text = item.Inventory.Des;               //Des

        }
        #region 父类逻辑实现
        protected override void OnClick(GameObject click)
        {
            base.OnClick(click);
            OnButtonClick(click);
        }
        protected override void Init()
        {
            base.Init();
            InitData();
        }
        protected override void OnDisable()
        {
            base.OnDisable();

        }
        protected override void Start()
        {
            base.Start();

        }
        #endregion
   
      
    }
}
