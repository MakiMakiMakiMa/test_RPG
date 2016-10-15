using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour
{
    //public static SkillUI _instance;
    //private Skill skill;
    //private UILabel skillNameLabel;
    //private UILabel skillDesLabel;
    //private UIButton closeButton;
    //private UIButton upGradeButton;
    //private UILabel  upGradeLabel;
    //private TweenPosition skillUITween;
    
    //void Awake()
    //{ 
    //     skillUITween=this.GetComponent<TweenPosition>();
    //     skillNameLabel=transform.Find("BGSprite/SkillNameLabel").GetComponent<UILabel>();
    //     skillDesLabel = transform.Find("BGSprite/DesLabel").GetComponent<UILabel>();
    //     closeButton=transform.Find("btn_close").GetComponent<UIButton>();
    //     upGradeButton=transform .Find ("SkillLevelUp").GetComponent<UIButton>();
    //     upGradeLabel=transform.Find("SkillLevelUp/Label").GetComponent<UILabel>();
    //     skillNameLabel.text = "";
    //     skillDesLabel.text = "";
    //     DisableUpGradeButton("选择技能");
    //     EventDelegate ed = new EventDelegate(this,"OnUPGradeButtonClick");
    //     upGradeButton.onClick.Add(ed);
    //     EventDelegate edClose = new EventDelegate(this, "HideSkillUI");
    //     closeButton.onClick.Add(edClose);
         
    //}
    //void Start()
    //{
    //    _instance = this;
    //}
    //void DisableUpGradeButton(string label = "")
    //{
    //    upGradeButton.SetState(UIButton.State.Disabled,true);
    //    upGradeButton.collider.enabled = false;
    //    if (label != "")
    //    {
    //        upGradeLabel.text = label;
    //    }
    //}
    //void EnableUpGradeButton(string label = "")
    //{
    //    upGradeButton.SetState(UIButton.State.Normal, true);
    //    upGradeButton.collider.enabled =true;
    //    if (label != "")
    //    {
    //        upGradeLabel.text = label;
    //    }
    //}
    //void OnSkillClick(Skill skill)//当技能点击时信息发生改变
    //{
    //    this.skill = skill;    
    //    skillNameLabel.text = this.skill.Name+"Lv."+this.skill.Level;
    //    skillDesLabel.text = "当前等级技能附加攻击力：" + this.skill.Level * this.skill.Damage + ",下一级技能攻击力为：" + this.skill.Damage * (this.skill.Level + 1) + "，升级到下一级所需金币为:" + (this.skill.Level + 1) * 500;    
    //    if (this.skill.Level < PlayerInfo._instance.Level&&(this.skill.Level + 1) * 500 <= PlayerInfo._instance.Coin)
    //    {
    //        EnableUpGradeButton("升级");
    //    }
    //    else if (this.skill.Level >= PlayerInfo._instance.Level)
    //    {
    //        DisableUpGradeButton("等级已达上限");
    //    }
    //   else if ((this.skill.Level + 1) * 500 > PlayerInfo._instance.Coin)
    //    {
    //        DisableUpGradeButton("金币不足");
    //    }
    //}

    // void OnUPGradeButtonClick()
    //{
    //    if (PlayerInfo._instance.Coin <= 0) return;  
    //    PlayerInfo info= PlayerInfo._instance;
    //    int needCoin = (this.skill.Level + 1) * 500;
    //    int remainCoin=info.Coin;
    //    if(needCoin<info.Coin)
    //    {
    //        info.UseCoin(needCoin);
    //    }
       
    //    //this.skill.UpGrade();
    //    OnSkillClick(skill);
    //}//还未完成
    // void IsCoinEnough()
    // { 
    // }

    // public  void HideSkillUI()
    //{
    //    skillUITween.PlayReverse();
    //}
    //public  void ShowSkillUI()
    //{
    //    skillUITween.PlayForward();
    //}


}
