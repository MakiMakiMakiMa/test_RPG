using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Kernal;
using Global;

namespace Controller
{
    public class Controller_SceneCity  : ControllerBase
    {
        private static Controller_SceneCity _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_SceneCity Instance
        {
            get { return Controller_SceneCity._instance; }
            set { _instance = value; }
        }
        #region 父类逻辑实现
        protected override void Awake()
        {
            base.Awake();
            _instance = this;
        }
        protected override void Start()
        {
            base.Start();
            AudioManager.Instacne.PlayAudioClip(AudioSourceType._BGAudioSource, "BaseScenes");
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion
        /// <summary>
        /// 当点击了底部按钮集合按钮
        /// </summary>
        /// <param name="click"></param>
        /// <param name="isShowButtomBar"></param>
        /// <returns></returns>
        public bool OnButtomBarButtonClick(GameObject click,bool isShowButtomBar)
        {
            if (isShowButtomBar)
            {
                for (int i = 0; i < click.transform.childCount; i++)
                {
                    click.transform.GetChild(i).gameObject.SetActive(false);
                }
                return false;
            }
            else
            {
                for (int i = 0; i < click.transform.childCount; i++)
                {
                    click.transform.GetChild(i).gameObject.SetActive(true);
                    //click.transform.GetComponentsInChildren<Button>(true)[i].gameObject.SetActive(true);
                }
                return true;
            }
        }
        /// <summary>
        /// 点击系统设置按钮
        /// </summary>
        public void OnSystemButtonClick()
        { 
        }
        public void OnKnapcapButtonClick()
        {
            GameObject go = Controller_SceneBase.Instance.UIBarDic["KnapcapPopUpBar"];
            if (go.active == false)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        /// <summary>
        /// 点击任务按钮
        /// </summary>
        public void OnTaskButtonClick()
        {
            GameObject go = Controller_SceneBase.Instance.UIBarDic["TaskBar"];
            if (go.active == false)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        /// <summary>
        /// 点击技能按钮
        /// </summary>
        public void OnSkillButtonClick()
        {
            GameObject go = Controller_SceneBase.Instance.UIBarDic["SkillBar"];
            if (go.active == false)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        /// <summary>
        /// 点击装备按钮
        /// </summary>
        public void OnEquipButtonClick()
        {
            GameObject go=Controller_SceneBase.Instance.UIBarDic["PlayerInfoPopUpBar"];
            if (go.active == false)
            {
               go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        /// <summary>
        /// 点击商店按钮
        /// </summary>
        public void OnShopButtonClick()
        {
            GameObject go = Controller_SceneBase.Instance.UIBarDic["ShopBar"];
            if (go.active == false)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
        /// <summary>
        ///头像按钮点击后
        /// </summary>
        public void OnHeadIconButtonClick()
        {
            GameObject go1 = Controller_SceneBase.Instance.UIBarDic["PlayerInfoPopUpBar"];
            GameObject go2 = Controller_SceneBase.Instance.UIBarDic["KnapcapPopUpBar"];
            if (go1.active == false || go2.active == false)
            {
                go1.SetActive(true);
                go2.SetActive(true);
            }
            else if (go1.active == true || go2.active == true)
            {
                go1.SetActive(false);
                go2.SetActive(false);
            }

        }
    }
}
