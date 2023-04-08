using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FMODUnity;


public class SoundEffect : MonoBehaviour
{
    [Tooltip("Select the Event you want.")]
    [SerializeField]EventReference eventRef;
    FMOD.Studio.EventInstance soundEvent;
    [SerializeField] bool is3D = true;

    //[SerializeReference]StudioEventEmitter emitter;

    void Awake()
    {
        soundEvent = RuntimeManager.CreateInstance(eventRef);
        RuntimeManager.AttachInstanceToGameObject(soundEvent, this.transform);
    }

    private void Update()
    {
        if (is3D) RuntimeManager.AttachInstanceToGameObject(soundEvent, this.transform);
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
            Debug.LogWarning("Sound Event Reference is Missing.");
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