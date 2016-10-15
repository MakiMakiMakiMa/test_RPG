using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Controller
{
    /// <summary>
    /// 总的UI处理
    /// </summary>
    public class Controller_SceneBase : MonoBehaviour
    {
        private static Controller_SceneBase _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_SceneBase Instance
        {
            get { return Controller_SceneBase._instance; }
            set { _instance = value; }
        }

        public Dictionary<string, GameObject> UIBarDic = new Dictionary<string, GameObject>();
        void Awake()
        {
            _instance = this;
        }
        void Start()
        {

        }
        /// <summary>
        /// UI集合
        /// </summary>
        public void GetUIBarDic(GameObject[] gos)
        {
            for (int i = 0; i < gos.Length; i++)
            {
                this.UIBarDic.Add(gos[i].name,gos[i]);
            }
        }
        /// <summary>
        /// UI集合
        /// </summary>
        public void GetUIBarDic(Dictionary<string, GameObject> dic)
        {
            this.UIBarDic = dic;
        }

        void Update()
        {

        }
    }
}
