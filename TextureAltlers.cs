using UnityEngine;
using System.Collections;
namespace Kernal
{
    public class TextureAltlers : MonoBehaviour
    {

        public Sprite[] textureArray;

        private static TextureAltlers _instance;

        public static TextureAltlers Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("TextureAltlers").AddComponent<TextureAltlers>();
                }
                return TextureAltlers._instance;
            }
        }

        void Awake()
        {
            _instance = this;
        }

    }
}
