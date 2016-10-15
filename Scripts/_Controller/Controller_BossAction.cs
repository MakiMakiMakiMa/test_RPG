using UnityEngine;
using System.Collections;
using Global;
using Model;
namespace Controller
{
    public class Controller_BossAction : MonoBehaviour
    {
        private EnemyProperty _bossInfo=new EnemyProperty();

        public  EnemyProperty BossInfo
        {
            get { return _bossInfo; }
            set { _bossInfo = value; }
        }

        //private static Controller_BossAction _instance;
        ///// <summary>
        ///// 这里无法使用单例 因为会同时出现很多boss或者小怪 单例就只会有一个怪或者boss做出反应
        ///// </summary>
        //public static Controller_BossAction Instance
        //{
        //    get { return Controller_BossAction._instance; }
        //    set { _instance = value; }
        //}
        #region 定义成员变量
        public EnemyActionType CurrentActionType { get; set; }
        private GameObject _player;
        private Animator transAnimBoss;//状态机
        private float timer = 10f;
        private bool _isDeath=false;
        private bool _isCanAttack = false;
        private int bossAttackCount = 0;
        public bool IsCanAttack
        {
            get { return _isCanAttack; }
            set { _isCanAttack = value; }
        }       
        public bool PrevisousAniIsEnd { get; set; }
        public bool IsDeath
        {
            get
            {
                return _isDeath;
            }
            set
            {
                _isDeath = value;
            }
        }
        #endregion
        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            transAnimBoss = this.transform.GetComponent<Animator>();
        }
        void Start()
        {
            CurrentActionType = EnemyActionType.Idle;
            PrevisousAniIsEnd = true;
            InitBossInfo();
            //InvokeRepeating("BossAI",1,60);
        }
        /// <summary>
        /// 初始化bossInfo
        /// </summary>
        void InitBossInfo()
        {

            foreach (string item in EnemyPropertyManager.Instance.enemyInfoDic.Keys)
            {
                Debug.Log(item);
                if (this.gameObject.name.Contains(item))
                {
                    _bossInfo = EnemyPropertyManager.Instance.enemyInfoDic[item];
                }
                else
                {
                    Debug.LogWarning(" EnemyPropertyManager的数据读取有问题");
                }
            }
        }
        /// <summary>
        /// 播放运动动画
        /// </summary>
        /// <param name="type">动画类型</param>
        public  void PlayBossAction(EnemyActionType type)
        {
            switch (type)
            {
                case EnemyActionType.Idle://返回等待状态             
                   transAnimBoss.SetBool(CurrentActionType.ToString(), false);//切换动画
                   CurrentActionType = EnemyActionType.Idle;
                    break;
                case EnemyActionType.Death:
                    transAnimBoss.SetTrigger(type.ToString());
                    CurrentActionType = type;
                    break;
                default:
                    transAnimBoss.SetBool(type.ToString(), true);//其他bool值控制的运动状态
                    CurrentActionType = type;
                    break;
            }

        }
        //玩家TakeDamage后敌人的反应
        //受到攻击后的动画
        //浮空和后退
        //出血的特效播放
        void TakeDamage(string args)
        {
            if (IsDeath) return;
            // Combo._instance.ComboPlus();玩家UI显示连击
            string[] proArray = args.Split(',');
            //脚本所在敌人播放当前动画
            transAnimBoss.SetTrigger("Hurt");
            //浮空和后退的控制
            float backDistance = float.Parse(proArray[1]);
            float jumpHeight = float.Parse(proArray[2]);
            //用iTween脚本来控制敌人的位置变换：浮空和后退
            iTween.MoveBy(this.gameObject, transform.InverseTransformDirection(_player.transform.forward) * backDistance + Vector3.up * jumpHeight, 0.2f);
            //出血的特效实例化           
            //读取玩家伤害值
            int damage = int.Parse(proArray[0]);
            _bossInfo.HP -= damage;
            Debug.Log(_bossInfo.HP);
            //UI血条变化
            //hpBarSlider.value = (float)hp / (float)totalHP;
            // hudText.Add("-" + damage, Color.red, 0.15f);//伤害值显示为红色，时间停留0.15s
            if (_bossInfo.HP <= 0)
            {
                Dead();
            }
        }
        /// <summary>
        /// 当玩家死亡时
        /// </summary>
        private void Dead()
        {
            IsDeath = true;
            //播放躺尸动画
            PlayBossAction(EnemyActionType.Death);
            //玩家获得经验值
            PlayerInfoRulesTool.Instance.ExpPlus(10500);
            //爆出装备和物品

        }//怪物受到伤害后的处理
        ///Boss攻击事件
        //0 传递的技能类型 normal  skill1 skill2 skill3
        //1 执行哪个特效  effectname
        //2 播放哪个技能声音 soundname
        //3 特效释放后的位移 moveforward
        //4 跳跃的高度 jumpheight 对敌人造成浮空效果
        //在游戏的Boss prefeb上的动画里的Event事件中注册上这个方法Attack
        //用这个方法去并输入上面策划的需要的字符串 
        //Event添加 技能类型,特效名,声音名,位移，浮空
        void Attack(string args)
        {
            string[] proArray = args.Split(',');
            //0 传递的技能类型
            string posType = proArray[0];
            //1 执行哪个特效  effectname
            string effectName = proArray[1];
            //2 播放哪个技能声音 soundname
            string soundName = proArray[2];
            //3 特效释放后的位移 moveforward
            float moveForward = float.Parse(proArray[3]);
            if (moveForward > 0.05f)
            {
                iTween.MoveBy(this.gameObject, Vector3.forward * moveForward, 0.3f);//boss跟随技能的移动效果
            }
            switch (posType)
            {
                case "Attack1":
              SendMessageToPlayer(AttackRangeType.Forward,  proArray,_bossInfo.Attack);
                    break;
                case "Attack2":
                    SendMessageToPlayer(AttackRangeType.Around, proArray, _bossInfo.Attack);
                    break;
                case "Attack3":
                    SendMessageToPlayer(AttackRangeType.Around, proArray, _bossInfo.Attack);
                    break;
            }

        }
        /// <summary>
        /// 释放技能时 向玩家发送消息 对方接受消息后回应
        /// </summary>
        void SendMessageToPlayer(AttackRangeType type, string[] proArray, float bossDamage)
        {
            AttackMessage(proArray,bossDamage);
        }
        void AttackMessage(string[] proArray, float bossDamage)
        {
                //向可攻击范围内玩家发送一个攻击消息
               _player.SendMessage("TakeDamage", bossDamage + "," + proArray[3] + "," + proArray[4]);
                //TODO 参数待定
        }
        /// <summary>
        /// 攻击范围内发现玩家的处理
        /// </summary>
        void FindPlayerInAttackRange()
        { 

        }
        /// 查找玩家
        /// </summary>
        /// <returns></returns>
        public bool IsFindPlayer()
        {
            if (IsDeath) return false;//死亡后不做回应
            // Debug.Log(this._hp);
            //将目标世界坐标转换自身为中心的范围坐标
            Vector3 pos = transform.InverseTransformPoint(_player.transform.position);
            float distance = Vector3.Distance(Vector3.zero, pos);
            if (distance < _bossInfo.CanFindPlayer)
            {
                if (distance >_bossInfo.CanAttackplayer)//离玩家5米的时候停止
                {
                    //不可攻击
                    IsCanAttack = false;
                    return true;
                }
                else
                {
                    //可攻击
                    IsCanAttack = true;
                    return false;
                    
                }
            }
            return false;
        }
        /// <summary>
        /// Boss的智能思考
        /// </summary>
        void BossAI()
        {
            if (IsDeath) return;//死亡后再不反应
            if (IsCanAttack == false) return;//不可攻击时返回
            if (PrevisousAniIsEnd == false) return;//上一个动画没完成的时候返回
            if (CurrentActionType != EnemyActionType.Idle) return;//待机状态才能攻击
            if (timer < _bossInfo.Dexterity) return;//冷却时间没到的时候返回
            //重置条件数据
            PrevisousAniIsEnd = false;
            timer = 0;
            bossAttackCount++;
            StopAllCoroutines();
            StartCoroutine(ColdTimeRefeash());//开启攻击频率冷却的携程
            //攻击:播放攻击动画
            if(bossAttackCount<=3)PlayBossAction(EnemyActionType.Attack1);
            if (bossAttackCount == 4 || bossAttackCount == 5) PlayBossAction(EnemyActionType.Attack2);
            if (bossAttackCount == 6) PlayBossAction(EnemyActionType.Attack3);
            if (bossAttackCount == 7) 
            { 
                bossAttackCount = 0; 
                PrevisousAniIsEnd = true;
            }
            //致命一击
        }
        /// <summary>
        /// 攻击频率
        /// </summary>
        /// <returns></returns>
        IEnumerator ColdTimeRefeash()
        {
            yield return new WaitForSeconds(0.01f);
            timer += 0.01f;
            StartCoroutine(ColdTimeRefeash());

        }
        /// <summary>
        /// 可攻击限制
        /// </summary>
        /// <returns></returns>
        IEnumerator FinsihPrevisousAni()
        {
            yield return new WaitForSeconds(0.01f);
            PrevisousAniIsEnd = true;
        }

        /// <summary>
        /// 在游戏的玩家prefeb上的动画里的Event事件中注册上这个方法Attack
        //用这个方法去并输入上面策划的需要的字符串 string args 为参数字符串的集合
        /// </summary>
        /// <param name="args"></param>
         void OnAniClipFinished()
        {
            this.PlayBossAction(EnemyActionType.Idle);
            StartCoroutine(FinsihPrevisousAni());

        }
        void Update()
        {
            BossAI();
        }
    }
}
