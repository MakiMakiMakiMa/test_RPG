using UnityEngine;
using System.Collections;
using Global;
namespace Controller
{
    public class Controller_PlayerMove : MonoBehaviour
    {
        private GameObject _player;
        private Animator transAnim;//状态机
        private CharacterController CC;//角色控制器
        //摇杆的名称
        private const string JOYSTICK_NAME = "HeroJoystick";
        public bool isExistJoystick = true;
        public float speed = 5;
        //角色控制器重力系统
        private float _FloGravity = 1F;    //角色控制器重力
        // Use this for initialization
        void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            transAnim = _player.transform.GetComponent<Animator>();
            CC = _player.transform.transform.GetComponent<CharacterController>();
        }
        /// <summary>
        /// 游戏对象启用
        /// </summary>
        void OnEnable()
        {
            EasyJoystick.On_JoystickMove += OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd += OnJoystickMoveEnd;
        }


        /// <summary>
        /// 游戏对象的禁用
        /// </summary>
        public void OnDisable()
        {
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        }

        /// <summary>
        /// 游戏对象销毁
        /// </summary>
        public void OnDestroy()
        {
            EasyJoystick.On_JoystickMove -= OnJoystickMove;
            EasyJoystick.On_JoystickMoveEnd -= OnJoystickMoveEnd;
        }
        void Start()
        {
            GlobalParametersManager.CurrentActionTYPE = ActionType.Idle;

        }
        // Update is called once per frame
        void Update()
        {
            //if (PlayerInfo.Instance.PlayerPropertyCurrent.HP <= 0) return;
            if (isExistJoystick == false)//键盘控制
            {
                //pc端移动
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                if (h != 0 || v != 0)
                {
                    if (GlobalParametersManager.CurrentActionTYPE == ActionType.Idle || GlobalParametersManager.CurrentActionTYPE == ActionType.Run)
                    {
                        //设置角色的朝向（朝向当前坐标+摇杆偏移量）                  
                        _player.transform.LookAt(new Vector3(_player.transform.position.x - v, _player.transform.position.y, _player.transform.position.z + h));
                        //移动玩家的位置（按朝向位置移动）  
                        //transform.Translate(Vector3.forward * Time.deltaTime * 5);
                        Vector3 movement = _player.transform.forward * Time.deltaTime * speed;
                        //角色控制器模拟重力
                        movement.y -= _FloGravity;
                        //角色控制器
                        CC.Move(movement);
                        Controller_PlayerAction.Instance.PlayAction(ActionType.Run);
                    }
                }
                else if (h == 0 &&v == 0&&GlobalParametersManager.CurrentActionTYPE==ActionType.Run)
                {
                    Controller_PlayerAction.Instance.PlayAction(ActionType.Idle);
                }
            }

        }
        //移动摇杆中  
        void OnJoystickMove(MovingJoystick move)
        {
            if (isExistJoystick == false)
            {
                return;
            }  
            else
            {
                //获取摇杆中心偏移的坐标  
                float joyPositionX = move.joystickAxis.x;
                float joyPositionY = move.joystickAxis.y;
                if (joyPositionY != 0 || joyPositionX != 0)
                {
                    if (GlobalParametersManager.CurrentActionTYPE == ActionType.Idle || GlobalParametersManager.CurrentActionTYPE == ActionType.Run)
                    {
                        Controller_PlayerAction.Instance.PlayAction(ActionType.Run);
                        //设置角色的朝向（朝向当前坐标+摇杆偏移量）                  
                        _player.transform.LookAt(new Vector3(_player.transform.position.x - joyPositionY, _player.transform.position.y, _player.transform.position.z + joyPositionX));
                        //移动玩家的位置（按朝向位置移动）  
                        //transform.Translate(Vector3.forward * Time.deltaTime * 5);
                        Vector3 movement = _player.transform.forward * Time.deltaTime * speed;
                        //角色控制器模拟重力
                        movement.y -= _FloGravity;
                        //角色控制器
                        CC.Move(movement);
                    }
                }
            }
        }
        /// <summary>
        /// 移动摇杆结束
        /// </summary>
        /// <param name="move"></param>
        void OnJoystickMoveEnd(MovingJoystick move)
        {
            //停止时，角色恢复
            Controller_PlayerAction.Instance.PlayAction(ActionType.Idle);
        }
       
    }
}
