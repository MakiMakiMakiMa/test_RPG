using UnityEngine;
using System.Collections;
using Model;
using System.Collections.Generic;
namespace Global
{
    public class EnemyPropertyManager : MonoBehaviour
    {
        public TextAsset txtEnemyInfoList;
        private static EnemyPropertyManager _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static EnemyPropertyManager Instance
        {
            get { 
                // if (_instance == null)
                //{
                //    _instance = new GameObject("EnemyPropertyManager").AddComponent<EnemyPropertyManager>();
                //}
                return EnemyPropertyManager._instance; 
            }
            set { _instance = value; }
        }
        /// <summary>怪物信息库</summary>
        public Dictionary<string, EnemyProperty> enemyInfoDic = new Dictionary<string, EnemyProperty>();
        void Awake()
        {
            _instance = this;
            ReadEnemyInfoFromLocalText();
        }
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// 从本地读取怪物清单(也可以从服务器获取)
        /// </summary>
        void ReadEnemyInfoFromLocalText()
        {  
            string allStr=txtEnemyInfoList.ToString();
             string[] itemStrArray = allStr.Split('\n');            
            //编号|类型|名称|等级|血量|攻击|防御|速度|致命一击|掉落ID|搜索范围|攻击距离
             foreach (string itemStr in itemStrArray)
             {
                 string[] proArray = itemStr.Split('|');
                 Debug.Log(itemStr);
                 EnemyProperty enemy = new EnemyProperty();
                 enemy.ID =int.Parse( proArray[0]);
                 enemy.TYPE = EnemyType.Boss;
                 enemy.Name = proArray[2];
                 enemy.Level = int.Parse(proArray[3]);
                 enemy.HP = int.Parse(proArray[4]);
                 enemy.Attack = int.Parse(proArray[5]);
                 enemy.Defence = int.Parse(proArray[6]);
                 enemy.Dexterity = int.Parse(proArray[7]);
                 enemy.CriticalTriggerRate = float.Parse(proArray[8]);
                 enemy.CriticalAttackRate = float.Parse(proArray[9]);
                 enemy.DropID = int.Parse(proArray[10]);
                 enemy.CanFindPlayer = float.Parse(proArray[11]);
                 enemy.CanAttackplayer = float.Parse(proArray[12]);
                 enemyInfoDic.Add(enemy.Name, enemy);
             }
        }
    }
}
