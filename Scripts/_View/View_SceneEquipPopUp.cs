using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Global;

namespace View
{
    public class View_SceneEquipPopUp:View_UIBase
    {
        #region 定义成员变量
        private Text mNameText;
        private Text mLevelText;
        private Text mExpText;
        private Text mHPText;
        private Text mMPText;
        private Text mAttackText;
        private Text mDefenceText;
        private Text mDexterityText;
        private Text mCriticalTriggerRateText;
        private Text mCriticalAttackMaxText;
        #endregion
        void InitData()
        {
            PlayerInfo.Instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
            mNameText = transform.Find("Info/NameOutLine/NameText").GetComponent<Text>();
            mLevelText = transform.Find("Info/LevelBg/LevelText/Text").GetComponent<Text>();
            mExpText = transform.Find("Info/ExpBg/ExpText/Text").GetComponent<Text>();
            mHPText = transform.Find("Info/HPBg /HpText/Text").GetComponent<Text>();
            mMPText = transform.Find("Info/MPBg/MPText/Text").GetComponent<Text>();
            mAttackText = transform.Find("Info/AttackBg/AttackText/Text").GetComponent<Text>();
            mDefenceText = transform.Find("Info/DefenceBg/DefenceText/Text").GetComponent<Text>();
            mDexterityText = transform.Find("Info/DEXBg/DeXText/Text").GetComponent<Text>();
            mCriticalTriggerRateText = transform.Find("Info/CriticalPercentBg/PercentText/Text").GetComponent<Text>();
            mCriticalAttackMaxText = transform.Find("Info/CriticalDamageBg /PercentText/Text").GetComponent<Text>();
            PlayerInfo.Instance.GetPlayerInfo(PlayerInfoType.All);//每次加载的时候获取下信息
        }
          /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="type"></param>
        void OnPlayerInfoChanged(PlayerInfoType type)
        {
            if (type == PlayerInfoType.All)
            {
                mNameText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Name.ToString();
                mLevelText.text = "Lv."+PlayerInfo.Instance.PlayerPropertyCurrent.Level.ToString();
                mExpText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Exp.ToString() + "/" + PlayerInfo.Instance.PlayerPropertyCurrent.ExpUpGrade.ToString(); ;
                mHPText.text = PlayerInfo.Instance.PlayerPropertyCurrent.HP.ToString() + "/" + PlayerInfo.Instance.PlayerPropertyCurrent.HPMax.ToString();
                mMPText.text = PlayerInfo.Instance.PlayerPropertyCurrent.MP.ToString() + "/" + PlayerInfo.Instance.PlayerPropertyCurrent.MPMax.ToString();
                mAttackText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Attack.ToString() + "/" + PlayerInfo.Instance.PlayerPropertyCurrent.AttackMax.ToString();
                mDefenceText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Defence.ToString() + "/" + PlayerInfo.Instance.PlayerPropertyCurrent.DefenceMax.ToString();
                mDexterityText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Dexterity.ToString() + "/" + PlayerInfo.Instance.PlayerPropertyCurrent.DexterityMax.ToString();
                mCriticalTriggerRateText.text = (PlayerInfo.Instance.PlayerPropertyCurrent.CriticalTriggerRateMax * 100f).ToString() + "%";// +"/" + (PlayerInfo.Instance.PlayerPropertyCurrent.CriticalTriggerRateMax * 100f).ToString() + "%100";
                mCriticalAttackMaxText.text =( PlayerInfo.Instance.PlayerPropertyCurrent.CriticalAttackMax * 100f).ToString() + "%";//+ "/" + PlayerInfo.Instance.PlayerPropertyCurrent.CriticalAttackMax.ToString();
            
            }
        }
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        { }
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

        #endregion
    }
}
