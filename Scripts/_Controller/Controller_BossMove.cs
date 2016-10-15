using UnityEngine;
using System.Collections;
using Global;
namespace Controller
{
    public class Controller_BossMove : MonoBehaviour
    {

        private CharacterController CC;//角色控制器
        private GameObject _player;
        private Animator _transAniBoss;
        public float _speed =5;
        //角色控制器重力系统
        private float _FloGravity = 1F;    //角色控制器重力
        // Use this for initialization
        void Start()
        {
            CC = this.transform.GetComponent<CharacterController>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _transAniBoss=transform.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (this.transform.GetComponent<Controller_BossAction>().IsFindPlayer() && (this.transform.GetComponent<Controller_BossAction>().CurrentActionType == EnemyActionType.Idle || this.transform.GetComponent<Controller_BossAction>().CurrentActionType == EnemyActionType.Run))
            {
                //Debug.Log("可以移动了");
                //四元素 旋转注视Boss
                this.transform.rotation =
                    Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation
                            (new Vector3(_player.transform.position.x, 0, _player.transform.position.z) -
                            new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)), 10f
                                );
                this.transform.GetComponent<Controller_BossAction>().PlayBossAction(EnemyActionType.Run);//boss和怪暂时用一套枚举
                Vector3 movement = transform.forward * Time.deltaTime * _speed;
                //角色控制器模拟重力
                movement.y -= _FloGravity;
                //角色控制器
                CC.Move(movement);
               // Debug.Log("Boss移动了");

            }
            else if (this.transform.GetComponent<Controller_BossAction>().CurrentActionType == EnemyActionType.Run)
            {
                this.transform.GetComponent<Controller_BossAction>().PlayBossAction(EnemyActionType.Idle);//不移动的时候转化Idle
            }

            Vector3 movement2 = new Vector3(0,0,0);
            //角色控制器模拟重力
            movement2.y -= _FloGravity;
            //角色控制器
            CC.Move(movement2);
        }
    }
}
