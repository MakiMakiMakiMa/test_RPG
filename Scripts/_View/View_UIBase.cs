using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace View
{
    public class View_UIBase : MonoBehaviour
    {
        #region 数据定义
        protected bool isPress=false;
        #endregion
        void Awake()
        {
            GetAllChildButtons();
            Init();
        }
        protected virtual void OnDisable()
        { 
        }
	    protected virtual void Start () 
        {
          
	    }
        /// <summary>
        /// 监听UI点击事件,并注册
        /// </summary>
        private  void GetAllChildButtons()
        {
            Button[] buttons = this.GetComponentsInChildren<Button>(true);
            for (int i = 0, max = buttons.Length; i < max; i++)
            {
                Button button = buttons[i];
                if (button.gameObject.name.StartsWith("Btn"))
                {
                    button.onClick.AddListener(delegate() { this.OnClick(button.gameObject); });
                   // EventTriggerListener.Get(button.gameObject).onClick = OnClick;同上面的为委托方法一样
                    EventTriggerListener.Get(button.gameObject).onDown = OnDown;//按下事件
                    EventTriggerListener.Get(button.gameObject).onUp = OnUp;//按下事件
                    EventTriggerListener.Get(button.gameObject).onSelect = OnSelect;

                }
            }
        }
        /// <summary>
        /// 按下
        /// </summary>
        /// <param name="click"></param>
        protected virtual void OnSelect(GameObject click)
        {

        }
        /// <summary>
        /// 按下
        /// </summary>
        /// <param name="click"></param>
        protected virtual void OnDown(GameObject click)
        {
            
        }
        /// <summary>
        /// 抬起
        /// </summary>
        /// <param name="click"></param>
        protected virtual void OnUp(GameObject click)
        {
           
        }
        /// <summary>
        /// 持续按下时间
        /// </summary>
        protected virtual void OnPress()
        {
           
        }
        /// <summary>
        /// 注册到按钮上的点击事件
        /// </summary>
        protected virtual void OnClick(GameObject click){ }
        protected virtual void Init() { }

	    void Update ()
        {
            OnUpdate();
            OnPress();
	    }
        protected virtual void OnUpdate()
        { 
        }
    }
    /// <summary>
    /// 辅助类：将UGUI的点击按下事件注册为委托事件，自动监听
    /// </summary>
    public class EventTriggerListener:UnityEngine.EventSystems.EventTrigger
    {
        public delegate void ButtonDelegateEvent(GameObject click);
        public ButtonDelegateEvent onClick;
        public ButtonDelegateEvent onDown;
        public ButtonDelegateEvent onEnter;
        public ButtonDelegateEvent onUp;
        public ButtonDelegateEvent onSelect;
        public ButtonDelegateEvent onUpdateSelect;
        static public EventTriggerListener Get(GameObject click)
        {
            EventTriggerListener listener = click.GetComponent<EventTriggerListener>();
            if (listener == null) listener = click.AddComponent<EventTriggerListener>();
            return listener;
        }
        /// <summary>
        /// 按钮点击事件
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (onClick != null)
            {
                onClick(gameObject);
            }
        }
        /// <summary>
        /// 按钮抬起事件
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            if (onUp != null)
            {
                onUp(gameObject);
            }
        }
        /// <summary>
        /// 按钮按下事件
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            if (onDown!= null)
            {
                onDown(gameObject);
            }
        }
        /// <summary>
        /// 持续按住
        /// </summary>
        /// <param name="eventData"></param>
        public override void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
        {
            base.OnSelect(eventData);
                if (onSelect!= null)
            {
                onSelect(gameObject);
            }
        
        }
        public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (onEnter != null)
            {
                onEnter (gameObject);
            }
        }

    }
}
