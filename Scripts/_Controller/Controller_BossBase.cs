using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Controller
{
    public class Controller_BossBase : MonoBehaviour
    {
        private static Controller_BossBase _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_BossBase Instance
        {
            get { return Controller_BossBase._instance; }
            set { _instance = value; }
        }
        /// <summary>boss个数比较少 暂定为三个 </summary>
        private List<GameObject> bossList = new List<GameObject>();//怪物列表
        /// <summary>
        /// 返回怪物列表
        /// </summary>
        /// <returns></returns>
        public List<GameObject> GetBossList()
        {
            return bossList;
        }
        /// <summary>
        /// 列表清空boss
        /// </summary>
        /// <param name="boss"></param>
        public void DestoryBoss(GameObject boss)
        {         
            bossList.Remove(boss);
            //当前boss的所有控制脚本必须失效
            boss.transform.GetComponent<Controller_BossMove>().enabled = false;
            boss.transform.GetComponent<Controller_BossAction>().enabled = false;
            //boss不会直接消失  会躺尸
            // Destroy(boss, 20f);

        }
        /// <summary>
        /// 查找boss
        /// </summary>
        public void FindBoss()
        {
            GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");//找到所有boss
            for (int i = 0, max = bosses.Length; i < max; i++)
            {
                Debug.Log(bosses[i].name);
                bossList.Add(bosses[i]);
            }
        }
        /// <summary>
        /// 动态生成怪物
        /// </summary>
        public void SpawnCreateBosses()
        {
            string path = "_Prefabs/_Boss/_Girl/BaiGuNiangNiang";
            //实例化
            for (int i = 0, max = transform.childCount; i < max; i++)
            {
                GameObject cloneBoss = ResourcesManager.GetInstance().CreateGameObject(path, true);
                cloneBoss.transform.parent = transform.GetChild(i);
                cloneBoss.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
        }
        void Awake()
        {
            SpawnCreateBosses();
            _instance = this;
            FindBoss();
        }
        void Start()
        {

        }
    }
}