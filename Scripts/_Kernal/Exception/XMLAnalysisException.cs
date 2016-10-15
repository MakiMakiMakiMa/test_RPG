using UnityEngine;
using System.Collections;
namespace Kernal
{
    public class XMLAnalysisException : System.Exception
    {
        public XMLAnalysisException() :base(){ }
        public XMLAnalysisException(string exceptionMessege) : base(exceptionMessege) { }

    }
}
