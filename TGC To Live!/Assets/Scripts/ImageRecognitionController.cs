using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionController : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager trackedImageManager;

    [System.Serializable]
    public struct NamedPrefab
    {
        public string name;
        public GameObject prefa;
    }

    [SerializeField]
    List<NamedPrefab> namedPrefabs;

    Dictionary<string, GameObject> pokemons = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (var namedPrefab in namedPrefabs)
        {
            var pokemon = Instantiate(namedPrefab.prefa);
            pokemon.SetActive(false);
            pokemons.Add(namedPrefab.name, pokemon);
        }
    }

    void OnEnable()
    {
        trackedImageManager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    private void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var trackedImage in args.removed)
            DeleteImage(trackedImage.Value);
        foreach (var trackedImage in args.added)
            UpdateImage(trackedImage);
        foreach (var trackedImage in args.updated)
            UpdateImage(trackedImage);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        foreach (var namedPrefab in namedPrefabs)
        {
            if (namedPrefab.name == trackedImage.referenceImage.name)
            {
                pokemons[namedPrefab.name]
                    .SetActive(
                        trackedImage.trackingState
                            == UnityEngine.XR.ARSubsystems.TrackingState.Tracking
                    );
                pokemons[namedPrefab.name].transform.position = trackedImage.pose.position;
                pokemons[namedPrefab.name].transform.rotation = trackedImage.pose.rotation;
                break;
            }
        }
    }

    private void DeleteImage(ARTrackedImage trackedImage)
    {
        foreach (var namedPrefab in namedPrefabs)
        {
            if (namedPrefab.name == trackedImage.referenceImage.name)
            {
                pokemons[namedPrefab.name].SetActive(false);

                break;
            }
        }
    }

    void OnDisable()
    {
        trackedImageManager.trackablesChanged.RemoveAllListeners();
    }
}
