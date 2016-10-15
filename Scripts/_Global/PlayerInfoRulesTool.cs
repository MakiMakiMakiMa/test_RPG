/***
 * 模型层：升级规则
 * 描述项目中“升级”的规则 
 * 按照设计模式中的[开放][封闭]原则与[单一]职责
 * 定义本类的功能
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Model;
using Excel;
using System.Data;
using System.IO;
namespace Global
{
    public class PlayerInfoRulesTool: MonoBehaviour
    {
        private static PlayerInfoRulesTool _instance;               //本类实例
        /// <summary>
        /// 得到本类实例
        /// </summary>
        public static PlayerInfoRulesTool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.Find("_PlayerInfo").AddComponent<PlayerInfoRulesTool>();
                }
                return _instance;
            }   
        }

        //定义一个数值改变的类
        //用来存取升级所需经验 升级时最大基础血量增加值 最大基础魔法增加值
        private Dictionary<int, PlayerUpGradeInfo> upGaradeInfoDic = new Dictionary<int, PlayerUpGradeInfo>();
        // 最大基础攻击增加值 最大基础防御增加值 最大基础敏捷增加值
        //最大基础暴击触发率增加值 最大基础暴击触发伤害增加值
        void Awake()
        {
            //读取EXCEL文件
            ExcelAccess.SelectMenuTable(1);
            SetUpGaradeInfoDic();
        }
        void Start()
        {

        }
        #region 读取升级信息Excel表格
        /// <summary>
        /// 获取到升级字典信息
        /// </summary>
        public void SetUpGaradeInfoDic()
        {
            this.upGaradeInfoDic = ExcelAccess.GetUpGaradeInfoDic();
        }
        #endregion
        #region 升级规则
        /// <summary>
        /// 经验增加规则
        /// </summary>
        /// <param name="exp_plus"></param>
        public void ExpPlus(int exp_plus)
        {
            int exp_upGrade= upGaradeInfoDic[PlayerInfo.Instance.PlayerPropertyCurrent.Level + 1].Exp;
            PlayerInfo.Instance.PlayerPropertyCurrent.ExpUpGrade = exp_upGrade;
            if (exp_plus + PlayerInfo.Instance.PlayerPropertyCurrent.Exp < exp_upGrade)
            {
                //未升级
                PlayerInfo.Instance.PlayerPropertyCurrent.Exp += exp_plus;
                //Debug.Log(PlayerInfo.Instance.GetCurrentMP());
                PlayerInfo.Instance.GetPlayerInfo(PlayerInfoType.All);
                return;
            }
            else
            {

                //
                PlayerInfo.Instance.PlayerPropertyCurrent.Exp = exp_plus + PlayerInfo.Instance.PlayerPropertyCurrent.Exp - exp_upGrade;
                PlayerInfo.Instance.PlayerPropertyCurrent.Level++;//升级
                UpgradeOperation(PlayerInfo.Instance.PlayerPropertyCurrent.Level);
                //升级
                //通知其他地方调用升级特效
                PlayerInfo.Instance.GetPlayerInfo(PlayerInfoType.Upgrade);
                //升级后判断时候还能在升级
                PlayerInfo.Instance.PlayerPropertyCurrent.ExpUpGrade = upGaradeInfoDic[PlayerInfo.Instance.PlayerPropertyCurrent.Level + 1].Exp;

            }
            ExpPlus(0);
        }
        /// <summary>
        /// 升级厚要执行的操作
        /// </summary>
        /// <param name="level">升级后的等级</param>
        public void UpgradeOperation(int level)
        { 
            PlayerUpGradeInfo levelInfo=upGaradeInfoDic[level];
            UpgradeRuleOperation(levelInfo);
        }//UpgradeOperation_End
        /// <summary>
        /// 具体升级规则
        /// </summary>
        /// <param name="levelInfo"></param>
        private void UpgradeRuleOperation(PlayerUpGradeInfo levelInfo)
        {
            //玩家所有的核心最大数值[生命值]增加
            PlayerInfo.Instance.PlusHPMax(levelInfo.HpPlus);
            PlayerInfo.Instance.PlusMPMax(levelInfo.MpPlus);
            PlayerInfo.Instance.PlusATKMax(levelInfo.AttackPlus);
            PlayerInfo.Instance.PlusDEFMax(levelInfo.DefencePlus);
            PlayerInfo.Instance.PlusDEXMax(levelInfo.DexterityPlus);
            PlayerInfo.Instance.PlusCTRMax(levelInfo.CriticalTriggerRatePlus);
            PlayerInfo.Instance.PlusCARMax(levelInfo.CriticalAttackPlus);
            //对应的"生命数值" "魔法数值"，加满为最大数值
            PlayerInfo.Instance.PlusHP(PlayerInfo.Instance.PlayerPropertyCurrent.HPMax);
            PlayerInfo.Instance.PlusMP(PlayerInfo.Instance.PlayerPropertyCurrent.HPMax);
        }
       #endregion

    }//Class_End
    /// <summary>
    /// Excel表格读取
    /// </summary>
    public class ExcelAccess
    {
        public static string ExcelName = "UpGradeInfo.xls";//表格名称
        public static string[] SheetNames = { "Sheet1", "Sheet2", "Sheet3", "Sheet4" };//表类工作区的名称
        private static Dictionary<int, PlayerUpGradeInfo> upGaradeInfoDic = new Dictionary<int, PlayerUpGradeInfo>();
        /// <summary>
        /// 获取当前升级信息词典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, PlayerUpGradeInfo> GetUpGaradeInfoDic()
        {
            return upGaradeInfoDic;
        }
        /// <summary>
        /// 读取表格信息保存到PlayerUpGradeInfo的list集合和字典中
       /// </summary>
       /// <param name="tableId"></param>
       /// <returns></returns>
        public static List<PlayerUpGradeInfo> SelectMenuTable(int tableId)
        {
            DataRowCollection collect = ExcelAccess.ReadExcel(SheetNames[tableId - 1]);
            List<PlayerUpGradeInfo> upGradeInfoList = new List<PlayerUpGradeInfo>();
            for (int i = 1; i < collect.Count; i++)
            {
                if (collect[i][1].ToString() == null) continue;
                PlayerUpGradeInfo itemInfo = new PlayerUpGradeInfo();
                if (collect[i][0].ToString() !="")
                {
                    
                    itemInfo.Level = int.Parse(collect[i][0].ToString());
                    itemInfo.Exp = int.Parse(collect[i][1].ToString());
                    itemInfo.HpPlus = int.Parse(collect[i][2].ToString());
                    itemInfo.MpPlus = int.Parse(collect[i][3].ToString());
                    itemInfo.AttackPlus = int.Parse(collect[i][4].ToString());
                    itemInfo.DefencePlus = int.Parse(collect[i][5].ToString());
                    itemInfo.DexterityPlus = int.Parse(collect[i][6].ToString());
                    itemInfo.CriticalTriggerRatePlus = float.Parse(collect[i][7].ToString());
                    itemInfo.CriticalAttackPlus = float.Parse(collect[i][8].ToString());
                    upGaradeInfoDic.Add(itemInfo.Level, itemInfo);
                    upGradeInfoList.Add(itemInfo);
                }
            }
            return upGradeInfoList;
        }
        /// <summary>
        /// 读取EXCEL表格时候要引用Excel.dll system.Data.dll
        /// </summary>
        /// <param name="sheet">表名</param>
        /// <returns>返回一个行集合</returns>
        static DataRowCollection ReadExcel(string sheet)
        {
            FileStream stream = File.Open(FilePath(ExcelName), FileMode.Open, FileAccess.Read, FileShare.Read);
            //IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);//读取xlsx使用
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);//读取xls使用
             DataSet result = excelReader.AsDataSet();
             return result.Tables[sheet].Rows;
        }
        /// <summary>
        /// 文件路径
        /// </summary>
        /// <param name="ExcelName"></param>
        /// <returns></returns>
        private static string FilePath(string ExcelName)
        {
            Debug.Log("怎么写啊");
            string str = Application.dataPath +"/"+ ExcelName;
            return str;
        }
    }
}