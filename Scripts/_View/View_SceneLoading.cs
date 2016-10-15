using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Global;
using Kernal;
using System.Collections.Generic;
namespace View
{
    public class View_SceneLoading : View_UIBase
    {
        private Slider mLoadingProgress;//进度条控制
        private float _progressValue;//进度值
        private AsyncOperation o;
        private Text progressText;
        IEnumerator LoadingSceneProgress()
        {
            yield return new WaitForSeconds(0.1f);
            o = Application.LoadLevelAsync(ConvertEnumToString.Instacne.GetSceneStrBySceneType(GlobalParametersManager.NextSceneTYPE));
            _progressValue = o.progress;
            o.allowSceneActivation = false;
        }

        void InitData()
        {

           // GlobalParametersManager.NextSceneTYPE = SceneType.SceneLogin;
            mLoadingProgress = transform.Find("Slider").GetComponent<Slider>();
            progressText = transform.Find("ProgressText").GetComponent<Text>();
            StartCoroutine(LoadingSceneProgress());
            
        }
        #region 父类逻辑实现
        protected override void Init()
        {
            base.Init();
            InitData();
        }
        protected override void OnUpdate()
        {
            base.OnUpdate();
            OnInfoUpdate();
        }
        protected override void Start()
        {
            base.Start();
            StartCoroutine(LogTest());
        }

        IEnumerator LogTest()
        {
            //测试Log日志系统
            //面向"接口的编程"
            //IConfigManager configMgr = new ConfigManager(KernalParameter.SystemConfigInfo_LogPath, KernalParameter.SystemConfigInfo_LogRootNodeName);
            //string strLogPath = configMgr.AppSetting["LogPath"];
            //string strLogState = configMgr.AppSetting["LogState"];
            //string strLogMaxCapacity = configMgr.AppSetting["LogMaxCapacity"];
            //string strLogBufferNumber = configMgr.AppSetting["LogBufferNumber"];
            //print("Log Paht =" +strLogPath);
            //print("LogState =" +strLogState);
            //print("LogMaxCapacity =" +strLogMaxCapacity);
            //print("LogBufferNumber =" +strLogBufferNumber);
            //测试Log.cs 类
            //Log.Write("我的企业日志系统开始运行了，第一次测试",LevelType.Low);
            //Log.Write("1 低等级调试语句", LevelType.Low);
            //Log.Write("1 中等级别调试语句", LevelType.Special);
            //Log.Write("1 高级与重要的级调试语句", LevelType.High);
            //Log.Write("2 低等级调试语句",LevelType.Low);
            //Log.Write("2 中等级别调试语句", LevelType.Special);
            //Log.Write("2 高级与重要的级调试语句", LevelType.High);

            //Log.Write("--------1--------", LevelType.Low);
            //Log.Write("--------2--------", LevelType.Low);
            //Log.Write("--------3--------", LevelType.Low);
            //Log.Write("--------4--------", LevelType.Low);
            //Log.Write("--------5--------", LevelType.Low);


            /*测试XML解析程序*/
            //参数赋值
            //Log.ClearLogFileAndBufferAllDate();
            XMLDialogsDataAnalysisMgr.GerInstance().SetXMLPathAndRootNodeName(KernalParameter.DialogsXMLConfig_XmlPath, KernalParameter.DialogsXMLConfig_XmlPath_XmlRootNodeName);
            yield return new WaitForSeconds(0.5F);
            ////得到XML中所有的数据
            List<DialogDataFormat> liDialogsDataArray = XMLDialogsDataAnalysisMgr.GerInstance().GetAllxmlDataArray();

            Debug.Log(liDialogsDataArray.Count);
            //里面每条数据显示出来
            //foreach (DialogDataFormat data in liDialogsDataArray)
            //{
            //    Debug.Log("DialogContent=" + data.DialogContent);
            //    //Log.Write("DialogSecNum=" + data.DialogSecNum, LevelType.Low);
            //    //Log.Write("DialogSecName=" + data.DialogSecName, LevelType.Low);
            //    //Log.Write("SectionIndex=" + data.SectionIndex, LevelType.Low);
            //    //Log.Write("DialogSide=" + data.DialogSide, LevelType.Low);
            //    //Log.Write("DialogPerson=" + data.DialogPerson, LevelType.Low);
            //    //Log.Write("DialogContent=" + data.DialogContent, LevelType.Low);
            //}

            //Log.Write("我的企业日志系统开始运行了,第一次测试", LevelType.Low);
            //Log.Write("这是一个低等级调试语句", LevelType.Low);
            //Log.Write("高级调试语句", LevelType.High);
            //Log.Write("中等级别调试语句", LevelType.Special);
            
        }
        #endregion
        void  OnInfoUpdate()
        {
            if (_progressValue <= 1)
            {
                mLoadingProgress.value = _progressValue;
                progressText.text = (int)(_progressValue * 100) + "/100";
                _progressValue += 0.01f;
            }
            else
            {
                _progressValue = 1;
                o.allowSceneActivation = true;
            }
        }
    }
}
