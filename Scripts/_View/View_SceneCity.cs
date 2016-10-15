using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Global;
using Controller;
namespace View
{
    public class View_SceneCity : View_UIBase
    {
        #region 定义成员变量
        private Text mLevelText;
        private Text mCoinText;
        private Text mDiamondText;
        private Image mHeadIcon;
        private Image HPProgressBar;
        private Image MPProgressBar;
        private Text mHPText;
        private Text mMPText;
        private Text mNameText;

        private bool isShowButtomBar = true;
        #endregion
        #region UI逻辑实现
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            PlayerInfo.Instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
            mLevelText = transform.Find("Top_PlayerHeadBar/LevelText").GetComponent<Text>();
            mHeadIcon = transform.Find("Top_PlayerHeadBar/Btn_HeadIcon").GetComponent<Image>();
            HPProgressBar = transform.Find("Top_PlayerHeadBar/HPProgressBar").GetComponent<Image>();
            MPProgressBar = transform.Find("Top_PlayerHeadBar/MPProgressBar").GetComponent<Image>();
            mCoinText = transform.Find("Top_CoinBar/CountCoin").GetComponent<Text>();
            mDiamondText = transform.Find("Top_DiamondBar/CountDiamond").GetComponent<Text>();
            mHPText = transform.Find("Top_PlayerHeadBar/HPText").GetComponent<Text>();
            mMPText = transform.Find("Top_PlayerHeadBar/MPText").GetComponent<Text>();
            mNameText = transform.Find("Top_PlayerHeadBar/NameText").GetComponent<Text>();
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="type"></param>
        void OnPlayerInfoChanged(PlayerInfoType type)
        {
            if (type == PlayerInfoType.All || type == PlayerInfoType.Coin)
            {
                mLevelText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Level.ToString();
                HPProgressBar.fillAmount = (float)PlayerInfo.Instance.PlayerPropertyCurrent.HP / PlayerInfo.Instance.PlayerPropertyCurrent.HPMax;
                MPProgressBar.fillAmount = (float)PlayerInfo.Instance.PlayerPropertyCurrent.MP / PlayerInfo.Instance.PlayerPropertyCurrent.MPMax;
                mCoinText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Coin.ToString();
                mDiamondText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Diamond.ToString();
                mHPText.text = PlayerInfo.Instance.PlayerPropertyCurrent.HP.ToString() + " /" + PlayerInfo.Instance.PlayerPropertyCurrent.HPMax.ToString();
                mMPText.text = PlayerInfo.Instance.PlayerPropertyCurrent.MP.ToString() + " /" + PlayerInfo.Instance.PlayerPropertyCurrent.MPMax.ToString();
                mNameText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Name;
            }
        }
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_ButtonBar"))
            {
               isShowButtomBar= Controller_SceneCity.Instance.OnButtomBarButtonClick(click,isShowButtomBar);              
            }
            if (click.gameObject.name.Equals("Btn_System"))
            {
                Controller_SceneCity.Instance.OnSystemButtonClick(); 
            }
            if (click.gameObject.name.Equals("Btn_Knapcap"))
            {
                Controller_SceneCity.Instance.OnKnapcapButtonClick(); 
            }
            if (click.gameObject.name.Equals("Btn_Task"))
            {
                Controller_SceneCity.Instance.OnTaskButtonClick(); 
            }
            if (click.gameObject.name.Equals("Btn_Skill"))
            {
                Controller_SceneCity.Instance.OnSkillButtonClick(); 
            }
            if (click.gameObject.name.Equals("Btn_Equip"))
            {
                Controller_SceneCity.Instance.OnEquipButtonClick(); 
            }
            if (click.gameObject.name.Equals("Btn_Shop"))
            {
                Controller_SceneCity.Instance.OnShopButtonClick(); 
            }
            if (click.gameObject.name.Equals("Btn_HeadIcon"))
            {
                Controller_SceneCity.Instance.OnHeadIconButtonClick();
            }

        }
        #endregion
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
            PlayerInfo.Instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;

        }
        protected override void Start()
        {
            base.Start();
           
        }
        #endregion
    }
}
