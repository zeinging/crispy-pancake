using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FMODUnity;


public class SoundEffect : MonoBehaviour
{
    [Tooltip("Select the Sound Effect you want. Note: this script is not intended to play music.")]
    [SerializeField]EventReference eventRef;
    FMOD.Studio.EventInstance soundEvent;

    [SerializeField]

    //[SerializeReference]StudioEventEmitter emitter;

    void Awake()
    {
        soundEvent = RuntimeManager.CreateInstance(eventRef);
    }

    public void Stop()
    {
        soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Play()
    {
        try
        {
            soundEvent.start();
        } 
        catch
        {
            Debug.LogWarning("SFX Event Reference is Missing.");
        }
    }

    private void OnDestroy()
    {
        Stop();
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(SoundEffect))]
public class SoundEffectEditor : Editor
{
    SerializedProperty stop;
    SoundEffect script;

    private void OnEnable()
    {
        script = (SoundEffect)target;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Play()", GUILayout.ExpandWidth(true)))
        {
            script.Play();
        }

        if (GUILayout.Button("Stop()", GUILayout.ExpandWidth(true)))
        {
            script.Stop();
        }

        base.OnInspectorGUI();
    }
}
#endif