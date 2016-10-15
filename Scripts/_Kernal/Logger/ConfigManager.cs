 /*
* Title: LOL项目开发
 *          接口：配置管理器
 * Descripts: 
 *          作用：读取系统核心XML配置信息
 *  Author: Renfei
 *  Date:  2016
 * Version: 0.1
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.IO;
namespace Kernal
{
    public class ConfigManager : IConfigManager
    {
        private Dictionary<string, string> _AppSetting;
        /// <summary>
        /// 配置管理器构造函数
        /// </summary>
        /// <param name="logPath">日志路径</param>
        /// <param name="xmlRootNodeName">XML根节点名称</param>
        public ConfigManager(string logPath, string xmlRootNodeName)
        {
            _AppSetting = new Dictionary<string, string>();
            //_AppSetting.Add("LogPath", "e:\\LoL_Log.txt");
            //_AppSetting.Add("LogState", "Develop");
            //_AppSetting.Add("LogMaxCapacity", "2000");
            //_AppSetting.Add("LogBufferNumber", "1");
            //初始化解析XML数据，到集合中(logPath, xmlRootNodeName)方法的输入参数
            AnalysisXML(logPath, xmlRootNodeName);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="xmlRootNodeName"></param>
        private void AnalysisXML(string logPath, string xmlRootNodeName) 
        {
            if (string.IsNullOrEmpty(logPath) || string.IsNullOrEmpty(xmlRootNodeName))
            {
                return;
            }
            XDocument xmlDoc;
            XmlReader xmlReader;
            //Debug.Log(logPath);
            //Debug.Log(xmlRootNodeName);
            try
            {
               xmlDoc=XDocument.Load(logPath);               //  加载路径获取树document
               xmlReader=XmlReader.Create(new StringReader(xmlDoc.ToString()));  //得到解析器
            }
            catch(System.Exception)
            {
                //GetType()代表类的全路径(只要报错就知道那个命名空间那个类那一行除了错)
                throw new XMLAnalysisException(GetType() + "/AnalysisXML()/XML Analysis Exception!,please Check!");
            }
            while(xmlReader.Read())
            {
                
                if (xmlReader.IsStartElement() && xmlReader.LocalName == xmlRootNodeName)
                {
                    using (XmlReader xmlReaderItem = xmlReader.ReadSubtree())
                    {
                        while (xmlReaderItem.Read())
                        {
                            if (xmlReaderItem.NodeType == XmlNodeType.Element)
                            {
                                string strNode = xmlReaderItem.Name;
                                xmlReaderItem.Read();
                                if (xmlReaderItem.NodeType == XmlNodeType.Text)
                                {
                                    _AppSetting.Add(strNode, xmlReaderItem.Value);
                                  
                                }
                            }
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 返回配置属性
        /// </summary>
        public System.Collections.Generic.Dictionary<string, string> AppSetting
        {
            get { return _AppSetting; }         
        }
        /// <summary>
        /// 返回配置属性数量
        /// </summary>
        /// <returns></returns>
        public int GetAppSettingMaxNumber()
        {
            if (_AppSetting != null && _AppSetting.Count > 0)
            {
                return _AppSetting.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}