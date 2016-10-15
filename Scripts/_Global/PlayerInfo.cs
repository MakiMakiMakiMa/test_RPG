using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model;
namespace Global
{
    /// <summary>
    /// 玩家信息
    /// </summary>
    public class PlayerInfo : MonoBehaviour
    {
        private static PlayerInfo _instance;
        /// <summary>
        /// 玩家信息单例
        /// </summary>
        public static PlayerInfo Instance
        {

            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("_PlayerInfo").AddComponent<PlayerInfo>();
                }
                return PlayerInfo._instance;
            }
        }
        private PlayerProperty _playerPropertyCurrent=new PlayerProperty();
        private Equip _equipCurrent=new Equip();
        /// <summary>
        /// 玩家身上装备
        /// </summary>
        public Equip EquipCurrent
        {
            get { return _equipCurrent; }
        }
        /// <summary>
        /// 玩家当前属性
        /// </summary>
        public PlayerProperty PlayerPropertyCurrent
        {
            get { return _playerPropertyCurrent; }
        }
        public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;//定义一个事件（委托变量）
        void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        void Start()
        {
            //读取本地数据保存：如果本地读取数据不存在便重新创建一个实例
            InitPlayerData(new PlayerProperty());

        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                //测试用
                SubHP(100);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                //测试用
                PlusHP(100); 
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                //测试用
                SubMP(50);
            }
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                //测试用
                PlusMP(50);

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //测试用
                PlayerInfoRulesTool.Instance.ExpPlus(500);
                OnPlayerInfoChanged(PlayerInfoType.All);
            }
        }
        /// <summary>
        /// 初始化玩家信息
        /// </summary>
        public  void InitPlayerData(PlayerProperty role) //
        {
            _playerPropertyCurrent.Name = role.Name;
            if (role.PlayerTYPE ==PlayerType.Warrior)
            {
                _playerPropertyCurrent.HeadPortrait = "头像底板男性";
            }
            if (role.PlayerTYPE == PlayerType.Female)
            {
                _playerPropertyCurrent.HeadPortrait = "头像底板女性";
                _playerPropertyCurrent.PlayerTYPE = PlayerType.Female;
            }
            _playerPropertyCurrent.Level = role.Level;
            _playerPropertyCurrent.Exp = role.Exp;
            _playerPropertyCurrent.Coin = role.Coin;
           _playerPropertyCurrent.Diamond = role.Diamond;
           _playerPropertyCurrent.Energy = role.Energy;
            _playerPropertyCurrent.Toughen = role.Toughen;
            _playerPropertyCurrent.VIP = role.VIP;
            //补充属性：角色基础血量=角色等级*100  基础伤害=角色等级*100  基础战力=角色基础血量+基础伤害
            _playerPropertyCurrent.HP = role.HP;
            _playerPropertyCurrent.Damage = _playerPropertyCurrent.Level* 50;//暂定
            _playerPropertyCurrent.Power = _playerPropertyCurrent.HP + _playerPropertyCurrent.Damage;//暂定
            ////装备属性
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        /// <summary>
        /// 发出所用信息的广播
        /// </summary>
        public void  GetPlayerInfo(PlayerInfoType type )
        {
            ////装备属性
            OnPlayerInfoChanged(type);
        }
        #region 生命数值的操作方式
        //生命值公式=玩家基础血量+装备增加血量
        //玩家基础血量=玩家等级*1000
        /// <summary>
        /// 血量值减少（技能释放）
        /// </summary>
        /// <param name="value"></param>
        public void SubHP(int HP_sub)
        {
            if (PlayerPropertyCurrent.HP <= 0)
            {
                //提示:玩家已经挂了
                return;
            }
            else
            {
                PlayerPropertyCurrent.HP -= HP_sub;
                if (PlayerPropertyCurrent.HP <= 0) PlayerPropertyCurrent.HP = 0;
                 OnPlayerInfoChanged(PlayerInfoType.All);
            }
            UpdateATKValues(0);
            UpdateDEFValues(0);
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        /// <summary>
        /// 增加血量值
        /// </summary>
        /// <param name="MP_plus"></param>
        public void PlusHP(int HP_plus)
        {
            if (PlayerPropertyCurrent.HPMax < PlayerPropertyCurrent.HP + HP_plus)
            {
                Debug.Log("PlayerPropertyCurrent.HPMax=" + PlayerPropertyCurrent.HP);
                //血量增加值不能大于最大魔法量
                PlayerPropertyCurrent.HP = PlayerPropertyCurrent.HPMax;
            }
            else
            {
                PlayerPropertyCurrent.HP = PlayerPropertyCurrent.HP + HP_plus;
            }

            UpdateATKValues(0);
            UpdateDEFValues(0);
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        /// <summary>
        /// 获取当前魔法包
        /// </summary>
        /// <returns></returns>
        public int GetCurrentHP()
        {
            return PlayerPropertyCurrent.HP;
        }
        /// <summary>
        /// 增加最大生命值
        /// </summary>
        /// <param name="MP_plus"></param>
        public void PlusHPMax(int HP_plus)
        {
            PlayerPropertyCurrent.HPMax += HP_plus;
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        #endregion
        #region 魔法数值的操作方式
        //魔法值公式=玩家基础魔法+装备增加魔法
        //玩家基础血量=玩家等级*100
        /// <summary>
        /// 魔法值减少（技能释放）
        /// </summary>
        /// <param name="value"></param>
        public void SubMP(int MP_sub)
        {
            if (MP_sub > PlayerPropertyCurrent.MP || PlayerPropertyCurrent.MP <= 0)
            {
                //提示:魔法值不足
            }
            else
            {
                PlayerPropertyCurrent.MP -= MP_sub;
                OnPlayerInfoChanged(PlayerInfoType.All);
            }
            UpdateATKValues(0);
            UpdateDEFValues(0);
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        /// <summary>
        /// 增加魔法值
        /// </summary>
        /// <param name="MP_plus"></param>
        public void PlusMP(int MP_plus)
        {
            if (PlayerPropertyCurrent.MPMax <PlayerPropertyCurrent.MP+MP_plus )
            {
                Debug.Log(" PlayerPropertyCurrent.MPMax" + PlayerPropertyCurrent.MP);
                //魔法增加值不能大于最大魔法量
                PlayerPropertyCurrent.MP = PlayerPropertyCurrent.MPMax;
                return;
            }
            else
            {
                PlayerPropertyCurrent.MP = PlayerPropertyCurrent.MP + MP_plus;
                OnPlayerInfoChanged(PlayerInfoType.All);
            }
            UpdateATKValues(0);
            UpdateDEFValues(0);
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        /// <summary>
        /// 获取当前魔法包
        /// </summary>
        /// <returns></returns>
        public int GetCurrentMP()
        {
            return PlayerPropertyCurrent.MP;
        }
        /// <summary>
        /// 增加最大魔法值
        /// </summary>
        /// <param name="MP_plus"></param>
        public void PlusMPMax(int MP_plus)
        {
           PlayerPropertyCurrent.MPMax+=MP_plus;
           OnPlayerInfoChanged(PlayerInfoType.All);
        }
        #endregion
        #region 攻击力ATK数值的操作方式
        //攻击力公式=玩家基础攻击力+【装备增加攻击力】
        //玩家基础攻击力=_AttackForce=MaxATK/2*(_Health/MaxHealth)
        /// <summary>
        /// 更新攻击力（当主角气血改变,获得新装备时）
        /// </summary>
        /// <param name="atkValue_weapon"></param>
        public void UpdateATKValues(int atkValue_weapon)
        {
            int atk_update = 0;
            if (atkValue_weapon == 0)//装备未发生变更
            {
                atk_update = (int)((float)PlayerPropertyCurrent.AttackMax / 2 * ((float)PlayerPropertyCurrent.HP / (float)PlayerPropertyCurrent.HPMax)) + atkValue_weapon;

            }
            else if (atkValue_weapon> 0)//装备发生了变更
            {
                atk_update = (int)((float)PlayerPropertyCurrent.AttackMax / 2 * ((float)PlayerPropertyCurrent.HP / (float)PlayerPropertyCurrent.HPMax)) + atkValue_weapon;
                //其他改变
            }
            //数值有效性验证
            //当前真实攻击力大于最大攻击力时
            if (atk_update > PlayerPropertyCurrent.AttackMax)
            {
                PlayerPropertyCurrent.Attack = PlayerPropertyCurrent.AttackMax;
            }
            else
            {
                PlayerPropertyCurrent.Attack =atk_update;
            }
        }
        /// <summary>
        /// 获得当前攻击力
        /// </summary>
        /// <returns></returns>
        public int GetCurrentATK()
        {
            return PlayerPropertyCurrent.Attack;
        }
        /// <summary>
        /// 增加最大攻击力(等级提升)
        /// </summary>
        /// <param name="atk_plus"></param>
        public void PlusATKMax(int atk_plus)
        { 
            //当前等级伤害值限定为最大攻击力
            PlayerPropertyCurrent.AttackMax += atk_plus;
        }
        #endregion
        #region 防御力DEF数值的操作方式
        //防御力公式=玩家基础防御力+【装备增加防御力】
        //玩家基础防御力DEFBase=MaxDEF/2*(_Health/MaxHealth)
        /// <summary>
        /// 更新基础防御力（当主角气血改变,脱掉新装备时）
        /// </summary>
        /// <param name="atkValue_weapon"></param>
        public void UpdateDEFValues(int defValue_weapon)
        {
            int def_update = 0;
            if (defValue_weapon == 0)//装备未发生变更
            {
                def_update = (int)((float)PlayerPropertyCurrent.DefenceMax / 2 * ((float)PlayerPropertyCurrent.HP / (float)PlayerPropertyCurrent.HPMax)) + defValue_weapon;

            }
            else if (defValue_weapon > 0)//装备发生了变更
            {
                def_update = (int)((float)PlayerPropertyCurrent.AttackMax / 2 * ((float)PlayerPropertyCurrent.HP / (float)PlayerPropertyCurrent.HPMax)) +defValue_weapon;
                //其他改变
            }
            //数值有效性验证
            //当前真实基础防御力大于最大基础防御力时
            if (def_update > PlayerPropertyCurrent.AttackMax)
            {
                PlayerPropertyCurrent.Defence= PlayerPropertyCurrent.AttackMax;
            }
            else
            {
                PlayerPropertyCurrent.Defence = def_update;
            }
        }
        /// <summary>
        /// 获得当前基础防御力
        /// </summary>
        /// <returns></returns>
        public int GetCurrentDEF()
        {
            return PlayerPropertyCurrent.Defence;
        }
        /// <summary>
        /// 增加最大基础防御力(等级提升)
        /// </summary>
        /// <param name="atk_plus"></param>
        public void PlusDEFMax(int def_plus)
        {
            //当前等级伤害值限定为最大防御力
            PlayerPropertyCurrent.DefenceMax += def_plus;
        }
        #endregion
        #region 敏捷力DEX数值的操作方式
        /// <summary>
        /// 增加最大魔法值
        /// </summary>
        /// <param name="MP_plus"></param>
        public void PlusDEXMax(int DEX_plus)
        {
            PlayerPropertyCurrent.DexterityMax += DEX_plus;
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        #endregion
        #region 暴击率数值的操作方式
        /// <summary>
        /// 增加最大暴击率
        /// </summary>
        /// <param name="MP_plus"></param>
        public void PlusCTRMax(float CTR_plus)
        {
            PlayerPropertyCurrent.CriticalTriggerRateMax += CTR_plus;
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        #endregion
        #region 暴击伤害率数值的操作方式
        /// <summary>
        /// 增加最大暴击伤害率
        /// </summary>
        /// <param name="MP_plus"></param>
        public void PlusCARMax(float CAR_plus)
        {
            PlayerPropertyCurrent.CriticalAttackMax += CAR_plus;
            OnPlayerInfoChanged(PlayerInfoType.All);
        }
        #endregion


  

        #region 伤害数值的操作方式
        //玩家伤害=？(玩家攻击力+玩家buff增加攻击力)*玩家暴击伤害率：(玩家攻击力+玩家buff增加攻击力)
        //玩家实际伤害=玩家伤害-敌方防御力
        #endregion
        #region 装备属性的操作方法
        /// <summary>
        ///穿上装备时,更新装备属性
        /// </summary> 
        /// <param name="item"></param>
        public void EquipDressOn(InventoryItem item, bool isSync = true)
        {
            OnDerssedEquip(item);
        }
        /// <summary>
        /// 脱掉装备是,更新装备属性
        /// </summary>
        /// <param name="item"></param>
        public void EquipDressOff(InventoryItem item)
        {     
            //人物面板装备需要存放脱下的装备
           // InventoryUI._instance.AddInventoryItem(equipDressed);
            OnTakeOffEquip(item);
            OnPlayerInfoChanged(PlayerInfoType.Equip);
        }
        /// <summary>
        ///装备升级时,更新装备的属性
        /// </summary> 
        /// <param name="item"></param>
        public void UpdateRoleEquip(InventoryItem item)
        {
            OnPlayerInfoChanged(PlayerInfoType.Equip);
        }
        /// <summary>
        /// 穿上装备后属性改变
        /// </summary>
        /// <param name="item"></param>
        public void OnDerssedEquip(InventoryItem item)
        {
            this.PlusHPMax(item.Inventory.HP);
            this.PlusMPMax(item.Inventory.MP);
            this.PlusATKMax(item.Inventory.ATK);
            this.PlusDEFMax(item.Inventory.DEF);
            this.PlusDEXMax(item.Inventory.DEX);
            this.PlusCTRMax((float)item.Inventory.CTX / (float)100);
            this.PlusCARMax((float)item.Inventory.CTK / (float)100);


            this.PlusHP(_playerPropertyCurrent.HPMax);
            this.PlusMP(_playerPropertyCurrent.MPMax);

        }
        /// <summary>
        /// 脱掉装备时,属性的改变
        /// </summary>
        /// <param name="item"></param>
        public void OnTakeOffEquip(InventoryItem item)
        {
            this.PlusHPMax(-item.Inventory.HP);
            this.PlusMPMax(-item.Inventory.MP);
            this.PlusATKMax(-item.Inventory.ATK);
            this.PlusDEFMax(-item.Inventory.DEF);
            this.PlusDEXMax(-item.Inventory.DEX);
            this.PlusCTRMax(-(float)item.Inventory.CTX / (float)100);
            this.PlusCARMax(-(float)item.Inventory.CTK / (float)100);

            this.PlusHP(_playerPropertyCurrent.HPMax);
            this.PlusMP(_playerPropertyCurrent.MPMax);
        }
        /// <summary>
        /// 装备升级时,属性的更新
        /// </summary>
        /// <param name="item"></param>
        public void OnUpdateEquip(InventoryItem item)
        {

        }
        #endregion
        #region 玩家信息属性值改变的调用方法
        /// <summary>
        /// 改变任务昵称
        /// </summary>
        /// <param name="name"></param>
        public void NameChanged(string name)
        {
            _playerPropertyCurrent.Name = name;
            OnPlayerInfoChanged(PlayerInfoType.Name);
        }
      
        /// <summary>
        /// 获得金币
        /// </summary>
        /// <param name="coin"></param>
        public void GetCoin(int coin)
        {
            _playerPropertyCurrent.Coin += coin;
            OnPlayerInfoChanged(PlayerInfoType.Coin);
        }
        /// <summary>
        /// 消耗金币
        /// </summary>
        /// <param name="coin"></param>
        public void UseCoin(int coin)
        {
            _playerPropertyCurrent.Coin -= coin;
            OnPlayerInfoChanged(PlayerInfoType.Coin);
        }
        /// <summary>
        /// 得到钻石
        /// </summary>
        /// <param name="diamond"></param>
        public void GetDiamond(int diamond)
        {
            _playerPropertyCurrent.Diamond += diamond;
            OnPlayerInfoChanged(PlayerInfoType.Diamond);
        }
        /// <summary>
        /// 消耗钻石
        /// </summary>
        /// <param name="diamond"></param>
        public void UseDiamond(int diamond)
        {
            _playerPropertyCurrent.Diamond -= diamond;
            OnPlayerInfoChanged(PlayerInfoType.Diamond);
        }
        /// <summary>
        /// 使用药品时,体力增加
        /// </summary>
        /// <param name="energy"></param>
        public void PlusEnergy(int energy)
        {
            if (_playerPropertyCurrent.Energy >= 100)
            {
                _playerPropertyCurrent.Energy = 100;
                return;
            }
            _playerPropertyCurrent.Energy += energy;
            OnPlayerInfoChanged(PlayerInfoType.Energy);
        }
        /// <summary>
        /// 打副本时,体力减少
        /// </summary>
        /// <param name="energy"></param>
        public void UseEnergy(int energy)
        {        
            if (_playerPropertyCurrent.Energy <=0)
            {
                _playerPropertyCurrent.Energy =0;
                return;
            }
            _playerPropertyCurrent.Energy -= energy;
            OnPlayerInfoChanged(PlayerInfoType.Energy);
        }
        #endregion

    }
   
}