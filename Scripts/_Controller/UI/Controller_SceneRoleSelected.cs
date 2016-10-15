using UnityEngine;
using System.Collections;
using Global;
namespace Controller
{


    /// <summary>
    /// 角色选择控制层
    /// </summary>
    public class Controller_SceneRoleSelected : MonoBehaviour
    {
        private static Controller_SceneRoleSelected _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_SceneRoleSelected Instance
        {
            get { return Controller_SceneRoleSelected._instance; }
            set { _instance = value; }
        }
        #region 定义成员变量
        private string _playerName;
        private bool _sex=false;//false表示男性 其他表示女性
        #endregion
        #region UI逻辑实现
        /// <summary>
        /// 点击玩家列表元素时
        /// </summary>
        /// <param name="isCreateNewRole">是否创建新角色</param>
        internal void OnPlayerListItemClick(bool isCreateNewRole)
        {
            Debug.Log("点击了已有玩家列表元素");
            if (isCreateNewRole)
            {
                if (GameObject.Find("Player_InfoPos").transform.childCount != 0)
                {
                    //如果已经存在子物体 消灭他
                    Destroy(GameObject.Find("Player_InfoPos").transform.GetChild(0).gameObject);
                }
                //场景切换
                Controller_SceneBase.Instance.UIBarDic["PlayerInfoBar"].gameObject.SetActive(false);
                Controller_SceneBase.Instance.UIBarDic["SelectRoleBar"].gameObject.SetActive(true);
                //显示展示角色
                Controller_PlayerShow.Instance.CreatePlayerShow();
            }
            else
            { 
                //切换已创建的角色
            }

        }
        /// <summary>
        /// 点击开始按钮
        /// </summary>
        internal void OnStartGameClick()
        {
            GlobalParametersManager.NextSceneTYPE = SceneType.SceneCity;
            Application.LoadLevel("SceneLoading");
        }
        /// <summary>
        /// 改变面板信息
        /// </summary>
        /// <param name="playerName"></param>
        internal void OnCreatePlayer(string playerName,bool sex)
        {
            _sex = sex;
            _playerName = playerName;
            if (OnPlayerInfoChanged != null)
            {
                OnPlayerInfoChanged(PlayerInfoType.Name);
            }
        }
        /// <summary>
        /// 获取玩家姓名
        /// </summary>
        /// <returns></returns>
        internal string GetPlayerName()
        {
            return _playerName;
        }
        /// <summary>
        /// 获取玩家性别
        /// </summary>
        /// <returns></returns>
        internal bool GetPlayerSex()
        {
            return _sex;
        }
        #endregion
        public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;
        void Awake()
        {
            _instance = this;
        }
        void Start()
        {

        }
        void Update()
        {

        }
    }
}
