/*
 *
 *  Title:  学习“高级对象缓冲池”技术
 *  
 *          多模复合缓冲池管理器
 *          
 *  Descripts: 
 *          “多模复合缓冲池”管理器，含义就是可以管理多类型、多样式的游戏对象缓冲处理。
 *           层级关系是： 
 *               PoolManger(管理大类)-->Pools(管理小类[多种类型游戏对象])-->PoolOption（单类型游戏对象管理）
 *          
 *  Author: ypp
 *
 *  Date:  2016.09.20
 *
 *  Version: 0.1
 *
 *  Modify Record:
 *        [描述版本修改记录]
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Kernal
{
    /// <summary>
    /// 池管理器
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        /// <summary>缓冲池集合/ </summary>
        public static Dictionary<string, Pools> poolMngDic = new Dictionary<string, Pools>();
        /// <summary>
        /// 添加池子
        /// </summary>
        public static void Add(Pools pool)
        {
            if (poolMngDic.ContainsKey(pool.name))
            {
                return;
            }
            else
            {
                poolMngDic.Add(pool.name,pool);
            }
        }
        /// <summary>
        /// 销毁所有不用的池子
        /// </summary>
        public static void DestoryAllInactive()
        { 
            foreach( KeyValuePair<string,Pools> keyValue in poolMngDic)
            {
                keyValue.Value.DestoryDisused();
            }
        }
        /// <summary>
        /// 清空所有池
        /// </summary>
        public static void OnDestory()
        {
            poolMngDic.Clear();
        }
    }
}