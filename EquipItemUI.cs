using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Model;
using Kernal;
namespace View
{
    public class EquipItemUI :View_UIBase
    {
        private Image itemImage;

        private Model.InventoryItem currentInventory;
        /// <summary>
        /// 获取当前的物品保存信息
        /// </summary>
        public Model.InventoryItem CurrentInventory
        {
            get { return currentInventory; }
        }

        private bool isDressed = false;
        /// <summary>
        /// 判断当前是否穿上装备的标志
        /// </summary>
        public bool IsDressed
        {
            get { return isDressed; }
        }

        //查找当前元素
        /// <summary>
        /// 设置当前格子的物品信息
        /// </summary>
        /// <param name="item"></param>
        public void SetCurrentInventory(InventoryItem item)
        {
            if (item == null) return;
            this.currentInventory = item;
            this.currentInventory.IsDressed = true;//表示当前装备的已经穿上的标记
            isDressed = true;
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = TextureAltlers.Instance.textureArray[int.Parse(currentInventory.Inventory.ICON)];
        }
        public void Clear()
        {
            currentInventory = null;
            itemImage.gameObject.SetActive(false);
            this.isDressed = false;
        }

        //实现父类
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            itemImage = transform.Find("Image").GetComponent<Image>();

            //初始化图片隐藏
            itemImage.gameObject.SetActive(!this.gameObject.active);

        }

        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Contains("Btn_Equip"))
            {
                object[] messeage = new object[2];
                messeage[0]=currentInventory;
                messeage[1] = this;
                transform.parent.parent.SendMessage("OnEquipItemClick",messeage);
            }
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
