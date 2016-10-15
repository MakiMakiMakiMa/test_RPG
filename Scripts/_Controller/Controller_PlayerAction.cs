using UnityEngine;
using System.Collections;
using Global;
using System.Collections.Generic;
using Kernal;
namespace Controller
{
    public class Controller_PlayerAction  : MonoBehaviour
    {
        private static Controller_PlayerAction _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_PlayerAction Instance
        {
            get { return Controller_PlayerAction._instance; }
            set { _instance = value; }
        }
        private GameObject[] effectPoses;
        #region 定义成员变量
        private GameObject _player;
        private Animator transAnim;//状态机
        private bool isDeath = false;
        private float _distanceAttack = 0;
        private float _playerDamage = 0;
        #endregion

        void Awake()
        {
            _instance = this;
            _player = GameObject.FindGameObjectWithTag("Player");
            transAnim = this.transform.GetComponent<Animator>();
            effectPoses = GameObject.FindGameObjectsWithTag("SkillPos");
        }
         void Start()
         {
             PlayerInfo.Instance.OnPlayerInfoChanged += this.OnPlayerUpgrade;
             AudioManager.Instacne.PlayAudioClip(AudioSourceType._EffectAudioSource, "LoadPlayer");
             CreatEffect("LoadPlayer");
             GlobalParametersManager.CurrentActionFinshed = true; ;
           
        }
         #region 响应外部事件
        /// <summary>
        /// 升级事件
        /// </summary>
        /// <param name="type"></param>
         void OnPlayerUpgrade(PlayerInfoType type)
         {
             if (type == PlayerInfoType.Upgrade)
             {
                 AudioManager.Instacne.PlayAudioClip(AudioSourceType._EffectAudioSource, "LevelUp");
                 CreatEffect("LevelUp");
             }
         }
         #endregion

         #region 内部自身方法
         /// <summary>
         /// 播放运动动画
         /// </summary>
         /// <param name="type">动画类型</param>
         public void PlayAction(ActionType type)
         {
           // Debug.Log("切换到" + type.ToString() + "状态");
            switch(type)
            {
                case ActionType.Idle://返回等待状态        
                    transAnim.SetBool(GlobalParametersManager.CurrentActionTYPE.ToString(),false);//切换动画
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Idle;
                    break;
                case ActionType.Run://其他运动状态
                    transAnim.SetBool(type.ToString(),true);//切换动画
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Run;
                    break;
                case ActionType.Basic:
                    StopAllCoroutines();
                    //需要注视敌人：代码未写
                    transAnim.SetTrigger("Basic");
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Basic;
                    StartCoroutine(PlayIdle(0.8125f));
                    break;
                case ActionType.Skill01:
                    GlobalParametersManager.CurrentActionFinshed = false;
                    transAnim.SetBool(type.ToString(), true);
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Skill01;
                    break;
                case ActionType.Skill02:
                    GlobalParametersManager.CurrentActionFinshed = false;
                    transAnim.SetBool(type.ToString(), true);
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Skill02;
                    break;
                case ActionType.Skill03:
                    GlobalParametersManager.CurrentActionFinshed = false;
                    transAnim.SetBool(type.ToString(), true);
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Skill03;
                    break;
                case ActionType.Death:
                    GlobalParametersManager.CurrentActionFinshed = false;
                    transAnim.SetBool(GlobalParametersManager.CurrentActionTYPE.ToString(),false);
                    transAnim.SetTrigger(type.ToString());
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Death;
                    break;
                case ActionType.Hurt:
                    transAnim.SetTrigger("Hurt");
                    if (GlobalParametersManager.CurrentActionTYPE.ToString().Contains("Skill"))
                    {
                        GlobalParametersManager.CurrentActionFinshed = true;
                        transAnim.SetBool(GlobalParametersManager.CurrentActionTYPE.ToString(), false);
                    }
                    GlobalParametersManager.CurrentActionTYPE = ActionType.Idle;
                    break;
            }

         }
        /// <summary>
        /// 在游戏的玩家prefeb上的动画里的Event事件中注册上这个方法Attack
        //用这个方法去并输入上面策划的需要的字符串 string args 为参数字符串的集合
        /// </summary>
        /// <param name="args"></param>
         void OnAniClipFinished()
         {
             if (GlobalParametersManager.CurrentActionTYPE == ActionType.Death)
             {
                 transAnim.SetBool(ActionType.Death.ToString(),false);
             }
             else
             {
                 PlayAction(ActionType.Idle);
             }
         }
         void OnAniClipEnd()
         {
             GlobalParametersManager.CurrentActionFinshed = true;
         }
         IEnumerator PlayIdle(float time)
         {
             yield return new WaitForSeconds(time);
             PlayAction(ActionType.Idle);
         }
        //玩家受伤后的反应反应
        //受到攻击后的动画
        //浮空和后退
        //出血的特效播放
        void TakeDamage(string args)
        {
            if (isDeath) return;
            // Combo._instance.ComboPlus();玩家UI显示连击
            string[] proArray = args.Split(',');
            //脚本所在敌人播放当前动画
            if (Random.Range(0, 5) == 1)
            {
                PlayAction(ActionType.Hurt);
                AudioManager.Instacne.PlayAudioClip(AudioSourceType._EffectAudioSource, "Hurt");
                }
            //浮空和后退的控制
            float backDistance = float.Parse(proArray[1]);
            float jumpHeight = float.Parse(proArray[2]);
            //玩家被boss击飞的效果
            //iTween.MoveBy(this.gameObject, transform.InverseTransformDirection(_player.transform.forward) * backDistance + Vector3.up * jumpHeight, 0.2f);
            //出血的特效实例化           
            //读取玩家伤害值
            int damage = int.Parse(proArray[0]);

             PlayerInfo.Instance.SubHP(damage);
             if (PlayerInfo.Instance.PlayerPropertyCurrent.HP <= 0) Dead();
            //UI血条变化
            //hpBarSlider.value = (float)hp / (float)totalHP;
            // hudText.Add("-" + damage, Color.red, 0.15f);//伤害值显示为红色，时间停留0.15s
        }
        void Dead()
        {
            isDeath = true;
            PlayAction(ActionType.Death);
        }
        /// <summary>
        /// 加载特效并创建到其父物体下
        /// </summary>
        /// <param name="effectName"></param>
         void CreatEffect(string effectName)
        {
            string path = "_Prefabs/_Effect/" + effectName;
            //GameObject effect = ResourcesManager.GetInstance().CreateGameObject(path, true); 这里现在不直接实例化加载 换成直接从池子里去
            GameObject effectPrefab = ResourcesManager.GetInstance().Load<GameObject>(path, true); //在缓存中放入预设 下次查找会更快
            foreach (GameObject go in effectPoses)
            {
                if (go.name.Contains(effectName))
                {
                    //通过池管理管理特效
                    Vector3 showPos=new Vector3(0, 0, 0);
                    Quaternion showRota=Quaternion.identity;
                    //从池子里取
                    GameObject effect = PoolManager.poolMngDic["ParticalsPool"].GetGameObjectByPools(effectPrefab,showPos,showRota);
                    
                    effect.transform.parent = go.transform;
                    effect.transform.localPosition = new Vector3(0, 0, 0);
                    effect.transform.localRotation = Quaternion.identity;
                    effect.transform.localScale = new Vector3(1,1,1);
                }

            }
            //if (effect != null)
            //{
            //    Destroy(effect, 2f);
            //}//特效的销毁交给池子管理
        }
        //0 传递的技能类型 normal  skill1 skill2 skill3
        //1 执行哪个特效  effectname
        //2 播放哪个技能声音 soundname
        //3 特效释放后的位移 moveforward
        //4 跳跃的高度 jumpheight 对敌人造成浮空效果
        //在游戏的玩家攻击prefeb上的动画里的Event事件中注册上这个方法Attack
        //用这个方法去并输入上面策划的需要的字符串 
        //Event添加 技能类型,特效名,声音名,位移，浮空
        void Attack(string args)
        {
            string[] proArray = args.Split(',');
            //0 传递的技能类型
            string posType = proArray[0];
            //1 执行哪个特效  effectname
            string effectName = proArray[1];
            CreatEffect(effectName);
            //2 播放哪个技能声音 soundname
            string soundName = proArray[2];
            AudioManager.Instacne.PlayAudioClip(AudioSourceType._EffectAudioSource,soundName);
            //3 特效释放后的位移 moveforward
            float moveForward = float.Parse(proArray[3]);
            if (moveForward > 0.05f)
            {
                iTween.MoveBy(this.gameObject, Vector3.forward * moveForward, 0.3f);//玩家随技能的移动效果
            }
            switch (posType)
            {
                case "Basic":
                    SendMessageToEnemys(AttackRangeType.Forward, proArray);
                    break;
                case "Skill01":
                    SendMessageToEnemys(AttackRangeType.Around, proArray);
                    break;
                case "Skill02":
                    SendMessageToEnemys(AttackRangeType.Forward, proArray);
                    break;
                case "Skill03":
                    SendMessageToEnemys(AttackRangeType.Forward, proArray);
                    break;
            }

        }
        /// <summary>
        /// 释放技能时 向敌方可攻击怪发送消息 对方接受消息后回应
        /// </summary>
        void SendMessageToEnemys(AttackRangeType type,string[] proArray)
        {
            switch (GlobalParametersManager.CurrentActionTYPE)
            {
                case ActionType.Basic:
                    _distanceAttack = 0.2f;//暂定数据
                    _playerDamage = 59;//暂定数据
                    AttackMessage(proArray,type, _distanceAttack, _playerDamage);
                    //直线攻击
                    break;
                case ActionType.Skill01:  
                     _distanceAttack = 0.5f;//暂定数据
                     _playerDamage = 110;//暂定数据
                     AttackMessage(proArray, type,_distanceAttack, _playerDamage);
                    //直线攻击
                    break;
                case ActionType.Skill02:
                    //直线攻击
                     _distanceAttack = 0.5f;//暂定数据
                     _playerDamage = 130;//暂定数据
                     AttackMessage(proArray,type, _distanceAttack, _playerDamage);
                     break;
                case ActionType.Skill03:
                    //直线攻击
                     _distanceAttack =0.6f;//暂定数据
                     _playerDamage =100;//暂定数据
                     AttackMessage(proArray, type,_distanceAttack, _playerDamage);
                    break;
            }
        }
        void AttackMessage(string[] proArray, AttackRangeType type, float distanceAttack, float playerDamage)
        {
             foreach (GameObject go in  GetEnemyInAttackRange(type,_distanceAttack))
            {
                Debug.Log(go.name);
                //向可攻击范围内敌人发送一个攻击消息
                go.SendMessage("TakeDamage", _playerDamage + "," + proArray[3] + "," + proArray[4]);
                //TODO 参数待定
            }
        }
        /// <summary>
        /// 得到攻击范围之内的敌人
        /// </summary>
        /// <param name="attackRange">攻击范围类型</param>
        /// <param name="distanceAttack">攻击距离</param>
        /// <param name="isBossExsit">当前是否存在boss</param>
        /// <returns></returns>
        ArrayList GetEnemyInAttackRange(AttackRangeType type,float distanceAttack,bool isBossExsit=true)
        {
            //获取到所有可攻击怪列表
            ArrayList enemyInAttackRangeList = new ArrayList();//可攻击敌人列表    
            if (isBossExsit)//存在boss的时候
            {
                CalculateEnemysCanAttack(enemyInAttackRangeList, Controller_BossBase.Instance.GetBossList(), distanceAttack);
            }
            CalculateEnemysCanAttack(enemyInAttackRangeList, Controller_EnemyBase.Instance.GetEnemyList(), distanceAttack);           
            //判定攻击范围
            ArrayList enemyInForwardAttackRangeList = new ArrayList();//可攻击敌人列表   
            if (type == AttackRangeType.Forward)//只攻击一定范围内的正前方
            {
                foreach (GameObject enemyItem in enemyInAttackRangeList)
                {
                    //定义"主角与敌人"的方向"enemyItem"表示敌人的空间点减去主角的空间位置的点"normalized"数值归1化
                    Vector3 dir = (enemyItem.transform.position - this.gameObject.transform.position).normalized;
                    //定义"主角与敌人"的夹角（用向量"点乘"进行计算）
                    float floDirection = Vector3.Dot(dir, this.gameObject.transform.forward);//计算夹角
                    //如果主角与敌人在同一个方向，且在有效攻击范围内，则返回此类敌人
                    //"Dot"求夹角如果角度大于0敌人在主角的视野范围内(正面)并且距离在有效范围才能
                    if (floDirection > 0 && floDirection < 30)//前方扇形区域60度类怪可攻击
                    {
                        enemyInForwardAttackRangeList.Add(enemyItem);
                    }
                }
                return enemyInForwardAttackRangeList;//前方可攻击怪
            }
            else
            {
                return enemyInAttackRangeList;//四周全范围可攻击怪
            }
            
        }
        /// <summary>
        /// 计算对方列表哪些怪可以攻击，并返回
        /// </summary>
        /// <param name="enemyInAttackRangeList">返回列表</param>
        /// <param name="enemys">怪物列表</param>
        /// <param name="distanceAttack">攻击距离</param>
        /// <returns></returns>
        ArrayList CalculateEnemysCanAttack(ArrayList enemyInAttackRangeList, List<GameObject> enemys, float distanceAttack)
        {
            foreach (GameObject go in enemys)
            {
                //将目标世界坐标转换自身为中心的范围坐标
                Vector3 pos = transform.InverseTransformPoint(go.transform.position);
                //获得自身到敌人的直线距离
                float distance = Vector3.Distance(Vector3.zero, pos);
                if (distance < distanceAttack)
                {
                    enemyInAttackRangeList.Add(go);
                }
            }
            return enemyInAttackRangeList;
        }
        /// <summary>
        /// 自动旋转注视最近的敌人
        /// </summary>
        /// <param name="list"></param>
        public void LookAtNearEnemy(ArrayList list)
        {
            if (list.Count <= 0) return;
            foreach(GameObject enemyItem in list)
            {
                if (enemyItem.tag == "Boss")
                {
                    //四元素 旋转注视Boss
                    this.transform.rotation =
                        Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation
                                (new Vector3(enemyItem.transform.position.x, 0, enemyItem.transform.position.z) -
                                new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)), 10f
                                    );
                }
                else 
                {
                    //四元素 随机注视一个
                    this.transform.rotation =
                        Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation
                                (new Vector3(enemyItem.transform.position.x, 0, enemyItem.transform.position.z) -
                                new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)), 10f
                                    );
                    break;
                }
            }
        }
        #endregion
    }

}
