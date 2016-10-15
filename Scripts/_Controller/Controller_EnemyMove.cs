using UnityEngine;
using System.Collections;
using Global;
namespace Controller
{
    public class Controller_EnemyMove : MonoBehaviour
    {
        private CharacterController CC;//角色控制器
        private GameObject _player;
        public float _speed = 5f;
        //角色控制器重力系统
        private float _FloGravity = 1F;    //角色控制器重力
        // Use this for initialization
        void Start()
        {
            CC =transform.transform.GetComponent<CharacterController>();
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (this.transform.GetComponent<Controller_EnemyAction>().IsFindPlayer())
            {
               //Debug.Log("小怪可以移动了");
                //四元素 旋转注视Boss
                this.transform.rotation =
                    Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation
                            (new Vector3(_player.transform.position.x, 0, _player.transform.position.z) -
                            new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z)), 10f
                                );
                transform.GetComponent<Animation>().CrossFade(EnemyActionType.Run.ToString());
                Vector3 movement = transform.forward * Time.deltaTime * _speed;
                //角色控制器模拟重力
                movement.y -= _FloGravity;
                //角色控制器
                CC.Move(movement);
             
            }

            Vector3 movement2 = new Vector3(0, 0, 0);
            //角色控制器模拟重力
            movement2.y -= _FloGravity;
            //角色控制器
            CC.Move(movement2);
        }
    }
}