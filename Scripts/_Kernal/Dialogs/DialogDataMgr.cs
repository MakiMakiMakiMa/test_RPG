using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kernal
{
    public enum DialogTalkType
    { 
        None,
        Player,       //玩家
        Guid,          //引导
        NPC            //NPC
    }
    /// <summary>
    /// 数据类
    /// </summary>
    public class DialogDataMgr 
    {
        private static DialogDataMgr _instance;

        #region 定义成员变量
        private int currentContentNum = 0;
        /// <summary>
        /// 当前段落编号中的内容编号
        /// </summary>
        private int indexByDialogNum = 0;

        private List<DialogDataFormat> cacheList = new List<DialogDataFormat>();//每一段落的内部对话信息
             
        private List<DialogDataFormat> xmlList = new List<DialogDataFormat>();     // 整个xml对话信息集

        #endregion




        public DialogDataMgr()
        {
            xmlList = XMLDialogsDataAnalysisMgr.GerInstance().GetAllxmlDataArray();
        }



        /// <summary>
        /// 获取本类实例
        /// </summary>
        /// <returns></returns>
        public static DialogDataMgr GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DialogDataMgr();
            }
            return _instance;
        }

        /// <summary>
        /// 返回当前数据集中是否已加载了对话数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool LoadAllDialogData(List<DialogDataFormat> list)
        {
            if ((list == null)||list.Count==0)//表示没有加载上对话xml
            {
                return false;
            }
            if (list != null && this.xmlList.Count == 0)//当前管理类对话集没有元素时
            {
                for (int i = 0; i < list.Count; i++)
                {
                    this.xmlList.Add(list[i]);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 获的下一条对话记录
        /// </summary>
        /// <param name="dialogNum">对话编号</param>
        /// <param name="type">对话人</param>
        /// <param name="strName">人名</param>
        /// <param name="dialogContent">对话内容</param>
        /// <returns>对话是否返回了数据</returns>
        public bool GetNextDialogInfoRecoder(int dialogNum,out DialogTalkType type,out string strName,out string dialogContent,out bool isLastDialog)
        {
            type = DialogTalkType.Player;
            strName = "[GetNexDialogInfoRecoder/strName]";
            dialogContent = "[GetNexDialogInfoRecoder/dialogContent]";      //  定位出错的地方
            //输入的段落编号必须默认不为0
            if(dialogNum<=0)
            {
                isLastDialog = false;
                return false;
                
            }
            //设置下一个对话内容
            if(dialogNum!=this.currentContentNum) //换下一段对话
            {
                indexByDialogNum = 0;       //  内部序号值
                cacheList.Clear();          //上一对话段落内容清空
                currentContentNum = dialogNum;      //当前对话内容编号
            }
            if (cacheList != null && cacheList.Count > 0)//     当前对话段落里存在对话
            {
                if (indexByDialogNum < cacheList.Count-1)
                {
                    //当前内容编号增加
                    indexByDialogNum++;
 
                }
                else
                {
                    indexByDialogNum = 0;
                    cacheList.Clear();
                    isLastDialog = true;
                    return true;   //当前序号在段落集合中
                }
            }
            GetDialogInfoReconder(dialogNum, out type, out  strName, out  dialogContent);
            isLastDialog = false;
            return true;
        }
        /// <summary>
        /// 获取对话信息
        /// </summary>
        /// <param name="dialogNum"></param>
        /// <param name="type"></param>
        /// <param name="strName"></param>
        /// <param name="dialogContent"></param>
        /// <returns></returns>
        private void GetDialogInfoReconder(int dialogNum, out DialogTalkType type, out string strName, out string dialogContent)
        {
            type = DialogTalkType.Player;
            string dialoType = "[GetNexDialogInfoRecoder/type]";
            strName = "[GetNexDialogInfoRecoder/strName]";
            dialogContent = "[GetNexDialogInfoRecoder/dialogContent]";      //  定位出错的地方
            //如果缓存段落的内容为空的
            if (cacheList == null ||cacheList.Count <= 0)
            {
                if (xmlList != null && xmlList.Count > 0)//当前缓存不存在的时候，把当前xml中的list加入缓存jihe 
                {
                    for (int i = 0; i < xmlList.Count; i++)
                    {
                        if (xmlList[i].DialogSecNum == dialogNum)
                        {
                            cacheList.Add(xmlList[i]);
                        }
                    }
                }

            }           
            if (cacheList != null && cacheList.Count > 0)
            {
                if (cacheList[indexByDialogNum].DialogSecNum == dialogNum)
                {
                    dialoType = cacheList[indexByDialogNum].DialogSide;
                    if (dialoType.Trim().Equals("Hero"))
                    {
                        type = DialogTalkType.Player;
                    }
                    else if (dialoType.Trim().Equals("NPC"))
                    {
                        type = DialogTalkType.NPC;
                    }
                    strName = cacheList[indexByDialogNum].DialogPerson;
                    dialogContent = cacheList[indexByDialogNum].DialogContent;

                }
            }//end if
        }

        /// <summary>
        /// 把当前段落编号写入当前缓存集合
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool WriteInCacheListByDialogNum(int num)
        { 
            if(num<=0)
            {
                return false;
            }
            if(xmlList!=null&&xmlList.Count>0)
            {
                cacheList.Clear();
                for (int i = 0; i < xmlList.Count; i++)
                {
                    if(xmlList[i].DialogSecNum==num)
                    {
                        cacheList.Add(xmlList[i]);
                    }
                }
                return true;
            }//end if
            return false;
        }
    
    
    
    }
}
