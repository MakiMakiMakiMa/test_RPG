using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskMng : MonoBehaviour 
{
    public static TaskMng _instance;
    public TextAsset taskList;
    public ArrayList taskInfoList = new ArrayList();//必须要new一下，否者下面的值添加不进去

   
	void Start ()
    {
        ReadTaskListInfo();
	}
    void Awake()
    {
        _instance = this;
    }
	void Update () 
    {	
	}
    void ReadTaskListInfo()
    {
        string[] itemTaskArray = taskList.ToString().Split('\n');
        foreach (string itemTask in itemTaskArray)
        {            
            string[] itemTaskInfo = itemTask.Split('|');
            Task task = new Task();
            task.ID =int.Parse(itemTaskInfo[0]);
            switch(itemTaskInfo[1])
            {
                case "Main":
                   task.TaskTYPE = Task.TaskType.Main;
                    task.TaskTypeICON = "pic_主线";
                    break;
                case "Reward":
                    task.TaskTYPE = Task.TaskType.Reward;
                    task.TaskTypeICON = "pic_奖赏";
                    break;
                case "Daily":
                    task.TaskTYPE = Task.TaskType.Daily;
                    task.TaskTypeICON = "pic_日常";
                    break;
            }
            task.TaskName = itemTaskInfo[2];
            task.RewardICON = itemTaskInfo[3];
            task.DesTask = itemTaskInfo[4];
            task.RewardCoin = int.Parse(itemTaskInfo[5]);
            task.RewardDiamond = int.Parse(itemTaskInfo[6]);
            task.PromptTask = itemTaskInfo[7];
            task.NPCID = int.Parse(itemTaskInfo[8]);
            task.TransID = int.Parse(itemTaskInfo[9]);
            task.TaskPRO = Task.TaskProgress.NoStart;
            this.taskInfoList.Add(task);
        }
    }
    public ArrayList GetTaskList()
    {
        return this.taskInfoList;
    }
}
