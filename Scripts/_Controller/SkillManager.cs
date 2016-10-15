using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

public class SkillManager : MonoBehaviour
{
    //public static SkillManager _instance;
    //public TextAsset SkillInfoList;
    //private ArrayList skillList = new ArrayList();
    //public Dictionary<int, Skill> skillDic = new Dictionary<int, Skill>();

    //void Awake()
    //{
    //    _instance = this;
    //    ReadSkillInfoList();
    //}
    //void Start()
    //{
       
    //}
    ///// <summary>
    ///// 读取本地技能信息清单
    ///// </summary>
    //void ReadSkillInfoList()//读取技能列表信息，保存在ArrayList集合中  此时顺带初始化；
    //{
    //    string[] allStrArray = SkillInfoList.ToString().Split('\n');
    //    foreach(string skillStr in allStrArray)
    //    {
    //        Skill skillinfo = new Skill();
    //        string[] infoArray = skillStr.Split(',');
    //        skillinfo.ID = int.Parse(infoArray[0]);
    //        skillinfo.Name = infoArray[1];
    //        skillinfo.ICON = infoArray[2];
    //        switch (infoArray[3])
    //        { 
    //            case "Warrior":
    //                skillinfo.PlayerTYPE = PlayerInfoType.Warrior;
    //                break;
    //            case "FemaleAssassin":
    //                skillinfo.PlayerTYPE = PlayerType.Female;
    //                break;
    //        }
    //        switch (infoArray[4])
    //        {
    //            case "Basic":
    //                skillinfo.Skilltype = SkillType.Basic;
    //                break;
    //            case "Skill":
    //                skillinfo.Skilltype = SkillType.Skill;
    //                break;
    //        }
    //        switch (infoArray[5])
    //        {
    //            case "Basic":
    //                skillinfo.Postype = PosType.Basic;
    //                break;
    //            case "One":
    //                skillinfo.Postype = PosType.One;
    //                break;

    //            case "Two":
    //                skillinfo.Postype = PosType.Two;
    //                break;

    //            case "Three":
    //                skillinfo.Postype = PosType.Three;
    //                break;
    //        }
    //        skillinfo.ColdTime = int.Parse(infoArray[6]);
    //        skillinfo.Damage = int.Parse(infoArray[7]);
    //        skillinfo.Level = 1;
    //        skillList.Add(skillinfo);
    //        skillDic.Add(skillinfo.ID, skillinfo);
    //    }


    //}
    ///// <summary>
    ///// 获取技能信息
    ///// </summary>
    ///// <param name="posType"></param>
    ///// <returns></returns>
    // public Skill GetSkillByPosType(PosType posType)
    //{
    //    foreach (Skill skill in skillList)
    //    {
    //        if (skill.Postype == posType&&skill.PlayerTYPE ==  PlayerInfo._instance.PlayerTYPE)
    //        {
    //            return skill;
    //        }
    //    }
    //    return null;
    //}

}
