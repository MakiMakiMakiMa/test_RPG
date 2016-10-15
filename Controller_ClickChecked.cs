using UnityEngine;
using System.Collections;
using View;
using UnityEngine.EventSystems;
using System.Collections.Generic;
namespace Controller
{
    /// <summary>
    /// 检测npc的点击事件
    /// </summary>
    public class Controller_ClickChecked : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray rays = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(rays,out hit)) {
                    if (EventSystem.current.IsPointerOverGameObject()) return;//判断当前鼠标是否点击在UI上
                    if (hit.transform.gameObject.tag =="NPC")
                    {
                        string dialogId=hit.transform.gameObject.name.Substring(0,1);
                        transform.Find("DialogsBar").gameObject.SetActive(true);
                        transform.Find("DialogsBar").GetComponent<View_SceneDialogs>().DialogCount = int.Parse(dialogId);
                    }
                }
            }
        }

      
    }

    /// <summary>
    /// 检测鼠标是否点击在UI上
    /// </summary>
    public class ClickIsOverUI 
    {
        private static ClickIsOverUI _instance = new ClickIsOverUI();

        public static ClickIsOverUI Instance
        {
            get { return ClickIsOverUI._instance; }
        }
        //使用触摸手势ID
        int id = Input.GetTouch(0).fingerId;
        public bool isPointOverUIObject(int fingerID)
        {
            return EventSystem.current.IsPointerOverGameObject(fingerID);
        }
        //UI时间射线
        public bool isPointOverUIObject(Vector2 screenPosition)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = screenPosition;
            List<RaycastResult> list = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData,list);
            return list.Count > 0;
        }

    }

}
