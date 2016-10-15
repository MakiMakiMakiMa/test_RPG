using UnityEngine;
using System.Collections;

namespace Global
{
    /// <summary>
    /// 全局委托
    /// </summary>
    public delegate void OnPlayerInfoChangedEvent(PlayerInfoType type);
    /// <summary>
    ///全局参数
    /// </summary>
    public class GlobalParameters
    {      
    }

    /// <summary>
    /// 场景类型
    /// </summary>
    public enum SceneType
    {
        SceneLogin,
        SceneLoading,
        SceneRole,
        SceneCity,
        SceneTran01,
        SceneBase
    }
    /// <summary>
    /// 音源类型
    /// </summary>
    public enum AudioSourceType
    {
        _BGAudioSource,     //背景音源
        _EffectAudioSource,   //特效音源
        _PlayerActionAS,    //玩家音源
        _JsActionAS        //js音源
    }
    #region 玩家信息、道具、技能枚举
    /// <summary>
    /// 人物信息改变类型
    /// </summary>
    public enum PlayerInfoType
    {
        Name,
        HeadPortrait,
        Level,
        Power,
        Exp,
        Coin,
        Diamond,
        Energy,
        Toughen,
        VIP,
        HP,
        Damage,
        Equip,
        All,
        Upgrade

    }
    public enum InventoryType
    {
        Drug,
        Equip,
        Box
    }
    public enum EquipType// 头盔 衣服 武器 鞋子 项链 手镯 戒指 翅膀
    {
        Helm,
        Cloth,
        Weapon,
        Shoes,
        Necklace,
        Bracelet,
        Ring,
        Wing,
        JadePendantLeft,
        JadePendantRight

    }
    /// <summary>
    /// 技能类类型
    /// </summary>
    public enum SkillType
    {
        Basic,//普通攻击
        Skill //技能攻击
    }
    /// <summary>
    /// 游戏界面技能按钮的位置
    /// </summary>
    public enum SkillPosType
    {
        Basic = 0, //普通攻击按钮位置
        One = 1,   //技能1按钮位置
        Two = 2,   //技能2按钮位置
        Three = 3   //技能3按钮位置
    }
    /// <summary>
    /// 玩家角色种类
    /// </summary>
    public enum PlayerType
    {
        Warrior,
        Female
    }
    /// <summary>
    /// 怪物类型
    /// </summary>
    public enum EnemyType
    { 
        Boss,
        XiaoGuai
    }
    #endregion
    /// <summary>
    /// 动画类型
    /// </summary>
    public enum ActionType 
    {
        Idle=0,
        Run=1,
        Basic,//基础攻击
        Skill01,//技能一
        Skill02,//技能二
        Skill03,//技能三
        Hurt,
        Death
    }
    /// <summary>
    /// 敌人动画类型
    /// </summary>
    public enum EnemyActionType
    {
        Idle = 0,
        Run = 1,
        Death=2,
        Fight=3,
        Walk=4,
        Hurt=5,
        Attack1,
        Attack2,
        Attack3,
        Wound
    }
    /// <summary>
    /// 攻击范围
    /// </summary>
    public enum AttackRangeType
    { 
        Forward,    //前方攻击
        Left,
        Right,
        Around  //范围攻击
    }

}
