using UnityEngine;
using System.Collections;
using Controller;
using Global;
using UnityEngine.UI;
namespace View
{
    public class View_SceneTranscript01 :View_UIBase
    {
        #region 定义成员变量

        private Text mLevelText;
        private Image mHeadIcon;
        private Image HPProgressBar;
        private Image MPProgressBar;
        private Text mHPText;
        private Text mMPText;
        private Text mNameText;
        #endregion
        #region UI逻辑实现
        void InitData()
        {
            PlayerInfo.Instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
            mLevelText = transform.Find("Top_PlayerHeadBar/LevelText").GetComponent<Text>();
            mHeadIcon = transform.Find("Top_PlayerHeadBar/Btn_HeadIcon").GetComponent<Image>();
            HPProgressBar = transform.Find("Top_PlayerHeadBar/HPProgressBar").GetComponent<Image>();
            MPProgressBar = transform.Find("Top_PlayerHeadBar/MPProgressBar").GetComponent<Image>();
            mHPText = transform.Find("Top_PlayerHeadBar/HPText").GetComponent<Text>();
            mMPText = transform.Find("Top_PlayerHeadBar/MPText").GetComponent<Text>();
            mNameText = transform.Find("Top_PlayerHeadBar/NameText").GetComponent<Text>();
            PlayerInfo.Instance.GetPlayerInfo(PlayerInfoType.All);//每次加载的时候问 PlayerInfo要玩家信息
        }
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_Attack_Normal"))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(click, SkillType.Basic, ActionType.Basic,0.01f);
            }
            if (click.gameObject.name.Equals("Btn_Attack_Skill01"))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(click, SkillType.Skill, ActionType.Skill01, 2f);
            }
            if (click.gameObject.name.Equals("Btn_Attack_Skill02"))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(click, SkillType.Skill, ActionType.Skill02, 3f);
            }
            if (click.gameObject.name.Equals("Btn_Attack_Skill03"))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(click, SkillType.Skill, ActionType.Skill03, 4f);
            }
        }
        /// <summary>
        /// 持续按下时
        /// </summary>
        void OnButtonPress(GameObject click)
        {
            if (isPress == false) return;
            StartCoroutine(OnPressAttack(click));
            Debug.Log("持续按下");
        }
        IEnumerator OnPressAttack(GameObject click)
        {
            yield return new WaitForSeconds(0.5f);
            Controller_SceneTranscipt01.Instance.OnAttackButtonClick(click, SkillType.Basic, ActionType.Basic, 0.01f);
            //Debug.Log(Random.Range(0, 9));
            StartCoroutine(OnPressAttack(click));
        }
        ///// <summary>
        ///// 按下
        ///// </summary>
        //void OnButtonDown(GameObject click)
        //{
        //    if (click.gameObject.name.StartsWith("Btn_Attack"))
        //    {
        //        iTween.ScaleTo(click, click.transform.localScale + new Vector3(0.15f, 0.15f, 0.15f), 0.2f);
        //    }
        //    if (click.gameObject.name.Equals("Btn_Attack_Normal"))
        //    {
        //        isPress = true;
        //    }
        //    OnButtonPress(click);
        //}
        ///// <summary>
        ///// 抬起
        ///// </summary>
        //void OnButtonUp(GameObject click)
        //{
        //    if (click.gameObject.name.StartsWith("Btn_Attack"))
        //    {
        //        iTween.ScaleTo(click, click.transform.localScale-new Vector3(0.15f, 0.15f, 0.15f), 0.2f);
        //    }
        //    if (click.gameObject.name.Equals("Btn_Attack_Normal"))
        //    {
        //        isPress = false;
        //    }
        //    StopAllCoroutines();
        //}
        //void OnButtonSelect(GameObject click)
        //{
        //    if (click.gameObject.name.StartsWith("Btn_Attack"))
        //    {
        //        iTween.ScaleTo(click, click.transform.localScale+new Vector3(0.15f, 0.15f, 0.15f), 0.2f);
        //    }
        //}
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="type"></param>
        void OnPlayerInfoChanged(PlayerInfoType type)
        {
            if (type == PlayerInfoType.All)
            {
                mLevelText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Level.ToString();
                HPProgressBar.fillAmount = (float)PlayerInfo.Instance.PlayerPropertyCurrent.HP / PlayerInfo.Instance.PlayerPropertyCurrent.HPMax;
                MPProgressBar.fillAmount = (float)PlayerInfo.Instance.PlayerPropertyCurrent.MP / PlayerInfo.Instance.PlayerPropertyCurrent.MPMax;
                mHPText.text = PlayerInfo.Instance.PlayerPropertyCurrent.HP.ToString() + " /" + PlayerInfo.Instance.PlayerPropertyCurrent.HPMax.ToString();
                mMPText.text = PlayerInfo.Instance.PlayerPropertyCurrent.MP.ToString() + " /" + PlayerInfo.Instance.PlayerPropertyCurrent.MPMax.ToString();
                mNameText.text = PlayerInfo.Instance.PlayerPropertyCurrent.Name;
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
        protected override void OnDown(GameObject click)
        {
            base.OnDown(click);
           // OnButtonDown(click);
        }
        protected override void OnUp(GameObject click)
        {
            base.OnUp(click);
           // OnButtonUp(click);
        }
        protected override void OnPress()
        {
            base.OnPress();
           // OnButtonPress();
            
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
        }
        protected override void OnSelect(GameObject click)
        {
            base.OnSelect(click);
           // OnButtonSelect(click);
        }
        #endregion

    }
}
