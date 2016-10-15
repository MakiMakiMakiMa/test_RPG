using UnityEngine;
using System.Collections;
using Kernal;
using UnityEngine.UI;
using Global;

namespace View
{
    public class View_SceneInventoryPopUp : View_UIBase
    {
        private Text sacleText;
        private Text knapcapInfoText;
        private InventoryUIManager inventoryUIManager;
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_Clear"))
            {
                 inventoryUIManager.itemUIManager.Clear();
            }
            if (click.gameObject.name.Equals("Btn_Scale"))
            {
                if (sacleText.text!=null||sacleText.text!="")
                //金币增加
                PlayerInfo.Instance.GetCoin(int.Parse(sacleText.text));
                if (inventoryUIManager.ItemUISave!=null)
                {
                    inventoryUIManager.ItemUISave.Clear();
                }
                sacleText.text = "";
                //弹窗要关闭
                transform.Find("InventoryInfoPopUp").gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 显示售价UI
        /// </summary>
        /// <param name="price"></param>
        public void SetSacleText(int price)
        {
            if (price == 0 || price == null)
            {
                sacleText.text = "";
            }
            else
            {
                sacleText.text = price.ToString();
            }
        }
        //实现父类
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            inventoryUIManager = this.gameObject.GetComponent<InventoryUIManager>();
            sacleText = transform.Find("ScalPrice/Text").GetComponent<Text>();
            knapcapInfoText = transform.Find("KnapcapInfoText").GetComponent<Text>();          
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
