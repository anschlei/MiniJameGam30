using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{

    [SerializeField]
    private int m_volume = 50;

    //
    // Sounds
    // 

    [SerializeField]
    private AudioClip m_shipEngine = null;

    [SerializeField]
    private AudioClip m_signalSound = null;

    [SerializeField]
    private AudioClip m_sucessDelivery = null;

    AudioSource m_backgroundAudioSource = null;
    AudioSource m_objectSound = null;

    // 
    // Musiks
    // 
    [SerializeField]
    private AudioClip m_backgroundMusik = null;


    public void SetVolume(int _volume)
    {
        m_volume = _volume;
    }

    private void Awake()
    {
        if(m_shipEngine != null)
        {
            m_objectSound = GetComponents<AudioSource>()[0];
            m_objectSound.clip = m_shipEngine;
            m_objectSound.Play();
        }

        if(m_signalSound != null)
        {
            m_backgroundAudioSource = GetComponents<AudioSource>()[1];
            m_backgroundAudioSource.clip = m_signalSound;
            m_backgroundAudioSource.Play();
        }
    }


}
