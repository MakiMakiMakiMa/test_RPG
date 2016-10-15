using UnityEngine;
using System.Collections;

namespace Controller
{
    /// <summary>
    /// 通过鼠标围绕目标旋转
    /// </summary>
    public class MouseLookWithTarget : MonoBehaviour
    {

        public enum RotationAxes { Mouse_XY=0, Mouse_X=1, Mouse_Y =1}
        public RotationAxes axes = RotationAxes.Mouse_XY;
        public float sensitivity_X = 15f;
        public float sensitivity_Y = 15f;

        public float minimum_X =-360f;
        public float maxmum__X= 360f;

        public float minimum_Y = -85f;//最小朝上旋转
        public float maxmum__Y =4f;//最大朝上旋转

        public float rotationY = 0f;//旋转速度
        private GameObject _target;
        public Vector3 offsetPosition;//初始时相机与人的位置关系
        public Vector3 offsetRoation;//初始时相机与人的位置关系
        public float smoothing = 1;
        public float distance = 0;

        public float theDistance=1;
        public float MaxDistance=2;
        // Use this for initialization
        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            offsetPosition = Camera.main.transform.position - _target.transform.position;
            offsetRoation.y = transform.localEulerAngles.y - _target.transform.localEulerAngles.y;
            //将目标世界坐标转换自身为中心的范围坐标
            Vector3 pos = transform.InverseTransformPoint(_target.transform.position);
            //获得自身到敌人的直线距离
            distance = Vector3.Distance(Vector3.zero, pos);
        
        }

        // Update is called once per frame
        void Update()
        {
            if (theDistance > 0) theDistance = 0;
            if (theDistance < MaxDistance) theDistance = MaxDistance;
            if (Input.GetMouseButton(1))
            {
                //transform.position = _target.transform.position;
                if (axes == RotationAxes.Mouse_XY)
                {
                    float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity_X;
                    rotationY += Input.GetAxis("Mouse Y") * sensitivity_Y;
                    rotationY = Mathf.Clamp(rotationY, minimum_Y, maxmum__Y);//判定值是否在控制区域类
                    transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
                }
                else if (axes == RotationAxes.Mouse_X)
                {
                    transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity_X, 0);
                }
                else
                {
                    rotationY += Input.GetAxis("Mouse Y") * sensitivity_Y;
                    rotationY = Mathf.Clamp(rotationY, minimum_Y, maxmum__Y);//判定值是否在控制区域类
                    transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
                }
               
            }
            else
            {
               // transform.position = _target.transform.position;
                SetDistance();
           
            }
        }
        void SetDistance()
        {
            ///摄像头缓冲跟随 插值运算
            Vector3 targetPos = _target.transform.position + offsetPosition;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothing);
            //平滑旋转：比较好的方法:不旋转摄像头的时候一定角度
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - this.transform.position), Time.deltaTime * 20);           
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.transform.position - this.transform.position), Time.deltaTime * 20);           
           
        }
        void LookPlayerForward()
        {

            Debug.Log(_target.transform.forward);
            ///摄像头缓冲跟随 插值运算
            Vector3 targetPos = new Vector3(_target.transform.forward.x -offsetPosition.x, _target.transform.position.y- offsetPosition.y, _target.transform.forward.z-offsetPosition.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothing);
 
        }
    }
}
