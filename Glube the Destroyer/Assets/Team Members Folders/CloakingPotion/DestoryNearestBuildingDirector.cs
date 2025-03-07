//#nullable enable

using Assets.Team_Members_Folders.CloakingPotion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class DestoryNearestBuildingDirector : MonoBehaviour
{
    public List<Transform> buildings;

    public GameObject buildingsParent;
    private GlubeAnimationController animController;

    private bool needsToFindNextBuilding = true;

    private void Start()
    {
        animController = GetComponent<GlubeAnimationController>();

        for(int i = 0; i < buildingsParent.transform.childCount; i++){
            //Debug.Log("" + i);
            buildings.Add(buildingsParent.transform.GetChild(i));
            //buildings[i] = buildingsParent.transform.GetChild(i);
        }
    }

    private Transform FindClosestBuilding()
    {
        if (buildings == null) return null;

        float? smallestDistance = null;
        Transform closestVector3 = null;
        foreach (Transform building in buildings)
        {
            if (building == null) continue;

            float distance = Vector3.Distance(building.position, this.transform.position);

            if (distance < smallestDistance || closestVector3 == null)
            {
                smallestDistance = distance;
                closestVector3 = building;
            }
        }

        return closestVector3;
    }

    // Update is called once per frame
    private void Update()
    {
        if(!GameplayControllerScript.instance.GlubeAngry){

            GlubeAttacksBuildings();

        }

    }

    public void GlubeAttacksBuildings(){
        


        if (!needsToFindNextBuilding)
        {
            // If we don't need to find the next building, simply return.
            return;
        }

        Transform closestVector = FindClosestBuilding();

        if (closestVector == null)
        {
            // Glube has destroyed all buildings!
            // Level Fail!~!
            // TODO: IMPLEMENT LEVEL FAIL!
            //Debug.Log("Level Failed. All Buildings Destroyed");
            if (animController != null)
            {
                animController.GlubeWin();
            }
        }
        else
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.isStopped = false;
            agent.destination = ((Transform)closestVector).position;
            needsToFindNextBuilding = false;
        }

    }

    public void HandleCompletedBulidingDestruction()
    {
        //needsToFindNextBuilding = true;
        StartCoroutine(FindDelay());

        //Debug.Log("Updated needs to find next building");
    }

    public void HandleStopAgentAndAnimateAttacking()

    {
        // Stops the agent
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.velocity = agent.velocity * 0.1f;
        StartCoroutine(AttackDelay());
        //if (animController != null)
        //{
            //animController.StartAttackingAnimation();
        //}
    }

    public void GlubeAttacksPlayer(){

        StartCoroutine(GlubeAttackDuration(5f));
    }

    private IEnumerator GlubeAttackDuration(float t){

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.velocity = agent.velocity * 0.1f;
        animController.GlubeMad();

        

        while(t > 0){
        Vector3 PlayerDir = GameplayControllerScript.instance.PlayerPlane.transform.position - transform.position;
        Quaternion target = Quaternion.LookRotation(PlayerDir, Vector3.up);
        //transform.rotation = Quaternion.Lerp(transform.rotation, target, 500 * Time.deltaTime);
        target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        transform.rotation = target;
        t -= Time.deltaTime;
        yield return null;
        }

        yield return new WaitForSeconds(3f);
        GameplayControllerScript.instance.GlubeAngry = false;
        animController.GlubeCalm();
        agent.isStopped = false;
    }

    public void GlubeDefeated(){
        StopAllCoroutines();
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.velocity = agent.velocity * 0.1f;

        animController.anim.SetBool("IsAttackingBuilding", false);
        animController.anim.CrossFade("GLube Idle And Walk", 0.1f);
    }

    private IEnumerator FindDelay(){

        yield return new WaitForSeconds(2.5f);
        //AudioManager.instance.BuildingExplodeEnd();//reset building emitter after finding a new building
        needsToFindNextBuilding = true;

    }

    private IEnumerator AttackDelay(){

        yield return new WaitForSeconds(1.2f);
        if (animController != null)
        {
            animController.StartAttackingAnimation();
        }

    }

}