using UnityEngine;
using System.Collections;

namespace View
{
    public class View_SceneTaskPopUp : View_UIBase
    {
        void InitData()
        {

        }
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_close"))
            {
                this.gameObject.SetActive(false);
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
