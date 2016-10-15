using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskUI : MonoBehaviour 
{
    //private UIGrid taskListGird;//在他的下面加载任务项
    //public  GameObject taskItmeUIPrefab;//把信息实例化

    //public static TaskUI _instance;
    //private UIPanel taskUI;
    //private UIButton btn_close;
    //string isGet;

    //private Task task;//当前正在执行的任务
    //private PlayerAutoMove playerAutoMove;
    //public PlayerAutoMove PlayerAutoMOVE
    //{
    //    get
    //    {
    //        if (playerAutoMove == null)
    //        {
    //            this.playerAutoMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAutoMove>();
    //        }
    //        return this.playerAutoMove;
    //    }
    //}

    //void Start ()
    //{
    //    InitTaskUI();
    //    taskUI.alpha = 0;
    //}
    //void Awake()
    //{
    //    _instance = this;
    //    taskUI=this.gameObject.GetComponent<UIPanel>();
    //    btn_close = transform.Find("Task_bg/btn_colse").GetComponent<UIButton>();

    //    taskListGird = transform.Find("Task_bg/Scroll_View/Grid").GetComponent<UIGrid>();

    //    EventDelegate edClose = new EventDelegate(this, "OnCloseButtonClick");
    //    btn_close.onClick.Add(edClose);
    //}

    //void Update () 
    //{
	
    //}
    //void InitTaskUI()
    //{
    //    ArrayList itemListUI = TaskMng._instance.taskInfoList;
    //    foreach (Task task in itemListUI)
    //    {
    //        //列表的实例化用NGUITools，在父类下添加子物体的方法           
    //        GameObject go = NGUITools.AddChild(taskListGird.gameObject,taskItmeUIPrefab);
    //        taskListGird.AddChild(go.transform);//每次实力化后对添加的物体都进行排序 
    //        TaskItemUI itemTask=go.GetComponent<TaskItemUI>();
    //        itemTask.Init(task);
    //    }
    //}
    //void OnCloseButtonClick()
    //{
    //    taskUI.alpha = 0;
    //}
    //public void ShowTaskUI()
    //{
    //    taskUI.alpha = 1;
    //}
    ////处理taskItem的点击事件
    //public void OnRewardButtonClick(object[] rewardArray)
    //{
    //    object[] array = rewardArray;
    //    RewardPopup._instance.RewardPopUpDisplay(array);
    //}
    //public void OnCombatButtonClick(object[] rewardArray)
    //{
    //   task = rewardArray[0] as Task;
    //    //未接受任务的时候 自动寻找NPC
    //   if (task.TaskPRO == Task.TaskProgress.NoStart)
    //   { 
    //       //取得PlayerAutoMove的目标
    //       Vector3 targetPos = NPCManager._instance.GetNPC(task.NPCID).transform.position;
    //       this.PlayerAutoMOVE.SetDestination(targetPos);
    //       NPCTaskTalk._instance.SetCurrent(task);
          
    //   }
    //    //自动寻路副本
    //   if (task.TaskPRO == Task.TaskProgress.Accepted)
    //   {
    //       //取得PlayerAutoMove的目标
    //       this.PlayerAutoMOVE.SetDestination(NPCManager._instance.TransPos.transform.position);
    //   }
    //}
    //public void OnRewardGeted(string str)
    //{
     
    //}

}
