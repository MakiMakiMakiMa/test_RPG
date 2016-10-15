using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Model;
using Kernal;

namespace View
{
    /// <summary>
    /// 背包元素：视图界面
    /// </summary>
    public class InventoryItemUI : View_UIBase
    {
        private Image inventoryImage;
        private Text countText;

        private Model.InventoryItem currentInventory;

        public Model.InventoryItem CurrentInventory
        {
            get { return currentInventory; }
        }

        //查找当前元素

        public void SetCurrentInventory(InventoryItem item)
        {
            currentInventory = item;
            inventoryImage.gameObject.SetActive(true);
            countText.gameObject.SetActive(true);
            inventoryImage.sprite = TextureAltlers.Instance.textureArray[int.Parse(currentInventory.Inventory.ICON)];
            if (currentInventory.Count > 1)
            {
                countText.text = currentInventory.Count.ToString();
            }
            else
            {
                countText.text = "";
            }
           
        }


        public void Clear()
        {
            currentInventory = null;
            inventoryImage.gameObject.SetActive(false);
            countText.gameObject.SetActive(false);
        }
            /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Contains("Btn_Inventory_Item"))
            {
                if (currentInventory==null) return;
                object[] obj = new object[2];
                obj[0] = currentInventory;
                obj[1] = this;
                transform.parent.parent.SendMessage("OnInventoryClick",obj);
            }
        }
        //实现父类
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            currentInventory = null;
            countText = transform.Find("Count").GetComponent<Text>();
            inventoryImage = transform.Find("Inventory").GetComponent<Image>();
            //初始化图片隐藏
            countText.gameObject.SetActive(!this.gameObject.active);
            inventoryImage.gameObject.SetActive(!this.gameObject.active);

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
