using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Kernal
{
    public interface IConfigManager
    {
        /// <summary>
        /// 属性：应用设置
        /// </summary>
        Dictionary<string, string> AppSetting { get; }

        /// <summary>
        /// 得到AppSetting的最大数量
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();
    }
}