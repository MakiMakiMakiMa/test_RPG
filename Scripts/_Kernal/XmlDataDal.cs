/***
 * Title:《LOL》项目开发
 *          公共层：进行游戏的存盘与调用
 * Description:
 * 实现原理：对于"模型层"数据核心类做"对象持久化"操作处理
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Kernal;
using Model;
using Assets.Scripts._Kernal;

namespace Global
{


    public class XmlDataDal : MonoBehaviour
    {

        //单例模式
        private static XmlDataDal _Instance;
        /*数据持久话路径*/
        //数据持久化路径(沙盒目录运行游戏unity才会创建"StreamingAssets"文件夹只能读不能写)
        //全局参数对象路径
        private static string _FileNameByGlobalParameterData = Application.persistentDataPath + "/GlobalParametersData.xml";
        //玩家核心数据对象路径
        private static string _FileNameByKernalData = Application.persistentDataPath + "/PlayerInfoData.xml";
        //玩家背包数据对象路径
        private static string _FileNameByPackageData = Application.persistentDataPath + "/InventoryData.xml";
        /// <summary>
        /// 得到本脚本实例
        /// </summary>
        /// <returns></returns>
        public static XmlDataDal GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("XmlDataDal").AddComponent<XmlDataDal>();
            }
            return _Instance;
        }


        #region 存储游戏进度
        //把我们对象存到xml里面在读取

        /// <summary>
        /// 存储游戏进度
        /// </summary>
        /// <returns></returns>
        public void SaveGameProccess()
        {
            //存储游戏全局参数"我们大部分存储Model层的数据[Global层玩家全局数据也要存数]"

            //存储游戏全局参数
            StoreTOXML_GlobalParametersData();
            //存储玩家核心数据
            StoreTOXML_PlayerInfoData();
            //存储玩家背包数据
            StoreTOXML_InventoryData();

        }


        //存储游戏全局参数
        private void StoreTOXML_GlobalParametersData()
        {
            string playerName = GlobalParametersManager.CurrentPlayerName;
            SceneType scenesName = GlobalParametersManager.NextSceneTYPE;
            GlobalParametersData GPD = new GlobalParametersData(scenesName, playerName);
            //对象序列化
            string s = XmlOperation.GetInstance().SerializeObject(GPD, typeof(GlobalParametersData));
            //创建XML文件，且写入[保证文件不为空写入]
            if (string.IsNullOrEmpty(_FileNameByGlobalParameterData))
            {
                XmlOperation.GetInstance().CreateXML(_FileNameByGlobalParameterData, s);
            }
            Log.Write(GetType() + "/ StoreTOXML_GlobalParaData()/xml Path =" + _FileNameByGlobalParameterData, LevelType.Special);
        }

        //存储玩家核心数据
        private void StoreTOXML_PlayerInfoData()
        {
            //数据准备
            PlayerProperty playerInfoData= PlayerInfo.Instance.PlayerPropertyCurrent;
            //对象序列化[对这个文件做字符串处理写入xml]
            string s = XmlOperation.GetInstance().SerializeObject(playerInfoData, typeof(PlayerProperty));
            //创建XML文件，且写入[保证文件不为空写入]
            if (string.IsNullOrEmpty(_FileNameByKernalData))
            {
                XmlOperation.GetInstance().CreateXML(_FileNameByKernalData, s);
            }

        }
        //存储玩家背包数据
        private void StoreTOXML_InventoryData()
        {
            //数据准备
            string s = "";
            for (int i = 0; i < InventoryUIManager.Instance.InventoryItemList.Count; i++)
			{
                s += XmlOperation.GetInstance().SerializeObject(InventoryUIManager.Instance.InventoryItemList[i], typeof(InventoryItem))+"|";
			}
            s.Substring(0, s.Length - 1);
            //对象序列化       
            //创建XML文件，且写入[保证文件不为空写入]
            if (string.IsNullOrEmpty(_FileNameByPackageData))
            {
                XmlOperation.GetInstance().CreateXML(_FileNameByPackageData, s);
            }
        }
        //存储玩家背包数据
        //private void StoreTOXML_EquipData()
        //{
        //    //数据准备
        //    string s = "";
        //    for (int i = 0; i < InventoryUIManager.Instance.InventoryItemList.Count; i++)
        //    {
        //        s += XmlOperation.GetInstance().SerializeObject(InventoryUIManager.Instance.InventoryItemList[i], typeof(InventoryItem)) + "|";
        //    }
        //    s.Substring(0, s.Length - 1);
        //    //对象序列化       
        //    //创建XML文件，且写入[保证文件不为空写入]
        //    if (string.IsNullOrEmpty(_FileNameByPackageData))
        //    {
        //        XmlOperation.GetInstance().CreateXML(_FileNameByPackageData, s);
        //    }
        //}

        #endregion



        #region 提取游戏进度

        /// <summary>
        /// 提取全局游戏参数数据
        /// </summary>
        /// <returns></returns>
        public bool LoadingGame_GlobalParametersData()
        {
            //读取游戏的全局参数
            ReadFromXML_GlobalParaData();
            return true;
        }

        /// <summary>
        /// 提取游戏玩家数据
        /// </summary>
        /// <returns></returns>
        public bool LoadingGame_PlayerData()
        {
            //读玩家核心数据
            ReadFromXML_PlayerInfoData();
            //读玩家背包数据
            ReadFromXML_InventoryData();

            return true;
        }

        /// <summary>
        /// 读取游戏的全局参数
        /// </summary>
        private void ReadFromXML_GlobalParaData()
        {
            GlobalParametersData dpd = null;

            //参数检查
            if (string.IsNullOrEmpty(_FileNameByGlobalParameterData))
            {
                Debug.LogError(GetType() + "/ReadFromXML_GlobalParaData()/_FileNameByGlobalParameterData is Null!!!");
                return;
            }

            try
            {
                //读取XML数据
                string strTemp = XmlOperation.GetInstance().LoadXML(_FileNameByGlobalParameterData);
                //反序列化
                dpd = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(GlobalParametersData)) as GlobalParametersData;
                //赋值
              GlobalParametersManager.CurrentPlayerName= dpd.PlayerName;
              GlobalParametersManager.NextSceneTYPE= dpd.NextScenesName;
             // GlobalParametersManager.= CurrentGameType.Continue;
            }
            catch
            {
                Debug.LogError(GetType() + "/ReadFromXML_GlobalParaData()/读取游戏的全局参数,不成功，请检查");
            }
        }

        /// <summary>
        /// 读取玩家核心数据
        /// </summary>
        private PlayerProperty ReadFromXML_PlayerInfoData()
        {
            PlayerProperty pkd = null;

            //参数检查
            if (string.IsNullOrEmpty(_FileNameByKernalData))
            {
                Debug.LogError(GetType() + "/ReadFromXML_GlobalParaData()/_FileNameByKernalData is Null!!!");
                return null;
            }

            try
            {
                //读取XML数据
                string strTemp = XmlOperation.GetInstance().LoadXML(_FileNameByKernalData);
                //反序列化
                pkd = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(PlayerProperty)) as PlayerProperty;
                //赋值
            }
            catch
            {
                Debug.LogError(GetType() + "/ReadFromXML_PlayerKernalData()/读取游戏的核心参数,不成功，请检查");
            }
            return pkd;
        }

        /// <summary>
        /// 读玩家背包数据
        /// </summary>
        private List<InventoryItem> ReadFromXML_InventoryData()
        {            
            List<InventoryItem> itemlist = new List<InventoryItem>();
            //参数检查
            if (string.IsNullOrEmpty(_FileNameByPackageData))
            {
                Debug.LogError(GetType() + "/ReadFromXML_PlayerPackageData()/_FileNameByPackageData is Null!!!");
                return null;
            }
            try
            {
                //读取XML数据
                string strTemp = XmlOperation.GetInstance().LoadXML(_FileNameByPackageData);
                string[] items = strTemp.Split('|');
                for (int i = 0; i < items.Length; i++)
                {
                    InventoryItem item = new InventoryItem();
                    //反序列化
                    item = XmlOperation.GetInstance().DeserializeObject(strTemp, typeof(InventoryItem)) as InventoryItem;
                    itemlist.Add(item);
                }
              
            }
            catch
            {
                Debug.LogError(GetType() + "/ReadFromXML_PlayerPackageData()/读取游戏的背包参数,不成功，请检查");
            }
            return itemlist;

        }
        #endregion




    }
}
