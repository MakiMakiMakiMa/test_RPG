using UnityEngine;
using System.Collections;
using Global;
namespace Controller
{
    public class Controller_KeyAttack : MonoBehaviour
    {
        public GameObject[] skillPosArray;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.J))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(skillPosArray[0], SkillType.Basic, ActionType.Basic, 0.01f);
            }
            if (Input.GetKey(KeyCode.K))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(skillPosArray[1], SkillType.Skill, ActionType.Skill01, 2f);
            }
            if (Input.GetKey(KeyCode.L))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(skillPosArray[2], SkillType.Skill, ActionType.Skill02, 3f);
            }
            if (Input.GetKey(KeyCode.I))
            {
                Controller_SceneTranscipt01.Instance.OnAttackButtonClick(skillPosArray[3], SkillType.Skill, ActionType.Skill03, 4f);
            }
        }
    }
}
