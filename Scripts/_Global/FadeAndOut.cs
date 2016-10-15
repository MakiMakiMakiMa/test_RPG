using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Global
{
    public class FadeAndOut : MonoBehaviour {

        
        private static FadeAndOut _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static FadeAndOut Instance
        {
            get { return FadeAndOut._instance; }
            set { FadeAndOut._instance = value; }
        }
        #region 定义成员变量
        private RawImage mFadeImage;
        private bool _fadeUp = false;     //控制屏幕逐渐清晰
        private bool _fadeDown = false;    //控制屏幕逐渐暗淡
        private float _fadeSpeed = 0.8f;    //控制屏幕逐渐暗淡速度
        public bool _fadeComplete = false;
        #endregion

        void Awake()
        {
            _instance = this;
            mFadeImage =GameObject.Find("Canvas/FadeAndOut").transform.GetComponent<RawImage>();
        }
	    void Start () 
        {
           // mFadeImage.color = Color.clear;
          //  mFadeImage.enabled = false;
            SetSceneFadeUp();
	    }

        #region UI逻辑实现
        /// <summary>
        /// 控制变清晰
        /// </summary>
        public void SetSceneFadeUp()
        {
            _fadeUp = true;
            _fadeDown = false;
        }
        /// <summary>
        /// 控制变黑暗
        /// </summary>
        public void  SetSceneFadeDown()
        {
            _fadeUp = false;
            _fadeDown = true;

        }
        void FadeDownEffectShow()
        {
            if (mFadeImage.color == Color.black)
            {
                _fadeComplete = true;
                return;
            }
            mFadeImage.enabled = true;
            mFadeImage.color= Color.Lerp(mFadeImage.color, Color.black, Time.deltaTime * _fadeSpeed);
            if (mFadeImage.color.a>=0.95f)
            {
                mFadeImage.color = Color.black;
            }
           // mFadeImage.enabled = false;
        }
        void FadeUpEffectShow()
        {
            if (mFadeImage.color == Color.clear)
            {
                //_fadeComplete = true;
                return;
            }
            mFadeImage.enabled = true;
            mFadeImage.color = Color.Lerp(mFadeImage.color, Color.clear, Time.deltaTime * _fadeSpeed);
            if (mFadeImage.color.a < 0.05f)
            {
                mFadeImage.color = Color.clear;
            }
           // mFadeImage.enabled = false;
        }
        #endregion
        void Update ()
        {
            if (mFadeImage.color == Color.clear || mFadeImage.color == Color.black)
            {
                mFadeImage.enabled = false;
            }
            if (_fadeDown)
            {
                FadeDownEffectShow();
            }
            else if (_fadeUp)
            {
                FadeUpEffectShow();
            }
	    }
}
}
