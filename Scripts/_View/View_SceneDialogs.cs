using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Global;
using Controller;
using Kernal;
namespace View
{
    public class View_SceneDialogs : View_UIBase
    {

        #region 定义成员变量
        private Text mDiaLogInfo;
        private Text mDiaLogName;
        private Image mPlayerHeadIcon;
        private Image mNPCHeadIcon;
        private Image mGuaidHeadIcon;
        private int dialogCount = 0;

        public int DialogCount
        {
            get { return dialogCount; }
            set { dialogCount = value; }
        }
        private bool isShowButtomBar = true;
        #endregion
        #region UI逻辑实现
        /// <summary>
        /// 初始化数据
        /// </summary>
        void InitData()
        {
            PlayerInfo.Instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;
            mDiaLogInfo = transform.Find("DialogBg/DiaLogInfo").GetComponent<Text>();
            mDiaLogName = transform.Find("DialogBg/NameInfo").GetComponent<Text>();
            mPlayerHeadIcon = transform.Find("DialogBg/PlayerHeadIcon").GetComponent<Image>();
            mNPCHeadIcon = transform.Find("DialogBg/NPCHeadIcon").GetComponent<Image>();
            mGuaidHeadIcon = transform.Find("DialogBg/GuaidHeadIcon").GetComponent<Image>();

            mNPCHeadIcon.gameObject.SetActive(false);
          //  mGuaidHeadIcon.gameObject.SetActive(false);
            mPlayerHeadIcon.gameObject.SetActive(false);
        }
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="type"></param>
        void OnPlayerInfoChanged(PlayerInfoType type)
        {
            if (type == PlayerInfoType.All)
            {
            }
        }
        /// <summary>
        /// 子类点击事件的实现
        /// </summary>
        /// <param name="click"></param>
        void OnButtonClick(GameObject click)
        {
           if (click.gameObject.name.Equals("Btn_continue"))
            {
                mGuaidHeadIcon.gameObject.SetActive(false);
                DialogContent(dialogCount);
            }

        }

        private void DialogContent(int dialogNum)
        {           
                    object[] dialogInfo = Controller_SceneDialogs.Instance.OnContinueButtonClick(DialogType.DoubleTalk, dialogNum);
                    if ((bool)dialogInfo[2])
                    {
                        mDiaLogInfo.text = "妹子什么的通通通的交出来！";
                        mPlayerHeadIcon.gameObject.SetActive(true);
                        mNPCHeadIcon.gameObject.SetActive(false);
                        mGuaidHeadIcon.gameObject.SetActive(false);
                        this.gameObject.SetActive(false);
                        return;
                    }
                    string name = (string)dialogInfo[0];
                    string content = (string)dialogInfo[1];
                    switch (name)
                    {
                        case "Player":
                            mPlayerHeadIcon.gameObject.SetActive(true);
                            mNPCHeadIcon.gameObject.SetActive(false);
                            mGuaidHeadIcon.gameObject.SetActive(false);
                            mDiaLogName.text = "yingjiu";
                            mDiaLogInfo.text = content;
                            break;
                        case "NPC":
                            switch (dialogNum)
	                        {
                                case 1:
                                     mPlayerHeadIcon.gameObject.SetActive(false);
                                     mNPCHeadIcon.gameObject.SetActive(true);
                                     mDiaLogName.text = "张老汉";                           
                                    break;
                                case 2:
                                    mPlayerHeadIcon.gameObject.SetActive(false);
                                    mGuaidHeadIcon.gameObject.SetActive(true);
                                    mDiaLogName.text = "神仙姐姐";
                                    break;
                                case 3:
                                    mPlayerHeadIcon.gameObject.SetActive(false);
                                    mNPCHeadIcon.gameObject.SetActive(true);
                                    mDiaLogName.text = "张老汉";
                                    break;
                                case 4:
                                    mPlayerHeadIcon.gameObject.SetActive(false);
                                    mNPCHeadIcon.gameObject.SetActive(true);
                                    mDiaLogName.text = "张老汉";
                                    break;
                                case 5:
                                    mPlayerHeadIcon.gameObject.SetActive(false);
                                    mNPCHeadIcon.gameObject.SetActive(true);
                                    mDiaLogName.text = "张老汉";
                                    break;
                                  default:
                                   break;
	                       }
                            break;
                        default:
                            break;
                    }
                    mDiaLogInfo.text = content;
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
        protected override void OnDisable()
        {
            base.OnDisable();
            PlayerInfo.Instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;

        }
        protected override void Start()
        {
            base.Start();

        }
        #endregion
    }
}
