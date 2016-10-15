using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Model;
using Kernal;
using Global;

namespace View
{
    public class EquipInfoPopUpUI : View_UIBase
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

        private Model.InventoryItem currentInventory;
        private EquipItemUI currentEquipItemUI;

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
            if (click.gameObject.name.Equals("Btn_TakeOff"))
            {
                object[] obj = new object[2];
                obj[0] = currentInventory;

                //控制层调用玩家穿上装备
                transform.parent.SendMessage("OnEquipItemTakeOffClick",obj);
                //当前格子清空
                currentEquipItemUI.Clear();
                //玩家属性变化
                PlayerInfo.Instance.EquipDressOff(currentInventory);
                //关闭弹框
                this.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 设置当前item
        /// </summary>
        /// <param name="item"></param>
        public void SetCurrentInventoryItem(InventoryItem item, EquipItemUI equipItemUI)
        {
            if (item == null) return;
            this.currentInventory = item;
            currentEquipItemUI = equipItemUI;
            DisPlayEquipInfo(currentInventory);
        }
        /// <summary>
        /// 显示物品信息
        /// </summary>
        void DisPlayEquipInfo(InventoryItem item)
        {
            this.gameObject.SetActive(true);
            mInventoryIcon.sprite = TextureAltlers.Instance.textureArray[int.Parse(item.Inventory.ICON)];
            mNameText.text = item.Inventory.Name;               //物品名称
            mLevelText.text = "Lv." + item.Inventory.Level.ToString();               //物品名称
            mHPText.text = "+" + item.Inventory.HP.ToString();               //HP
            mMPText.text = "+" + item.Inventory.MP.ToString();               //MP
            mATKText.text = "+" + item.Inventory.ATK.ToString();               //ATK
            mDEFText.text = "+" + item.Inventory.DEF.ToString();               //DEF
            mDEXText.text = "+" + item.Inventory.DEX.ToString();               //DEX
            mCTXText.text = "+" + item.Inventory.CTX.ToString() + "%";               //CTX
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
