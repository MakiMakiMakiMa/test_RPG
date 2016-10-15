using UnityEngine;
using System.Collections;
using Kernal;
using Global;
namespace Controller
{   
    /// <summary>
    /// 人物创建显示界面
    /// </summary>
    public class Controller_PlayerShow : MonoBehaviour
    {
        private static Controller_PlayerShow _instance;
        /// <summary>
        /// 单例
        /// </summary>
        public static Controller_PlayerShow Instance
        {
            get { return Controller_PlayerShow._instance; }
            set { _instance = value; }
        }
        #region 定义成员变量
        private GameObject[] playerShow=new GameObject[2];
        private GameObject[] playerShowPrefab = new GameObject[2];
        private GameObject[] playerPos = new GameObject[2];
        private GameObject roleSelectedPos;
        public AnimationClip[] playerAni_0;
        public AnimationClip[] playerAni_1;
        #endregion
        void Awake()
        {
            _instance = this;
            playerShowPrefab[0] = Resources.Load("_Prefabs/_RoleShow/CreateMage") as GameObject;
            playerShowPrefab[1] = Resources.Load("_Prefabs/_RoleShow/GreateWarrior") as GameObject;
            playerPos[0] = GameObject.Find("Girl_Pos");
            playerPos[1] = GameObject.Find("Boy_Pos");
            roleSelectedPos = GameObject.Find("Player_InfoPos");         
        }
        void Start()
        {
            AudioManager.Instacne.PlayAudioClip(AudioSourceType._BGAudioSource, "SelectHeroScenes");
           
        }
        /// <summary>
        /// 创建人物展示角色
        /// </summary>
        public   void CreatePlayerShow()
        {
            for (int i = 0, max = playerShow.Length; i < max; i++)
            {
                playerShow[i] = GameObject.Instantiate(playerShowPrefab[i]) as GameObject;
                playerShow[i].transform.parent = playerPos[i].transform;
                playerShow[i].transform.position = playerPos[i].transform.position;
                playerShow[i].transform.rotation = playerPos[i].transform.rotation;
               // playerShow[i].transform.localScale = new Vector3(1f,1f,1f);
                StartCoroutine(PlayerAni());
            }
        }
        /// <summary>
        /// 随机展示动画
        /// </summary>
        /// <returns></returns>
        IEnumerator PlayerAni()
        {
            yield return new WaitForSeconds(2.5f);
            playerShow[0].GetComponent<Animation>().CrossFade(playerAni_0[Random.Range(0, playerAni_0.Length)].name);
            playerShow[1].GetComponent<Animation>().CrossFade(playerAni_1[Random.Range(0, playerAni_1.Length)].name);
            StartCoroutine(PlayerAni());
            
        }
        /// <summary>
        /// 显示选择的角色
        /// </summary>
        public GameObject SelectRoleShow(int index)
        {
            HidePlayerShow();
            GameObject go = GameObject.Instantiate(playerShowPrefab[index]) as GameObject;
            go.transform.parent = roleSelectedPos.transform;
            go.transform.position = roleSelectedPos.transform.position;
            go.transform.rotation = roleSelectedPos.transform.rotation;
           // go.transform.localScale = new Vector3(1f, 1f, 1f);
            return go;
        }
        public GameObject SelectRoleShow(GameObject player)
        {
            HidePlayerShow();
            GameObject go = GameObject.Instantiate(player) as GameObject;
            go.transform.parent = roleSelectedPos.transform;
            go.transform.position = roleSelectedPos.transform.position;
            go.transform.rotation = roleSelectedPos.transform.rotation;
            go.GetComponent<Animation>().enabled = true;
            // go.transform.localScale = new Vector3(1f, 1f, 1f);
            return go;
        }
        /// <summary>
        /// 显示和隐藏角色
        /// </summary>
        public  void HidePlayerShow()
        {
            for (int i = 0, max = playerShow.Length; i < max; i++)
            {
               Destroy( playerShow[i]);
               StopAllCoroutines();
            }
         }
        public GameObject GetPlayer(int index)
        {
            if (playerShow.Length != null)
            {
                return playerShow[index];
            }
            else
            {
                Debug.Log("检查展示的人物是否存在");
                return null;
            }
        }
    }
}
