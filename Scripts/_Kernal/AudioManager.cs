using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;
namespace Kernal
{
    /// <summary>
    /// AudioManager声音管理器
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instacne;
        public static AudioManager Instacne
        {
            get
            {
                if (_instacne == null)
                {
                    _instacne = new AudioManager();
                }
                return AudioManager._instacne;
            }
        }
        public Transform _target;
        private float _BGAS_VolumeValue = 1f;
        private float _EffectAS_VolumeValue = 1f;

        private Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();//定义音频存储片段的字典
        public AudioClip[] _ClipArray;//音效片段
        private AudioSource[] _AudioSourceArray;//音源数组
        private AudioSource _BackgrounAS;//背景音乐音源
        private AudioSource _EffectAS;//效果音乐音源
        private AudioSource _PlayerActionAS;//效果音乐音源
        private AudioSource _JsActionAS;//效果音乐音源
        void Awake()
        {
            _instacne = this;
            //取得所有音源
            _AudioSourceArray = this.GetComponents<AudioSource>();
            _BackgrounAS = _AudioSourceArray[0];
            _EffectAS = _AudioSourceArray[1];
            _PlayerActionAS = _AudioSourceArray[2];
            _JsActionAS = _AudioSourceArray[3];
            //加载音频片段
            foreach (AudioClip clip in _ClipArray)
            {
                audioClipDic.Add(clip.name, clip);
            }

        }
        void Start()
        {
            //设置音效初始值
            //_BGAS_VolumeValue = GlobalManager._BGAS_VolumeValue;
           // _EffectAS_VolumeValue = GlobalManager._EffectAS_VolumeValue;
            //DontDestroyOnLoad(this.gameObject);
        }
        /// <summary>
        /// 播放场景中的音乐
        /// </summary>
        /// <param name="type"></param>音源的类型
        /// <param name="clipName"></param>音效片段名称
        public void PlayAudioClip(AudioSourceType type, string clipName)
        {
            AudioClip clipPlay = null;
            bool isExist = audioClipDic.TryGetValue(clipName, out clipPlay);
            if (isExist == false)
            {
                Debug.Log("Can't find  the AduioClip need to play,AduioClip name is " + clipName + "!");
                return;
            }
            switch (type)
            {
                case AudioSourceType._BGAudioSource:
                    if (_BackgrounAS.clip == clipPlay) return;//防止重复播放同一个背景音乐
                    _BackgrounAS.clip = clipPlay;
                    _BackgrounAS.Play();
                    _BackgrounAS.loop = true;
                 //   _BackgrounAS.volume = GlobalManager._BGAS_VolumeValue;//待定
                    break;
                case AudioSourceType._EffectAudioSource:
                    _EffectAS.clip = clipPlay;
                    _EffectAS.loop = false;
                   // _EffectAS.volume = GlobalManager._EffectAS_VolumeValue;
                    _EffectAS.Play();
                    break;
                case AudioSourceType._PlayerActionAS:
                    _PlayerActionAS.clip = clipPlay;
                    _PlayerActionAS.loop = false;
                   // _PlayerActionAS.volume = GlobalManager._EffectAS_VolumeValue;
                    _PlayerActionAS.Play();
                    break;
                case AudioSourceType._JsActionAS:
                    _JsActionAS.loop = false;
                   // _JsActionAS.volume = GlobalManager._EffectAS_VolumeValue;
                    if (_JsActionAS.clip == clipPlay && _JsActionAS.clip != null)
                    {
                        _JsActionAS.Play();
                    }
                    else
                    {
                        _JsActionAS.clip = clipPlay;
                        _JsActionAS.Play();
                    }


                    break;
            }

        }
        /// <summary>
        /// 调节声音大小
        /// </summary>
        /// <param name="type"></param>音源的类型
        /// <param name="_value"></param>声音大小
        public void SetVolumeValue(AudioSourceType type, float _value)
        {
            switch (type)
            {
                case AudioSourceType._BGAudioSource:
                   // GlobalManager._BGAS_VolumeValue = _value;
                    this._BGAS_VolumeValue = _value;
                    _BackgrounAS.volume = this._BGAS_VolumeValue;
                    break;
                case AudioSourceType._EffectAudioSource:
                  //  GlobalManager._EffectAS_VolumeValue = _value;
                    this._EffectAS_VolumeValue = _value;
                    _EffectAS.volume = this._EffectAS_VolumeValue;
                    _PlayerActionAS.volume = this._EffectAS_VolumeValue;
                    _JsActionAS.volume = this._EffectAS_VolumeValue;
                    break;
            }
        }


        void Update()
        {
            if (_target != null)
            {
                transform.position = _target.position;
            }
        }
    }
}
