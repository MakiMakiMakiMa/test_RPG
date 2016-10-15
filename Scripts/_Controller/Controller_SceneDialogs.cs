using UnityEngine;
using System.Collections;
using Kernal;
using System.Collections.Generic;
using View;
namespace Controller
{
    public class Controller_SceneDialogs : ControllerBase
    {
        private static Controller_SceneDialogs _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_SceneDialogs Instance
        {
            get { return Controller_SceneDialogs._instance; }
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
        }

        /// <summary>
        /// 当点击了底部按钮集合按钮
        /// </summary>
        /// <param name="click"></param>
        /// <param name="isShowButtomBar"></param>
        /// <returns></returns>
        public object[] OnContinueButtonClick(DialogType type,int num)
        {
            return DialogUIMgr.Instance.DiaplayNextDiaLog(type, num);
        }
        /// <summary>
        /// 显示对话框
        /// </summary>
        public void ShowDiaolog()
        {
            this.gameObject.SetActive(!this.gameObject.active);
        }
        #endregion
    }
}