using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineAnimTrigger : MonoBehaviour {

    public PlayerController pc;
    public GameController gc;

    public Animator characterBodyAnim;
    public ParticleSystem cubeWaveParticleSystem;

    public bool lockIntoAnim;
    public bool lockIntoAnimRunner;

    public bool reachForCubeTrigger;
    public bool reachForCubeTriggerRunner;

    public bool cubeWaveTrigger;
    public bool cubeWaveTriggerRunner;

    // Update is called once per frame
    void Update () {
        if (lockIntoAnim && !lockIntoAnimRunner)
        {
            lockIntoAnim = false;
            pc.lockIntoAnim = true;
            characterBodyAnim.SetBool("canSwitch", false);
            lockIntoAnimRunner = true;
        }
        if (reachForCubeTrigger && !reachForCubeTriggerRunner)
        {
            reachForCubeTrigger = false;
            characterBodyAnim.Play("ReachingForCube");
            reachForCubeTriggerRunner = true;
        }
        if(cubeWaveTrigger && !cubeWaveTriggerRunner)
        {
            cubeWaveTrigger = false;
            cubeWaveParticleSystem.Play();
            cubeWaveTriggerRunner = true;
        }
    }
}
