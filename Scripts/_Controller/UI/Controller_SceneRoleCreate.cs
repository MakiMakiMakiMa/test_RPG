using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Controller
{
    public class Controller_SceneRoleCreate : MonoBehaviour
    {

        private static Controller_SceneRoleCreate _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_SceneRoleCreate Instance
        {
            get { return Controller_SceneRoleCreate._instance; }
            set { _instance = value; }
        }
        #region 定义成员变量
        #endregion
        void Awake()
        {
            _instance = this;
        }
        #region UI逻辑实现
        /// <summary>
        /// 点击创建玩家按钮
        /// </summary>
        internal bool OnCreateButtonClick(string name)
        {
            if (CheakUserInfoIsNull(name))
            {
                if (CheakUserInfoFormatIsOK(name))
                {
                    Debug.Log("玩家已经创建");
                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// 本地校验：检查密码账号是否为空 无已购买的武器
        /// </summary>
        /// <returns></returns>
        bool CheakUserInfoIsNull(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        /// <summary>
        /// 本地校验：检查密码账号格式是否正确
        /// </summary>
        /// <returns></returns>
        bool CheakUserInfoFormatIsOK(string name)
        {
            if (name.Length < 4)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 点击开始游戏按钮
        /// </summary>
        internal void OnBackButtonClick(bool isCreateFinshed,GameObject player,string playerName)
        {

            if (isCreateFinshed)//点击确认创建
            {
                Debug.Log("角色创建成功");
                //界面切换
                Controller_SceneBase.Instance.UIBarDic["SelectRoleBar"].SetActive(false);
                Controller_SceneBase.Instance.UIBarDic["PlayerInfoBar"].SetActive(true);
                //信息传递
                bool sex = false;
                if (player.name.Contains("Warrior"))
                {
                    sex=false;
                }
                else
                {
                    sex = true;
                }
                Controller_SceneRoleSelected.Instance.OnCreatePlayer(playerName,sex);
                //人物创建成功
                Controller_PlayerShow.Instance.SelectRoleShow(player);
            }
            else 
            {
                Debug.Log("返回人物选择界面");
                //人物未创建 点击返回按钮
                Controller_SceneBase.Instance.UIBarDic["SelectRoleBar"].SetActive(false);
                Controller_SceneBase.Instance.UIBarDic["PlayerInfoBar"].SetActive(true);
                Controller_PlayerShow.Instance.HidePlayerShow();
                return;
            }

        }
        /// <summary>
        /// 选择男孩
        /// </summary>
        internal GameObject OnCreateBoyButtonClick()
        {
            iTween.ScaleTo(Controller_PlayerShow.Instance.GetPlayer(0).transform.parent.gameObject, new Vector3(2.5f, 2.5f, 2.5f), 0.1f);
            iTween.ScaleTo(Controller_PlayerShow.Instance.GetPlayer(1).transform.parent.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.1f);
            return Controller_PlayerShow.Instance.GetPlayer(0);
        }
        /// <summary>
        /// 选择女孩
        /// </summary>
        internal GameObject OnCreateGirlButtonClick()
        {
            iTween.ScaleTo(Controller_PlayerShow.Instance.GetPlayer(0).transform.parent.gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.1f);
            iTween.ScaleTo(Controller_PlayerShow.Instance.GetPlayer(1).transform.parent.gameObject, new Vector3(2.5f, 2.5f, 2.5f), 0.1f);
            return Controller_PlayerShow.Instance.GetPlayer(1);
        }
        #endregion
        void Update()
        {
        }
    }
}
