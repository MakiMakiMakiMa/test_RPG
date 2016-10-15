using UnityEngine;
using System.Collections;
using Global;
namespace Controller
{
    /// <summary>
    /// 控制器父类
    /// </summary>
    public class ControllerBase : MonoBehaviour
    {
        /// <summary>
        /// 进入下一个场景
        /// </summary>
        /// <param name="scenesEnumName">场景（枚举）名称</param>
        protected void EnterNextScenes(SceneType sceneName)
        {
            //转到下一个场景
            GlobalParametersManager.NextSceneTYPE = sceneName;
            Application.LoadLevel(ConvertEnumToString.Instacne.GetSceneStrBySceneType(GlobalParametersManager.NextSceneTYPE));
        }
        
        #region 系统方法
        protected virtual void OnEnable() { }
        protected virtual void Awake() { }
        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void OnDisable() { }
        #endregion
    }
}
