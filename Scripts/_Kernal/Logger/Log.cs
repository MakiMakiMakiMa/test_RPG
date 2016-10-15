using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System;
namespace Kernal
{
    public static class Log
    {
        private static List<string> _LogCacheList;         //Log日志缓存数据
        private static string _LogPath;                      //Log日志文件路
        private static StateType _LogState;                 //Log日志状态(部署模式） 
        private static int _LogMaxCapacity;                 //Log日志最大容量
        private static int _LogBufferMaxNumber;             //Log日志缓存最大容量

        static Log()
        {
            _LogCacheList = new List<string>();
            IConfigManager configMng = new ConfigManager(KernalParameter.SystemConfigInfo_LogPath,KernalParameter.SystemConfigInfo_LogRootNodeName);
            _LogPath = configMng.AppSetting["LogPath"];
            if (string.IsNullOrEmpty(_LogPath)) 
            {
                //如果提取为空(persistentDataPath)可读可写的沙盒目录
                _LogPath =UnityEngine.Application.persistentDataPath + "\\LoL_Log.txt";
            }
            string strLogState = configMng.AppSetting["LogState"];
            if (!string.IsNullOrEmpty(strLogState))
            {
                switch (strLogState)
                {
                    case "Develop":
                        _LogState = StateType.Develop;
                        break;
                    case "Speacial":
                        _LogState = StateType.Speacial;
                        break;
                    case "Deploy":
                        _LogState = StateType.Deploy;
                        break;
                    case "Stop":
                        _LogState = StateType.Stop;
                        break;

                    default:
                        _LogState = StateType.Stop;                 //如果写错不输出
                        break;
                }
            }
             else
            {
                 _LogState = StateType.Stop;  
            }
             string strLogMaxCapacity=configMng.AppSetting["LogMaxCapacity"];
            if(!string.IsNullOrEmpty(strLogMaxCapacity))
            {
                _LogMaxCapacity= System.Convert.ToInt32(strLogMaxCapacity);
            }
            else
            {
                _LogMaxCapacity=2000;
            }
             string strLogBufferMaxNumber =configMng.AppSetting["LogBufferNumber"];
            if (!string.IsNullOrEmpty(strLogBufferMaxNumber))
            {
                _LogBufferMaxNumber = System.Convert.ToInt32(strLogBufferMaxNumber);
            }
            else
            {
                _LogBufferMaxNumber = 1;                                      
            }
            //Debug.Log("1231231");
            if(!File.Exists(_LogPath))
            {
                Debug.Log("1231231");
                File.Create(_LogPath);
                Thread.CurrentThread.Abort();
            }
            SyncFileDataToLogArray();
        }

        private static void SyncFileDataToLogArray()
        {
            if (!string.IsNullOrEmpty(_LogPath))
            {
                StreamReader sr = new StreamReader(_LogPath);
                while (sr.Peek() >= 0)
                {
                    _LogCacheList.Add(sr.ReadLine());
                }
                sr.Close();
            }
        }

        public static  void Write(string writeFileData, LevelType level)
        {          
            if (_LogState == StateType.Stop) return;
            
            if (_LogCacheList.Count > _LogMaxCapacity) _LogCacheList.Clear();
            if (!string.IsNullOrEmpty(writeFileData))
            {
                
                writeFileData = "Log State:" + _LogState.ToString() + "/" + DateTime.Now.ToString() + "/" + writeFileData;
                if (level ==LevelType.High)
                {
                    writeFileData = "@@@ Error Or Warring Or Important  !!! @@@" + writeFileData;

                    //重要信息或者错误或者？？？？？？
                }
                switch (_LogState)
                {
                   case StateType.Develop:                      
                        AppendDataToFile(writeFileData); 
                        break;
                    case StateType.Speacial:                                                          
                        if (level ==  LevelType.High || level ==  LevelType.Special)
                        {
                            AppendDataToFile(writeFileData); 
                        }
                        break;
                    case StateType.Deploy:                                                            
                        if (level == LevelType.High)
                        {
                            AppendDataToFile(writeFileData);
                        }
                        break;
                    case StateType.Stop:                                                              
                        break;
                    default:
                        break;
                }
            }

        }

        private static void AppendDataToFile(string writeFileDate)
        {

            if (!string.IsNullOrEmpty(writeFileDate))
            {
                //调试信息数据追加到缓存集合中
               _LogCacheList.Add(writeFileDate);

            }
            //缓存集合数量超过一定"指定数量_LogBufferMaxNumber"，则同步到实体文件中
            if (_LogCacheList.Count % _LogBufferMaxNumber == 0)
            {
                //同步缓存中的数据信息到实体文件中
                SyncLogArrayToFile();
            }
            
        }
        /// <summary>
        /// 同步缓存中的数据信息到实体文件中
        /// </summary>
        private static void SyncLogArrayToFile()
        {
            if (!string.IsNullOrEmpty(_LogPath))
            {
                StreamWriter sw = new StreamWriter(_LogPath);
                foreach (string item in _LogCacheList)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
        }
    }
    public enum StateType
    {
        Develop,                                                            //开发模式（输出所有日志内容）
        Speacial,                                                           //指定输出模式
        Deploy,                                                             //部署模式（最核心日志信息，例如严重错误信息，用户登陆账号）
        Stop                                                                //停止输出模式（不输出任何日志部署）
    }
    /// <summary>
    /// 调试信息的等级(表示调试信息本身的重要程度)
    /// </summary>
    public enum LevelType
    {
        High,
        Special,
        Low
    }

}