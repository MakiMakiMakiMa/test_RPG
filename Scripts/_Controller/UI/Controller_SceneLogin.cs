using UnityEngine;
using System.Collections;
using Global;
using Kernal;
namespace Controller
{
    public class Controller_SceneLogin : ControllerBase
    {

        private static Controller_SceneLogin _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_SceneLogin Instance
        {
            get { return Controller_SceneLogin._instance; }
            set { _instance = value; }
        }
        #region 定义成员变量
        AsyncOperation o;
        #endregion
        #region UI逻辑实现
        /// <summary>
        /// 点击继续游戏按钮
        /// </summary>
        internal void OnContinueButtonClick()
        {
            Debug.Log("OnContinueButtonClick已经被点击了");                     
        }
        /// <summary>
        /// 点击开始游戏按钮
        /// </summary>
        internal void OnStartButtonClick()
        {
            GlobalParametersManager.NextSceneTYPE = SceneType.SceneRole;
            o = Application.LoadLevelAsync(ConvertEnumToString.Instacne.GetSceneStrBySceneType(GlobalParametersManager.NextSceneTYPE));
            o.allowSceneActivation = false;
            FadeAndOut.Instance.SetSceneFadeDown();
        }
        #endregion
        #region 实现父类
        protected override void Awake()
        {
        	 base.Awake();
              _instance = this;
        }
        protected override void Start()
        {
            base.Start();
            AudioManager.Instacne.PlayAudioClip(AudioSourceType._BGAudioSource, "StartScenes");

           
        }

        protected override void Update()
        {
            base.Update();
            if (FadeAndOut.Instance._fadeComplete&&o!=null)
            {

                o.allowSceneActivation = true;
            }
        }
        #endregion
    }
}
