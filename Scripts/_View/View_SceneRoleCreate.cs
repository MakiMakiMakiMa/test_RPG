using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Controller;

namespace View
{
    public class View_SceneRoleCreate :View_UIBase
    {
        #region 定义成员变量
        private bool isCreateFinished = false;
        private GameObject _player;
        private string _playerName="";
        private InputField _input;
        #endregion
        #region UI逻辑实现
        void InitData()
        {

            _input = transform.Find("Input_RoleName").GetComponent<InputField>();
            _playerName = _input.text;
        }
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_CreateRole"))
            {
                _playerName = _input.text;
                //确定输入的昵称是否正确
                if (Controller_SceneRoleCreate.Instance.OnCreateButtonClick(_playerName))
                {
                    Debug.Log(_playerName);
                    isCreateFinished = true;//创建完成
                    Controller_SceneRoleCreate.Instance.OnBackButtonClick(isCreateFinished, _player,_playerName);
                }
            }
            if (click.gameObject.name.Equals("Btn_Back"))
            {
                Controller_SceneRoleCreate.Instance.OnBackButtonClick(false,new GameObject(),"");
            }
            if (click.gameObject.name.Equals("Btn_CreateBoy"))
            {
               _player = new GameObject();
              _player= Controller_SceneRoleCreate.Instance.OnCreateBoyButtonClick();
              Debug.Log(_player.name);
            }
            if (click.gameObject.name.Equals("Btn_GreateGirl"))
            {
                _player = new GameObject();
               _player= Controller_SceneRoleCreate.Instance.OnCreateGirlButtonClick();
               Debug.Log(_player.name);
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

        #endregion
    
    }
}
