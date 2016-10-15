using UnityEngine;
using System.Collections;
using Controller;
using UnityEngine.UI;
using Global;
namespace View
{
    /// <summary>
    /// 角色选择
    /// </summary>
    public class View_SceneRoleSelect : View_UIBase
    {
        #region 定义成员变量
        private Text mLevelText;
        private Text mNameText;
        private Text mBtn_CreateText;
        private GameObject[] _PlayerOwenArray=new GameObject[6];
        #endregion
        #region UI逻辑实现
        void InitData()
        {
      
            //已有人物面板
            for (int i = 0, max = 6; i < max; i++)
            {
                string str = "PlayerList/Scroll View/Viewport/Btn_Role01010" + (i + 1) + "_Item";
                _PlayerOwenArray[i] = transform.Find(str).gameObject;
                Debug.Log(_PlayerOwenArray[i].name);
            }
            //人物信息面板
            mLevelText = transform.Find("PlayerInfo/Level_Text").GetComponent<Text>();
            mNameText = transform.Find("PlayerInfo/Name_Text ").GetComponent<Text>();
            mBtn_CreateText = transform.Find("Btn_RoleSelect/Text ").GetComponent<Text>();
            mLevelText.text = "";
            mNameText.text = "";
            mBtn_CreateText.text = "创建角色";
        }
        /// <summary>
        /// 回调
        /// </summary>
        /// <param name="type"></param>
        void OnPlayerInfoChanged(PlayerInfoType type)
        {
            switch (type)
            { 
                case PlayerInfoType.Name:
                    this.mNameText.text = Controller_SceneRoleSelected.Instance.GetPlayerName();
                    this.mLevelText.text = "Lv."+"1";
                    this.mBtn_CreateText.text = "已选择";
                    PlayOwenListShow(this.mNameText.text);
                    break;
            }
        }
        /// <summary>
        /// 创建列表显示
        /// </summary>
        /// <param name="name"></param>
        void PlayOwenListShow(string name)
        {
            foreach (GameObject go in _PlayerOwenArray)
            {
                if (go.transform.Find("Text").GetComponent<Text>().text.Equals("创建角色"))
                {
                    go.transform.Find("Level_Text").GetComponent<Text>().text = "Lv.1";
                    go.transform.Find("Name_Text ").GetComponent<Text>().text = name;
                    go.transform.Find("HeadIcon").GetComponent<Image>().enabled = true;
                    if (Controller_SceneRoleSelected.Instance.GetPlayerSex())
                    {
                        Debug.Log(Controller_SceneRoleSelected.Instance.GetPlayerSex());
                        go.transform.Find("HeadIcon").GetComponent<Image>().overrideSprite.name= "data.dat_000184";
                      
                    }
                    else
                    {
                        go.transform.Find("HeadIcon").GetComponent<Image>().overrideSprite.name = "SwordHero2";
                    }
                    go.transform.Find("Text").GetComponent<Text>().text = "";
                    break;
                }
            }
        }
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
            if (click.gameObject.name.Equals("Btn_RoleSelect"))
            {
                Debug.Log("点击了创建角色");
                if (click.gameObject.transform.GetChild(0).GetComponent<Text>().text.Equals("创建角色"))
                {
                    Debug.Log("点击了创建角色");
                    //创建角色
                    Controller_SceneRoleSelected.Instance.OnPlayerListItemClick(true);
                }
                else
                {
                    Debug.Log("已经选了角色");
                    return;
                }
            }
            //点击到了已有角色列表
            if (click.gameObject.name.EndsWith("Item"))
            {
                Debug.Log("点击了开始游戏");
                if (click.gameObject.transform.GetChild(3).GetComponent<Text>().text.Equals("创建角色"))
                {
                    Controller_SceneRoleSelected.Instance.OnPlayerListItemClick(true);
                }
                else
                {
                    Controller_SceneRoleSelected.Instance.OnPlayerListItemClick(false);
                }
            }
            if (click.gameObject.name.Equals("Btn_StartGame"))
            {
                //加载游戏主城
                Debug.Log("点击了开始游戏");
                Controller_SceneRoleSelected.Instance.OnStartGameClick();
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
        protected override void Start()
        {
            base.Start();
            Controller_SceneRoleSelected.Instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;//注册时间
        }
        #endregion
    }
}
