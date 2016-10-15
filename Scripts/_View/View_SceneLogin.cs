using UnityEngine;
using System.Collections;
using Controller;
///D:\Unity5.3.5\Unity5.3.5\Unity\Editor\Data\Resources\ScriptTemplates
namespace View
{
    public class View_SceneLogin :View_UIBase
    {
        #region 定义成员变量
        #endregion
        #region UI逻辑实现
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_Start"))
            {
              Controller_SceneLogin.Instance.OnStartButtonClick();
            }
            if (click.gameObject.name.Equals("Btn_Continue"))
            {
              Controller_SceneLogin.Instance.OnContinueButtonClick();
            }
        }

        #endregion
        #region 父类逻辑实现
        protected override void OnClick(GameObject click)
        {
            base.OnClick(click);
            OnButtonClick(click);
        }


        #endregion
    }
}
