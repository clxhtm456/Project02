using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{
    public static string nextScene;
    public EasyTween[] loadingImage;
    public float slideTimer;
    private float mSliderTimer;
    public int recentImage;
    private int imageLength;
    public Button nextButton;
    public bool nextTrigger;
    public Image fadeOut;

    // Use this for initialization
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Start()
    {
        StartCoroutine(LoadScene());
        mSliderTimer = slideTimer - 2.0f;
        imageLength = loadingImage.Length;
        for (int i = 1; i < imageLength; i++)
        {
            loadingImage[i].OpenCloseObjectAnimation();
        }
        recentImage = 1;
        AudioManager.instance.PlayBGM("LoadingBGM");
    }
    void ImageSwitch()
    {
        int beforeImage = recentImage - 1 < 0 ? imageLength - 1 : recentImage - 1;
        loadingImage[recentImage].OpenCloseObjectAnimation();
        loadingImage[beforeImage].OpenCloseObjectAnimation();
    }
    // Update is called once per frame
    void Update()
    {
        if (mSliderTimer < Time.deltaTime)
        {
            ImageSwitch();
            recentImage = recentImage < imageLength - 1 ? recentImage + 1 : 0;
            mSliderTimer = slideTimer;
        }
        else
            mSliderTimer -= Time.deltaTime;
    }
    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        while (!op.isDone)
        {
            if (nextTrigger == true && fadeOut.color.a >= 0.9f)
                op.allowSceneActivation = true;
            if ((nextButton.gameObject.activeInHierarchy == false && op.progress >= 0.9f) && (SaveListManager.instance.playerdata.newGame == false || recentImage == 0))
                nextButton.gameObject.SetActive(true);
            yield return null;
        }
    }
    public void SetNextTrigger(bool _value)
    {
        fadeOut.gameObject.SetActive(true);
        nextTrigger = _value;
        fadeOut.GetComponent<EasyTween>().OpenCloseObjectAnimation();
        AudioManager.instance.FadeOutBGM();
    }
}