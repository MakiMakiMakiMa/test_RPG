using UnityEngine;
using System.Collections;

namespace Global
{
    /// <summary>
    /// 全局变量定义
    /// </summary>
    public static class GlobalParametersManager
    {
        public static SceneType NextSceneTYPE{get;set;}
        public static ActionType  CurrentActionTYPE { get; set; }
        public static bool CurrentActionFinshed { get; set; }
        public static string CurrentPlayerName { get; set; }
    }
}
