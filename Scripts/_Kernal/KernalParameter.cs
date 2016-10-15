using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kernal
{
    public class KernalParameter
    {
#if UNITY_STANDALONE_WIN
        //系统配置信息_日志路径
        internal static readonly string SystemConfigInfo_LogPath ="file://" + Application.dataPath + "/StreamingAssets/SystemConfigInfo.xml";
        //系统配置信息_日志根节点的名称
        internal static readonly string SystemConfigInfo_LogRootNodeName = "SystemConfigInfo";
        //系统配置信息_对话路径
        internal static readonly string DialogsXMLConfig_XmlPath = "file://" + Application.dataPath + "/StreamingAssets/SystemDialogsInfo.xml";
        //系统配置信息_对话根节点的名称
        internal static readonly string DialogsXMLConfig_XmlPath_XmlRootNodeName ="Dialogs_CN";  
#elif UNITY_ANDROID
        //系统配置信息_日志路径
        internal static readonly string SystemConfigInfo_LogPath =Application.dataPath + "!/Assets/StreamingAssets/SystemConfigInfo.xml";
        //系统配置信息_日志根节点的名称
        internal static readonly string SystemConfigInfo_LogRootNodeName = "SystemConfigInfo";
#elif UNITY_IPHONE
        //系统配置信息_日志路径
        internal static readonly string SystemConfigInfo_LogPath =Application.dataPath + "/Raw/StreamingAssets/SystemConfigInfo.xml";
        //系统配置信息_日志根节点的名称
        internal static readonly string SystemConfigInfo_LogRootNodeName = "SystemConfigInfo";
#endif

    }
}
