using UnityEngine;
using System.Collections;

namespace Kernal
{
    /// <summary>
    /// 对话类型
    /// </summary>
    public enum DialogType 
    { 
        None,
        DoubleTalk,     //双人对话
        SingTalk        //单人对话
    }
    public class DialogUIMgr : MonoBehaviour
    {
        private static DialogUIMgr _instance;

        public static DialogUIMgr Instance
        {
            get 
            {
                if (_instance == null)
                { 
                    _instance=new GameObject("DialogUIMgr").AddComponent<DialogUIMgr>();
                }
                return DialogUIMgr._instance; 
            }
        }
        #region 定义成员变量

        #endregion
        void Awake()
        {
            _instance = this;
        }

        /// <summary>
        /// 显示下条信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dialogNum"></param>
        /// <returns></returns>
        public object[] DiaplayNextDiaLog(DialogType type,int dialogNum)
        {
            bool isDialogEnd = false;   //对话开始
            DialogTalkType dType = DialogTalkType.None;
            string strName;
            string dialogContent;
            bool isLastDialog=false;
            //切换说话方
            ChangeDialogType(type);
            //得到会话信息
            bool flag = DialogDataMgr.GetInstance().GetNextDialogInfoRecoder(dialogNum,out dType,out strName,out dialogContent,out isLastDialog);
            if (flag)
            {
                //返回对话信息
                return DisplayDialogInfo(type,dType,strName,dialogContent,isLastDialog);
            }
            else
            {
                isDialogEnd = true;
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dType"></param>
        /// <param name="strName"></param>
        /// <param name="dialogContent"></param>
        private object[] DisplayDialogInfo(DialogType type, DialogTalkType dType, string strName, string dialogContent,bool isLastDialog)
        {
            object[] dialogInfo = new object[3];
            dialogInfo[2] = isLastDialog;
            switch (type)
            {
                case DialogType.None:
                    break;
                case DialogType.DoubleTalk:
                    if(!string.IsNullOrEmpty(strName)&&!string.IsNullOrEmpty(dialogContent))
                    {
                        if (dType == DialogTalkType.Player)
                        {
                            //显示玩家文本
                            dialogInfo[0] = "Player";
                        }
                        else
                        { 
                            //显示其他人名称
                            dialogInfo[0] = "NPC";
                        }
                    }
                    //双人对话内容---
                    dialogInfo[1] = dialogContent;
                    //确定显示精灵----暂时不考虑
                    switch (dType)
                    {
                        case DialogTalkType.None:
                            break;
                        case DialogTalkType.Player:
                            //彩色显示
                            //黑白显示
                            break;
                        case DialogTalkType.Guid:
                            
                            break;
                        case DialogTalkType.NPC:
                            //显示NPC部分
                            break;
                        default:
                            break;
                    }
                    break;
                case DialogType.SingTalk:
                    break;
                default:
                    break;
            }
            Debug.Log(dialogInfo[0]);
            Debug.Log(dialogInfo[1]);
           // Debug.Log(dialogInfo[2]);
            return dialogInfo;
        }



        /// <summary>
        /// 切换对话
        /// </summary>
        /// <param name="type"></param>
        private void ChangeDialogType(DialogType type)
        {
            switch (type)
            {
                case DialogType.None:
                    break;
                case DialogType.DoubleTalk:
                    break;
                case DialogType.SingTalk:
                    break;
                default:
                    break;
            }
        }


    }
}
