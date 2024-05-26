using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{

    [SerializeField, Range(0.0f, 1.0f)]
    private float m_volume = 1.0f;

    //HACKY
    public static int rescuedPersons = 0;

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


    public void SetVolume(float _volume)
    {
        m_volume = _volume;
        m_objectSound.volume = m_volume;
        m_backgroundAudioSource.volume = m_volume;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        //if(m_shipEngine != null)
        //{
        //    m_objectSound = GetComponents<AudioSource>()[0];
        //    m_objectSound.volume = m_volume;
        //    m_objectSound.clip = m_shipEngine;
        //    m_objectSound.Play();
        //}

        if(m_backgroundMusik != null)
        {
            m_backgroundAudioSource = GetComponents<AudioSource>()[1];
            m_backgroundAudioSource.volume = m_volume;
            m_backgroundAudioSource.clip = m_backgroundMusik;
            m_backgroundAudioSource.Play();
        }
    }


}
