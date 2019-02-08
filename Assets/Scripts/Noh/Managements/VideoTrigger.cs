using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.EventSystems;
public class VideoTrigger : MonoBehaviour
{
    EventTrigger eventTrigger;
    VideoPlayer videoPlayer;
    [System.Serializable]
    public struct trigger { public string sound; public bool bgm; public float timer; }
    public trigger[] clipList;
    int triggerCount = 0;

    void quickSort(trigger[] arr, int left, int right)
    {
        if (arr.Length <= 0)
            return;
        int i = left, j = right;
        float pivot = arr[(left + right) / 2].timer;
        trigger temp;
        do
        {
            while (arr[i].timer < pivot)
                i++;
            while (arr[j].timer > pivot)
                j--;
            if (i <= j)
            {
                temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
                i++;
                j--;
            }
        } while (i <= j);

        /* recursion */
        if (left < j)
            quickSort(arr, left, j);

        if (i < right)
            quickSort(arr, i, right);
    }
    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        quickSort(clipList, 0, clipList.Length - 1);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (triggerCount < clipList.Length && videoPlayer.time >= clipList[triggerCount].timer)
        {
            if (!clipList[triggerCount].bgm)
                AudioManager.instance.PlayEffect(clipList[triggerCount].sound);
            else
                AudioManager.instance.PlayBGM(clipList[triggerCount].sound);
            triggerCount++;
        }
    }
}
