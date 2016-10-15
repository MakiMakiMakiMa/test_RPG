using UnityEngine;
using System.Collections;
using Global;
namespace Controller
{
    public class TestTrigger : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter(Collider e)
        {
            Debug.Log("玩家进入");
            GlobalParametersManager.NextSceneTYPE = SceneType.SceneTran01;
            Application.LoadLevel(SceneType.SceneLoading.ToString());
        }
    }
}
