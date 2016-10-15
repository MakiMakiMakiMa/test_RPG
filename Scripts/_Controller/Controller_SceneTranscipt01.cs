using UnityEngine;
using System.Collections;
using Global;
using UnityEngine.UI;
using Kernal;
namespace Controller
{
    public class Controller_SceneTranscipt01 : ControllerBase
    {
        private static Controller_SceneTranscipt01 _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_SceneTranscipt01 Instance
        {
            get { return Controller_SceneTranscipt01._instance; }
            set { _instance = value; }
        }
        #region 定义成员变量
        int i = 1;//测试用

        #endregion
        #region UI逻辑实现
        /// <summary>
        /// 点击了技能按钮按钮
        /// </summary>
        internal void OnAttackButtonClick(GameObject click, SkillType type, ActionType actionType, float rate)
        {
            if (type == SkillType.Basic && GlobalParametersManager.CurrentActionTYPE == ActionType.Basic)//当前为基础攻击 可以连击
            {                            
                Debug.Log("连击"+i);
                i++;
                if (i > 3) i = 1;
            }
            else
            {
                if (GlobalParametersManager.CurrentActionTYPE != ActionType.Idle) return;//当前状态为技能等待时可以释放技能
                if (GlobalParametersManager.CurrentActionFinshed == false) return;
            }
            Debug.Log(actionType.ToString() + "技能被点击已经被点击了");
            Image mask = click.transform.GetChild(0).GetComponent<Image>();
            Text txtCold = click.transform.GetChild(1).GetComponent<Text>();
            mask.gameObject.SetActive(true);
            txtCold.gameObject.SetActive(true);
            Controller_PlayerAction.Instance.PlayAction(actionType);//播放攻击动画
            if (type == SkillType.Basic)
            {
                StartCoroutine(SkillCold(txtCold,mask, rate, 10f));
            }
            else
            {
                StartCoroutine(SkillCold(txtCold,mask, rate));
            }
        }
        /// <summary>
        /// 技能图标遮罩自动消除
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        IEnumerator SkillCold(Text txtCold,Image mask,float rate1,float rate2=1f)
        {
            yield return new WaitForSeconds(0.01f * rate1);
            mask.fillAmount -= 0.01f*rate2;
            txtCold.text = ((int)(mask.fillAmount* rate1+1)).ToString();
            if (mask.fillAmount > 0)
            {
                StartCoroutine(SkillCold(txtCold,mask, rate1, rate2));
            }
            else
            {
                txtCold.text = "0";
                yield return new WaitForSeconds(0.1f);
                mask.fillAmount = 1;             
                mask.gameObject.SetActive(false);
                txtCold.gameObject.SetActive(false);
            }
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
            AudioManager.Instacne.PlayAudioClip(AudioSourceType._BGAudioSource, "LevelThree");
        }

        protected override void Update()
        {
            base.Update();
        }
        #endregion
    }
}
