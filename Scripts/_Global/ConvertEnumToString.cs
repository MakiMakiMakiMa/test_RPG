using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Global
{
    public class ConvertEnumToString 
    {
        private static ConvertEnumToString _instacne;
        public static ConvertEnumToString Instacne
        {
            get
            {
                if (_instacne == null)
                {
                    _instacne = new ConvertEnumToString();
                }
                return ConvertEnumToString._instacne; 
            }
        }
        #region 定义成员变量
        private Dictionary<SceneType, string> enumDic = new Dictionary<SceneType, string>();
        #endregion
        #region 定义成员变量
        /// <summary>
        /// 数据装入字典
        /// </summary>
        private ConvertEnumToString()
        {
            enumDic.Add(SceneType.SceneLogin, SceneType.SceneLogin.ToString());
            enumDic.Add(SceneType.SceneLoading, SceneType.SceneLoading.ToString());
            enumDic.Add(SceneType.SceneRole, SceneType.SceneRole.ToString());
            enumDic.Add(SceneType.SceneTran01, SceneType.SceneTran01.ToString());
            enumDic.Add(SceneType.SceneCity, SceneType.SceneCity.ToString());
        }
        /// <summary>
        /// 取得登路场景的名称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetSceneStrBySceneType(SceneType type)
        {
            if (enumDic != null && enumDic.Count > 0)
            {
                string str = enumDic[type];
                return str;
            }
            else
            {
                Debug.LogWarning("没有找到场景:"+type.ToString());
                return SceneType.SceneLogin.ToString();//返回开始界面
            }
             
        }
        #endregion
    }
}
