/*
 *
 *  Title:  学习“高级对象缓冲池”技术
 *          
 *          多模缓冲池管理器       
 * 
 *  Descripts: 
 *          
 *  Author:ypp
 *
 *  Date: 2016.09.22
 * 
 *
 *  Version: 0.1
 *
 *
 *  Modify Record:
 *        [描述版本修改记录]
 *
 *
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Kernal
{
    /// <summary>
    /// 池:挂载对象为重复性利用较高的模型
    /// </summary>
    public class Pools : MonoBehaviour
    {
        #region 成员变量
        /// <summary> 本类挂载游戏对象位置   </summary>
        public Transform gosOnPos;       
        /// <summary>单模池集合 </summary>
        public List<PoolOption> poolOptionList = new List<PoolOption>();
        public bool isUsedTime = false; //是否使用时间戳
        #endregion
        void Awake()
        {
            PoolManager.Add(this);
            gosOnPos = transform;
            PrefabLoadGo();

        }
        void Start()
        {
            if (isUsedTime)
            {
                InvokeRepeating("ProecessGo_NameTime", 1, 10);
            }
        }
        #region 原始方法
        /// <summary>
        ///   
        /// 时间戳处理
        /// 主要业务逻辑:
        /// 1>： 每间隔10秒种，对所有正在使用的活动状态游戏对象的时间戳减去10秒。
        /// 2>:  检查每个活动状态的游戏对象名称的时间戳如果小于等于0，则进入禁用状态。
        /// 3>:  重新进入活动状态的游戏对象，获得预先设定的存活时间写入对象名称的时间戳中。
        /// </summary>
        /// <param name="parent">回收后的挂在对象</param>
        void ProecessGo_NameTime()
        {
            for (int i = 0; i < poolOptionList.Count; i++)
            {
                PoolOption po = poolOptionList[i];
                po.AllActiveGameObjectTimeSubtraction(gosOnPos);
            }
        }
        /// <summary>
        /// 预设加载
        /// </summary>
        public void PrefabLoadGo()
        {
            for (int i = 0; i < poolOptionList.Count;i++ )
            {
                PoolOption po=poolOptionList[i];
                for (int j = po.totalCount(); j < po.numCacheCapcity; j++)//最开始的时候totalCount()为0
                {
                    GameObject obj = po.PrefabLoad(po.prefab, Vector3.zero, Quaternion.identity);
                    obj.transform.parent = gosOnPos;
                }
            }
        }
        /// <summary>
        /// 从缓冲池中获取与当前需求预设相同的物体
        /// 功能描述： 
        /// 1： 对指定“预设”在自己的缓冲池中激活一个，且加入自己缓冲池中的"可用激活池"。
        /// 2： 然后再建立一个池对象，且激活预设，再加入自己的缓冲池中的“可用激活池”中。
        /// </summary>
        /// <param name="prefab">需求的预设体</param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        /// <returns></returns>
        public GameObject GetGameObjectByPools(GameObject prefab, Vector3 pos, Quaternion rot)
        {
            GameObject obj = null;
            for (int i = 0; i < poolOptionList.Count; i++)
            {
                PoolOption po = poolOptionList[i];
                if (prefab == po.prefab)
                {
                    obj = po.ObjActive(pos,rot);
                    if (obj == null) return null;
                    //所有激活的游戏对象必须是本类所挂空对象的子对象。
                    if (obj.transform.parent != gosOnPos)
                    {
                        obj.transform.parent = gosOnPos;
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 将激活状态的物体回收到池子的非激活集合内
        /// </summary>
        /// <param name="go"></param>
        public void RecoverGameObjectToPools(GameObject go)
        {
            for (int i = 0; i < poolOptionList.Count; i++)
            {
                PoolOption opt = poolOptionList[i];
                //检查自己的每一类“池”中是否包含指定的“预设”对象。
                if (opt.activeGosList.Contains(go))
                {
                    if (go.transform.parent != gosOnPos)
                        go.transform.parent = gosOnPos;
                    //特定“池”回收这个指定的对象。
                    opt.ObjDeactive(go);
                }
            }
        }
        /// <summary>
        /// 销毁未使用对象
        /// </summary>
        public void DestoryDisused()
        {
            for (int i = 0; i < poolOptionList.Count; i++)
            {
                PoolOption opt = poolOptionList[i];
                opt.DestoryAllDeactiveObj();
            }
        }
        /// <summary>
        /// 销毁指定数量的模型
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="count"></param>
        public void DestoryParticalPrefab(GameObject prefab, int count)
        {
            for (int i = 0; i < poolOptionList.Count; i++)
            {
                PoolOption opt = poolOptionList[i];
                if (prefab == poolOptionList[i].prefab)
                {                 
                    opt.DestroyParticlDeactiveObj(count);
                    return;
                }
            }
            
        }

        /// <summary>
        /// 停止时间戳
        /// </summary>
        public void OnDestory()
        { 
            if(isUsedTime)
            {
                CancelInvoke("ProecessGo_NameTime");
            }
            for (int i = 0; i <poolOptionList.Count; i++)
            {
                PoolOption opt = poolOptionList[i];
                opt.ResetPool();
            }
        }
#endregion
        #region 拓展方法
        /// <summary>
        /// 销毁池子里的某个物体
        /// </summary>
        /// <param name="obj"></param>
        public void DestoryParticalCloneObj(GameObject obj)
        {
            for (int i = 0; i < poolOptionList.Count; i++)
            {
                PoolOption opt = poolOptionList[i];
                if (obj.name.Contains(poolOptionList[i].prefab.name))//同一个预设
                {
                    opt.DestroyParticlActiveObj(obj);
                    return;
                }
            }
        }
        #endregion
    }//End_class Pools

    /// <summary>
    /// 游戏（单类型）缓冲对象管理（即：单模池操作管理）
    /// 功能： 激活、收回、预加载等。
    /// </summary>
    [System.Serializable]
    public class PoolOption 
    {        
        #region 成员变量
        public GameObject prefab;           //挂载的预设体
        public int numCacheCapcity=0;         //初始缓冲容量
        public float timeAutoDeactive=30f;      //模型自动禁用时间

        /// <summary>活动使用状态游戏对象集合</summary>
        [HideInInspector]
        public List<GameObject> activeGosList = new List<GameObject>();     
        /// <summary>禁用状态游戏对象集合</summary>
        public List<GameObject> deactiveGosList = new List<GameObject>();  

        private int _index = 0;     //初始索引
        #endregion

        #region 调用方法
        /// <summary>
        /// 返回非活体预加载对象
        /// </summary>
        /// <param name="prefab">克隆预设</param>
        /// <param name="initPos">实体位置（初始位置）</param>
        /// <param name="initRotation">实体四元数（初始位置）</param>
        /// <returns></returns>
        internal GameObject PrefabLoad(GameObject prefab,Vector3 initPos,Quaternion initRotation)
        {
            GameObject obj = null;
            if(prefab!=null)
            {
                obj = GameObject.Instantiate(prefab, initPos, initRotation) as GameObject;
            }
            ReName(obj);    //重命名
            obj.SetActive(false);
            deactiveGosList.Add(obj);
            return obj;
        }
        /// <summary>
        /// 游戏对象激活
        /// </summary>
        /// <param name="showPos">激活对象的显示位置（新位置）</param>
        /// <param name="showRota">激活对象的显示四元数（新位置）</param>
        /// <returns></returns>
        internal GameObject ObjActive(Vector3 showPos,Quaternion showRota)
        {
            GameObject obj;
            if (deactiveGosList.Count > 0)
            {
                //从非活动集合里取索引下表为0的数字 放到活动激活的末尾处（非活动集合：先进先出）
                obj = deactiveGosList[0];
                deactiveGosList.RemoveAt(0);
            }
            else //对象池需要补充新元素
            {
                obj = GameObject.Instantiate(prefab, showPos, showRota) as GameObject;
                ReName(obj);   
            }
            //将激活模型放到新位置
            obj.transform.localPosition = showPos;
            obj.transform.localRotation = showRota;
            //激活模型加入活动对象池
            activeGosList.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        /// <summary>
        /// 将活动游戏禁用
        /// </summary>
        /// <param name="go"></param>
        internal void ObjDeactive(GameObject go)
        {
            activeGosList.Remove(go);
            deactiveGosList.Add(go);
            go.SetActive(false);
        }
        /// <summary>
        /// 游戏对象规则化命名
        /// </summary>
        /// <param name="obj"></param>
        internal void ReName(GameObject go)
        {
            go.name += (_index + 1).ToString("#000");
            //游戏对象自动禁用时间戳
            go.name = timeAutoDeactive + "@" + go.name;
            _index++;
        }
        /// <summary>
        /// 返回活动池或者非活动池的总数（即池容量）
        /// </summary>
        /// <returns></returns>
        internal int totalCount()
        {
            return activeGosList.Count + deactiveGosList.Count;
        }
        /// <summary>
        /// 清空池
        /// </summary>
        internal void ResetPool()
        {
            activeGosList.Clear();
            deactiveGosList.Clear();
        }
        /// <summary>
        /// 彻底清空非活动集合
        /// </summary>
        internal void DestoryAllDeactiveObj()
        {
            foreach (GameObject go in deactiveGosList)
            {
               Object.Destroy(go, 0.1f);
            }
            deactiveGosList.Clear();
        }
        /// <summary>
        /// 部分清空非活动集合
        /// </summary>
        /// <param name="count">具体清空数量</param>
        internal void DestroyParticlDeactiveObj(int count)
        {
            if (count >= deactiveGosList.Count)
            {
                DestoryAllDeactiveObj();
                return;
            }
            else 
            {
                for (int i = deactiveGosList.Count - 1; i > deactiveGosList.Count-count; i--)
                {
                    Object.Destroy(deactiveGosList[i]);
                }
                deactiveGosList.RemoveRange(deactiveGosList.Count-count,count);
            }
        }
        /// <summary>
        /// 回调函数：时间戳管理
        /// 功能：所有游戏对象进行时间倒计时管理，时间小于零则进行“非活动”容器集合中，即:按时间自动回收游戏对象。 　　　
        /// </summary>
        internal void AllActiveGameObjectTimeSubtraction(Transform parent)
        {
            for (int i = 0; i < activeGosList.Count;i++ )
            {
                string strHead = null;
                string strTail = null;
                int intTimeInfo = 0;
                GameObject goActive = null;

                goActive = activeGosList[i];
                string[] strArray = goActive.name.Split('@');
                strHead = strArray[0];
                strTail = strArray[1];

                intTimeInfo = System.Convert.ToInt32(strHead);
                if (intTimeInfo >=10)
                {
                    strHead = (intTimeInfo - 10).ToString();
                }
                else if(intTimeInfo<=0)
                {
                    //游戏对象自动转为禁用
                    goActive.name = timeAutoDeactive.ToString() + "@" + strTail;
                    ObjDeactive(goActive);
                    goActive.transform.parent = parent;
                    continue;
                }
                goActive.name = strHead + "@" + strTail;
            }
        }
        #endregion
        #region 拓展方法
        /// <summary>
        /// 销毁一个物体
        /// </summary>
        /// <param name="obj"></param>
        internal void DestroyParticlActiveObj(GameObject obj)
        {

                for (int i = activeGosList.Count; i > 0; i--)
                {
                    if (activeGosList[i] == obj)
                    {
                        //回收一个再
                        activeGosList.Remove(obj);
                        Object.Destroy(activeGosList[i]);
                        return;
                    }
                }

        }
        #endregion
    }//end_class PoolOption 

    /// <summary>
    /// 内部类： 池时间
    /// </summary>
    //[System.Serializable]
    public class PoolTimeObject
    {
        public GameObject _instace;
        public float _time;
    }//end_class PoolTimeObject
}