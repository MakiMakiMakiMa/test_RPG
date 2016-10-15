using UnityEngine;
using System.Collections;
/// <summary>
/// ResourcesManager资源管理器
/// </summary>
public class ResourcesManager : MonoBehaviour
{
    #region 资源加载器的获取
    private static ResourcesManager mInstance;
    /// <summary>
    /// 获取资源加载实例
    /// </summary>
    /// <returns></returns>
    public static ResourcesManager GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new GameObject("_ResourcesManager").AddComponent<ResourcesManager>();
        }
        return mInstance;
    }
    #endregion
    private Hashtable _cacheHash;//缓存器
    private ResourcesManager()
    {
        _cacheHash = new Hashtable();
    }
    #region 资源加载方法重新封装
    /// <summary>
    /// Load 资源(预设)
    /// </summary>
    /// <typeparam name="T">预设:UnityEngine.Object</typeparam>
    /// <param name="path">path：T路径</param>
    /// <param name="cache">cache：是否加入缓存器</param>
    /// <returns>T预设</returns>
    public T Load<T>(string path,bool cache)where T:UnityEngine.Object
    {
        if (_cacheHash.ContainsKey(path))//如果已经在缓存器,直接返回
        {
            return _cacheHash[path] as T;
        }
        Debug.Log(string.Format("Load assset frome resource folder,path:{0},cache:{1}", path, cache));
        T assetObj =Resources.Load<T>(path);
        if (assetObj == null)
        {
            Debug.LogWarning("Resources中找不到资源：" + path);
        }
        if (cache)
        {
            _cacheHash.Add(path, assetObj);
            Debug.Log("Asset对象已缓存,Resource'path=" + path);
        }
        return assetObj;
    }
   /// <summary>
    /// 创建Resource中GameObject对象
   /// </summary>
    /// <param name="path">path"资源路径</param>
    /// <param name="cache">cache：是否加入缓存器</param>
    /// <returns>GameObject的实例化</returns>
    public GameObject CreateGameObject(string path, bool cache)
    {
        GameObject assetObj = Load<GameObject>(path, cache);
        GameObject go = GameObject.Instantiate(assetObj) as GameObject;
        if (go ==null)
        {
            Debug.LogWarning("从Resource创建对象失败：" + path);
        }
        return go;
    }
    #endregion
}
