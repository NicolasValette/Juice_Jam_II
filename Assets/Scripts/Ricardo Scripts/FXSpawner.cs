using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FXSpawner : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float DestroyDelay = 1f;

    [SerializeField]
    private Vector3 FXSpawnRotationOffset = new Vector3();

    [SerializeField]
    private Vector3 FXSpawnPositionOffset = new Vector3();

    public Vector3 SourceObjPosition;
    public Vector3 startPosition;

    [SerializeField]
    private AudioSource audioSource;

    [Serializable]
    public class FXData
    {
        public GameObject ParticlesPrefab;
        public List<AudioClip> SoundsList = new List<AudioClip>();

    }
    [SerializeField]
    private FXData FXs = new FXData();

    #endregion Variables

    public void InitSystem(bool lookAt)
    {
        if (lookAt) transform.LookAt(startPosition);
        InstantiateParticles();
        PlayAudio();
        //   UnityEngine.Debug.Break();
        Invoke("DestroySelf", DestroyDelay);
    }

    void InstantiateParticles()
    {

        // Instantiate the particle system at the impact position
        GameObject fx = Instantiate<GameObject>(FXs.ParticlesPrefab, transform.position/*+ FXSpawnPositionOffset*/, transform.rotation, transform);

        //   fx.transform.LookAt(startPosition);
        fx.transform.Rotate(FXSpawnRotationOffset.x, FXSpawnRotationOffset.y, FXSpawnRotationOffset.z);
        fx.transform.Translate(FXSpawnPositionOffset.x, FXSpawnPositionOffset.y, FXSpawnPositionOffset.z);
    }

    void PlayAudio()
    {
        if (FXs.SoundsList.Count > 0)
        {
            int ran = UnityEngine.Random.Range(0, FXs.SoundsList.Count - 1);

            audioSource.PlayOneShot(FXs.SoundsList[ran]);
        }
    }

    void DestroySelf() // Destroy the FX by itself
    {
        Destroy(gameObject);
    }
}
