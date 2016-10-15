using UnityEngine;
using System.Collections;
using Global;
using Model;
using Kernal;
namespace Controller
{
    public class Controller_EnemyAction : MonoBehaviour
    {
        private EnemyProperty _enemyInfo = new EnemyProperty();

        public EnemyProperty EnemyInfo
        {
            get { return _enemyInfo; }
            set { _enemyInfo = value; }
        }
        //private static Controller_EnemyAction _instance;
        ///// <summary>
        ///// 单例
        ///// </summary>
        //public static Controller_EnemyAction Instance
        //{
        //    get { return Controller_EnemyAction._instance; }
        //    set { _instance = value; }
        //}
        #region 成员变量
        private GameObject _player;
        private bool _isDeath = false;
        private bool _isCanAttack = false;
        public bool IsCanAttack
        {
            get { return _isCanAttack; }
            set { _isCanAttack = value; }
        }
        private bool _isOnAttackState = false;
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
        }
        void Start()
        {
            InitEnemyInfo();
        }
        /// <summary>
        /// 初始化bossInfo
        /// </summary>
        void InitEnemyInfo()
        {

            foreach (string item in EnemyPropertyManager.Instance.enemyInfoDic.Keys)
            {
                Debug.Log(item);
                if (this.gameObject.name.Contains(item))
                {
                    EnemyInfo = EnemyPropertyManager.Instance.enemyInfoDic[item];
                   
                }
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
            transform.GetComponent<Animation>().CrossFade(EnemyActionType.Wound.ToString(),0.1f);
            //AudioManager.Instacne.PlayAudioClip(AudioSourceType._JsActionAS, "EnemyHurt");//建议每个怪自身挂个播放器
            //transform.GetComponent<AudioSource>().clip = ResourcesManager.GetInstance().Load<AudioClip>("_Audio/Skill/EnemyHurt", true);
            //transform.GetComponent<AudioSource>().Play();
            //浮空和后退的控制
            float backDistance = float.Parse(proArray[1]);
            float jumpHeight = float.Parse(proArray[2]);
            //用iTween脚本来控制敌人的位置变换：浮空和后退
            iTween.MoveBy(this.gameObject, transform.InverseTransformDirection(_player.transform.forward) * backDistance + Vector3.up * jumpHeight, 0.2f);
            //出血的特效实例化           
            //读取玩家伤害值
            int damage = int.Parse(proArray[0]);
            EnemyInfo.HP -= damage;
            Debug.Log(EnemyInfo.HP);
            //UI血条变化
            //hpBarSlider.value = (float)hp / (float)totalHP;
           // hudText.Add("-" + damage, Color.red, 0.15f);//伤害值显示为红色，时间停留0.15s
            if (EnemyInfo.HP <= 0)
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
            transform.GetComponent<Animation>().CrossFade(EnemyActionType.Death.ToString());
            //玩家获得经验值
            PlayerInfoRulesTool.Instance.ExpPlus(500);
            //爆出装备和物品
            
            //死亡后回归池子
            //Controller_EnemyBase.Instance.GetEnemyList().Remove(this.gameObject);
            //Controller_EnemyBase.Instance.SpawnCreateEnemys();
            //Controller_EnemyBase.Instance.Findenemy();
            //PoolManager.poolMngDic["EnemysPool"].DestoryParticalCloneObj(this.gameObject);
            //等待30s后原地再刷新一个怪

        }//怪物受到伤害后的处理
        /// <summary>
        /// 查找玩家
        /// </summary>
        /// <returns></returns>
        public  bool IsFindPlayer()
        {
            if (IsDeath) return false;//死亡后不做回应
            // Debug.Log(this._hp);
            //将目标世界坐标转换自身为中心的范围坐标
            Vector3 pos = transform.InverseTransformPoint(_player.transform.position);
            float distance = Vector3.Distance(Vector3.zero, pos);
            //Debug.Log(distance);
            if (distance < EnemyInfo.CanFindPlayer)
            {
                if (distance > EnemyInfo.CanAttackplayer)//离玩家5米的时候停止
                {
                    //可攻击
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
        /// 小怪智能
        /// </summary>
        void EnemyAI()
        {   
            if(IsDeath)
            {
                StopAllCoroutines();
                _isOnAttackState = false;
                return;
            }
            if (IsCanAttack == false)
            {
                StopAllCoroutines();
                _isOnAttackState = false;
            }
            if (IsCanAttack&&_isOnAttackState==false&&IsDeath==false)
            {
                _isOnAttackState = true;
                StartCoroutine(EnemyAttack());
            }

        }
        IEnumerator EnemyAttack()
        {
            this.GetComponent<Animation>().Play(EnemyActionType.Attack3.ToString());
            yield return new WaitForSeconds(1f);//玩家受伤
            _player.SendMessage("TakeDamage",EnemyInfo.Attack+ "," + 0+ "," + 0);
            yield return new WaitForSeconds(0.5f);//整个动画完成

            this.GetComponent<Animation>().Play(EnemyActionType.Attack2.ToString());
            yield return new WaitForSeconds(1f);//玩家受伤
            _player.SendMessage("TakeDamage", EnemyInfo.Attack + "," + 0 + "," + 0);
            yield return new WaitForSeconds(0.5f);//整个动画完成

            this.GetComponent<Animation>().Play(EnemyActionType.Attack1.ToString());
            yield return new WaitForSeconds(0.8f);//玩家受伤
            _player.SendMessage("TakeDamage", EnemyInfo.Attack + "," + 0 + "," + 0);
            yield return new WaitForSeconds(0.7f);//整个动画完成
            StartCoroutine(EnemyAttack());
        }
        void Update()
        {
            EnemyAI();
        }
    }
}