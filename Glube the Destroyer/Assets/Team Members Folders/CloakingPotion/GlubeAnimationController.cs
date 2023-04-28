using System.Collections;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Team_Members_Folders.CloakingPotion
{
    public class GlubeAnimationController : MonoBehaviour
    {
        public Animator anim;

        public float agentSpeedSLider = 0;

        public NavMeshAgent myAgent;

        private void Start()
        {
            //anim.SetFloat("AgentSpeed", agentSpeedSLider);
        }

        private void Update()
        {
            agentSpeedSLider = Mathf.Clamp01(agentSpeedSLider);
            float temp = Mathf.Clamp01(myAgent.velocity.magnitude);
            anim.SetFloat("AgentSpeed", temp);
        }

        public void StopAttacking()

        {
            anim.SetBool("IsAttackingBuilding", false);
        }

        public void StartAttackingAnimation()
        {
            //Debug.Log("Starting Attacking Animation");
            anim.SetBool("IsAttackingBuilding", true);
        }

        public void GlubeMad(){
            anim.SetBool("Angry", true);
        }
        public void GlubeCalm(){
            anim.SetBool("Angry", false);
        }

        public void GlubeWin()
        {
            anim.SetBool("IsAttackingBuilding", false);
            anim.SetBool("isGlubeWin", true);
            //Debug.Log("wining");
            //anim.Play("GlubeWin");
            //anim.SetBool("isGlubeWin", false);//maybe change isGlubeWin to trigger inseatd of bool
        }
    }
}