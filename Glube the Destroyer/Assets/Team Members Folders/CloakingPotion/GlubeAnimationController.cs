using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Team_Members_Folders.CloakingPotion
{

    public class GlubeAnimationController : MonoBehaviour
    {
        public Animator anim;

        public float agentSpeedSLider = 0;

        public NavMeshAgent myAgent;

        private void Start(){
            //anim.SetFloat("AgentSpeed", agentSpeedSLider);

        }

        void Update(){
            agentSpeedSLider = Mathf.Clamp01(agentSpeedSLider);
            float temp = Mathf.Clamp01(myAgent.velocity.magnitude);
            anim.SetFloat("AgentSpeed", temp);
            
        }

        public void StartWalkingAnimationdDirectedTowards(Vector3 targetDirection)

        {
            //anim.CrossFade("Walk", 0.25f);
            //anim.Play("Walk");
            //Vector3 targetDirection = Quaternion.LookRotation();
        }

        public void StartAttackingAnimationDirectedAt()
        {
        }
    }
}