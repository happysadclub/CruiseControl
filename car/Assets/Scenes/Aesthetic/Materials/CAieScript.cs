using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteInEditMode]
public class CAieScript : MonoBehaviour
{
    public Material Mat;
    PostProcessVolume m_Volume;
    ChromaticAberration m_ChromaticAbberation;

    void Start()
    {
        m_ChromaticAbberation = ScriptableObject.CreateInstance<ChromaticAberration>();
        m_ChromaticAbberation.enabled.Override(true);
        m_ChromaticAbberation.intensity.Override(1f);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_ChromaticAbberation);
    }


    private void Update()
    {
        m_ChromaticAbberation.intensity.value = Random.Range(0.5f, 1f);
    }
}
