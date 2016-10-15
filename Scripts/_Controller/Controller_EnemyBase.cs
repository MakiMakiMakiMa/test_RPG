using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kernal;

namespace Controller
{
    public class Controller_EnemyBase : MonoBehaviour
    {
        private string[] enemyNameArray = new string[4] { "YuBaoBao", "Tufei", "ShouWei", "ShanZei" };
        private static Controller_EnemyBase _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_EnemyBase Instance
        {
            get { return Controller_EnemyBase._instance; }
            set { _instance = value; }
        }
        private  List<GameObject> enemyList = new List<GameObject>();//怪物列表
        /// <summary>
        /// 返回怪物列表
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetEnemyList()
        {
            return enemyList;
        }
        /// <summary>
        /// 列表清空boss
        /// </summary>
        /// <param name="boss"></param>
        public void DestoryBoss(GameObject enemy)
        {
            enemyList.Remove(enemy);
            //当前boss的所有控制脚本必须失效
            enemy.transform.GetComponent<Controller_BossMove>().enabled = false;
            enemy.transform.GetComponent<Controller_BossAction>().enabled = false;
            //boss不会直接消失  会躺尸
            // Destroy(boss, 20f);

        }
        /// <summary>
        /// 查找enemy
        /// </summary>
        public void Findenemy()
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");//找到所有boss
            for (int i = 0, max = enemys.Length; i < max; i++)
            {
                Debug.Log(enemys[i].name);
                if (!enemyList.Contains(enemys[i]))//当不包含的时候
                {
                    enemyList.Add(enemys[i]);
                }
                Debug.Log(enemyList.Count);
            }
        }
        /// <summary>
        /// 动态生成怪物
        /// </summary>
        public void SpawnCreateEnemys()
        {            
            //实例化
            for (int i = 0, max = transform.childCount; i < max; i++)
            {
                if (transform.GetChild(i).childCount == 0)
                {
                    string path = "_Prefabs/_Enemy/" + enemyNameArray[Random.Range(0, enemyNameArray.Length)];
                    GameObject cloneEnemyPrefab = ResourcesManager.GetInstance().Load<GameObject>(path, true);
                    Debug.Log("123123");
                    GameObject poolEnemy = PoolManager.poolMngDic["EnemysPool"].GetGameObjectByPools(cloneEnemyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    //注意怪的池子不需要自动回收
                    poolEnemy.transform.parent = transform.GetChild(i);
                    poolEnemy.transform.localPosition = new Vector3(0,0,0);
                    poolEnemy.transform.localRotation =Quaternion.identity;
                    poolEnemy.transform.localScale =new Vector3(2,2,2);
                }

            }
        }
        void Awake()
        {
            //动态生成敌人
            SpawnCreateEnemys();
            _instance = this;
            //查找敌人
            Findenemy();
        }
        void Start()
        {

        }
        void Update()
        {
        }
    }
}