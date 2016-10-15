/***
 * 核心层："XML对话系统数据解析管理器"脚本
 * 功能：对话XML做数据解析
 * 
 * 
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.Xml;
using System.IO;

namespace Kernal
{
    public class XMLDialogsDataAnalysisMgr : MonoBehaviour
    {
        private static XMLDialogsDataAnalysisMgr _Instance;                         //本类实例
        private List<DialogDataFormat> _LiDiaLogDataArray;                          //对话数据集合(XML解析出来程序都在这个集合里)
        private string _StrXMLPath;                                                //Xml路径
        private string _StrXMLRootNodeName;                                         //Xml根节点名称
        //系统常量定义
        private const float TIME_DELAY = 0.1F;                                      //延迟时间   
        private const string XML_ATTRIBUTE_1 = "DialogSecNum";                      //XML文件属性字符串1-6
        private const string XML_ATTRIBUTE_2 = "DialogSecName";                     
        private const string XML_ATTRIBUTE_3 = "SectionIndex";                      
        private const string XML_ATTRIBUTE_4 = "DialogSide";                        
        private const string XML_ATTRIBUTE_5 = "DialogPerson";                     
        private const string XML_ATTRIBUTE_6 = "DialogContent";                     
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private XMLDialogsDataAnalysisMgr()
        {
            _LiDiaLogDataArray = new List<DialogDataFormat>();
        }

        /// <summary>
        /// 得到本类实例
        /// </summary>
        /// <returns></returns>
        public static XMLDialogsDataAnalysisMgr GerInstance()
        { 
            //
            if (_Instance==null)
            {
                //_Instance = new XMLDialogsDataAnalysisMgr();
                //创建一个游戏对象把这个脚本挂在游戏对象上（因为我们这是个脚本所以必须挂在UNITY里面使用）自己脚本自己挂在自己创建的脚本上
                _Instance = new GameObject("_XMLDialogsDataAnalysisMgr").AddComponent<XMLDialogsDataAnalysisMgr>();
            }
            return _Instance;
        }

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        /// <summary>
        /// 设置XML路径与根节点名称
        /// </summary>
        /// <param name="xmlPath">xml的路径</param>
        /// <param name="xmlRootNodeName">xml根节点的名称</param>
        public void SetXMLPathAndRootNodeName(string xmlPath,string xmlRootNodeName)
        {
        //赋值之前做一个参数检查(要求这两个参数不能为空)
            if (string.IsNullOrEmpty(xmlPath) ==false&&string.IsNullOrEmpty(xmlRootNodeName) ==false)
            {
                Debug.Log("123123");
                //XML赋值参数值附加
                _StrXMLPath = xmlPath;
                _StrXMLRootNodeName = xmlRootNodeName;
                //路径设置正确后
                if (PathCheack())
                {                  
                    StartCoroutine(ReadXMLConfigByWWW());
                }
            }
        }

        /// <summary>
        /// 得到本脚本数据集合
        /// </summary>
        /// <returns></returns>
        public List<DialogDataFormat> GetAllxmlDataArray()              //得到所有XML数据的集合
        { 
        //如果前面我们写的内容不等于空并且集合里面必须有一条数据就返回我们的集合
            if (_LiDiaLogDataArray!=null && _LiDiaLogDataArray.Count>=1)
            {
                return _LiDiaLogDataArray;
            }
            else
            {
                //得不到数据返回空
                return null;
            }
        }

        bool PathCheack()
        {         
            //我们要保证XML路径不能为空，根节点名称不能为空(才能允许携程启动)
            if (string.IsNullOrEmpty(_StrXMLPath)==false && string.IsNullOrEmpty(_StrXMLRootNodeName)==false)
            {
               
                return true;
            }
            else
            {
                Debug.LogError(GetType() + "/Start()/_StrXMLPath or _StrXMLRootNodeName is null!,Plsase check");
                return false;
            }
            
        }//Start_End[Start 读取ReadXMLConfigByWWW 读取InitXMLConfig]

        //使用读取XML配置文件
        IEnumerator ReadXMLConfigByWWW()
        {
            WWW www = new WWW(_StrXMLPath);
            //数据是否下载完毕
            while (!www.isDone)
            {
                yield return www;
                //初始化XML配置
                InitXMLConfig(www,_StrXMLRootNodeName);
            }
        }//ReadXMLConfigByWWW_End

        /// <summary>
        /// 初始化XML配置（必须要有参数）
        /// </summary>
        /// <param name="www"></param>
        /// <param name="rootNodeName"></param>
        private void InitXMLConfig(WWW www, string rootNodeName)
        { 
        //参数检查
            if (_LiDiaLogDataArray == null || string.IsNullOrEmpty(www.text))
            {
                Debug.LogError(GetType() + "/InitXMLConfig()/_LiDiaLogDataArray == null or rootNodeName is null!,Plsase check");
                return;
            }
            Debug.Log("123");
        //XML解析程序
            XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.LoadXml(www.text);  //发现这种方式，发布到ANDROID手机端，不能正确的输出中文

            //先用StringReader读取www.text
            /*以下四行代码代替上面注释掉的内容，解决正在发布手机端解析输出中文问题*/
            System.IO.StringReader stringReader = new System.IO.StringReader(www.text);
            stringReader.Read();
            System.Xml.XmlReader reader = System.Xml.XmlReader.Create(stringReader);
            xmlDoc.LoadXml(stringReader.ReadToEnd());

            //读取"<Dialogs_CN>"单个节点的名称每个节点都读出来
            XmlNodeList nodes = xmlDoc.SelectSingleNode(_StrXMLRootNodeName).ChildNodes; ;
            //循环读取XML属性"xe"读取每个属性
            foreach (XmlElement xe in nodes)
            {
                //实例化“XML解析实例类”
                DialogDataFormat data = new DialogDataFormat();
                //段落编号需要转换字符串到整形
                data.DialogSecNum = Convert.ToInt32(xe.GetAttribute("DialogSecNum"));
                //段落名称
                data.DialogSecName = xe.GetAttribute("DialogSecName");
                //段落内符号
                data.SectionIndex = Convert.ToInt32(xe.GetAttribute("SectionIndex"));
                //对话双方
                data.DialogSide = xe.GetAttribute("DialogSide");
                //对话人名
                data.DialogPerson = xe.GetAttribute("DialogPerson");
                //对话内容
                data.DialogContent = xe.GetAttribute("DialogContent");    
                //加入集合
                _LiDiaLogDataArray.Add(data);
            }//foreach_End
        }//InitXMLConfig_End
    }//Class_End
}