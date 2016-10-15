using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using Controller;
namespace View
{
    /// <summary>
    /// 挂载panel的基础（画布）
    /// </summary>
    public class View_SceneBase : MonoBehaviour
    {
        private Dictionary<string, GameObject> UIBarDic = new Dictionary<string, GameObject>();
        GameObject[] gos;
        void Awake()
        {
            View_UIBase[] Bars = this.GetComponentsInChildren<View_UIBase>(true);//获取子类的所有View_UIBase类
            gos = new GameObject[Bars.Length];
            if (Bars != null)
            {
                for (int i = 0; i < Bars.Length; i++)
                {
                    gos[i] = Bars[i].gameObject;
                    Debug.Log(Bars[i].gameObject.name);
                    UIBarDic.Add(Bars[i].gameObject.name, Bars[i].gameObject);//把所有UI模块装进字典
                }
            }
            else
            {
                Debug.Log("没有找到Bar");
            }
        }

        void Start()
        {
            Controller_SceneBase.Instance.GetUIBarDic(gos);
        }

        void Update()
        {

        }
    }
}
