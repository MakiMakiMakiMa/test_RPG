using UnityEngine;
using System.Collections;
/// <summary>
/// 摄像机跟随玩家移动
/// </summary>
public class MoveWithTarget : MonoBehaviour {

    public Vector3 offsetPosition;
    public Vector3 offsetRotation;
    private Transform _player;
    public float smoothing = 1;
    //-6f;
    //3.553f
    //3f;
   Transform player;
	// Use this for initialization
	void Start ()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;//.Find("Bip01");
        offsetPosition = Camera.main.transform.position - _player.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
       ///无缓冲跟随
        //transform.position = player.position + offset;
        ///摄像头缓冲跟随 插值运算
        Vector3 targetPos = _player.position + offsetPosition;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * smoothing);

        //平滑旋转：比较好的方法
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_player.transform.position - this.transform.position), Time.deltaTime * 20);
	}
}
