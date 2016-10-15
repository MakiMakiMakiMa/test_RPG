using UnityEngine;
using System.Collections;

public class Task : MonoBehaviour 
{
    public enum TaskType
    {
        Main,
        Reward,
        Daily
    }
    public enum TaskProgress
    { 
        NoStart,
        Accepted,
        Completed
    }
    private TaskType taskType;
    private TaskProgress taskPro;
    private int id;
    private string iconTaskType;//"pic_主线","pic_日常","pic_"
    private string taskname; // "试炼之地", "赏金猎人", "每日通关" 
    private string iconReward;
    private string desTask;//任务描述
    private string promptTast;//任务提示
    private int coinReward;
    private int diamondReward;
    private int npcID;
    private int transID;


    //属性
    public TaskType TaskTYPE
    {
        get
        {
            return this.taskType;
        }
        set
        {
            this.taskType = value;
        }
    }
    public TaskProgress TaskPRO
    {
        get 
        {
        return this.taskPro;
        }
        set
        {
        this.taskPro=value;
        }
    }
    public int ID
    {
        get 
        { 
            return this.id; 
        }
        set
        { 
            this.id = value;
        }
    }
    public string TaskTypeICON
    {
        get
        {
            return this.iconTaskType;
        }
        set
        {
            this.iconTaskType = value;
        }
    }
    public string TaskName
    {
        get 
        { 
            return this.taskname; 
        }
        set
        {
            this.taskname = value;
        }
    }
    public string RewardICON
    {
        get
        {
            return this.iconReward;
        }
        set
        {
            this.iconReward = value;
        }
    }
    public string DesTask
    {
        get 
        {
            return this.desTask;
        }
        set
        {
            this.desTask = value;
        }
    }
    public string PromptTask
    {
        get
        {
            return this.promptTast;
        }
        set
        {
            this.promptTast = value;
        }
    }
    public int RewardCoin
    {
        get
        {
            return this.coinReward;
        }
        set
        {
            this.coinReward = value;
        }
    }
    public int RewardDiamond
    {
        get
        {
            return this.diamondReward;
        }
        set
        {
            this.diamondReward = value;
        }
    }
    public int NPCID
    {
        get 
        {
            return this.npcID;
        }
        set
        {
        this.npcID=value;}
    }
    public int TransID
    {
        get 
        {
            return this.transID;
        }
        set
        {
            this.transID = value;
        }
    }
	void Start () 
    {
	}	
	void Update () 
    {
	}
}
