using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts._Kernal
{
    public  class GlobalParametersData
    {
        //下一场景名称
        private SceneType _NextScenesName;
         //玩家的姓名
        private string _PlayerName;

        /*属性定义*/
        public SceneType NextScenesName
        {
            get { return _NextScenesName; }
            set { _NextScenesName = value; }
        }
       
        public string PlayerName
        {
            get { return _PlayerName; }
            set { _PlayerName = value; }
        }

        //无参数构造函数序列化里面用
        private GlobalParametersData() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="scenesName">场景名称</param>
        /// <param name="playerName">玩家姓名</param>
        public GlobalParametersData(SceneType scenesName, string playerName)
        {
            _NextScenesName = scenesName;
            _PlayerName = playerName;
        }
    }
}
