using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private Animator animator;
    private int tempAnimState;//이전애니메이션
    private int animationState;
    /*
     * 0:기본상태
     * 1:망치질
     * 2:무두질
     * 3:컴퓨터
     * 4:휴식
    */
    private bool working = false;
    private bool canWork = true;
    public int[] playerState;
    public GameObject actionObject = null;
    private UISpeakBlank speakBlank;
    private AudioSource audioSource;
    public AudioClip hammerSound;
    public AudioClip leatherSound;
    public UISpeakBlank SpeakBlank
    {
        get
        {
            return speakBlank;
        }
    }
    private Vector3[] AnimPosition =
        {new Vector3(-12f, -172f, 0.0f),
        new Vector3(-97f, -150f, 0.0f),
        new Vector3(-106f, -259f, 0.0f),
        new Vector3(2.1f, 68.9f, 0.0f),
        new Vector3(306f, -114f, 0.0f)
    };
    public int AnimationState
    {
        set
        {
            tempAnimState = animationState;
            animationState = value;
            if(animator)
                animator.SetInteger("AnimState", value);
            GetAnimPos(value);

            //애니메이션 재생
        }
    }
    public void GetAnimPos(int _temp)
    {
         transform.localPosition = AnimPosition[_temp];
    }
    public int TempAnimState
    {
        get
        {
            return tempAnimState;
        }
    }
    public bool Working
    {
        set
        {
            working = value;
            if (!working)
                AnimationState = 0;
        }
        get { return working; }
    }
    public GameObject ActionObject
    {
        get
        {
            return actionObject;
        }
        set
        {
            actionObject = value;
            if(actionObject != null)
            {
                try
                {
                    actionObject.transform.SendMessage("ButtonAction");
                }
                catch
                {
                    Debug.Log("버튼액션없음");
                }
            }

        }
    }
    new private void Awake()
    {
        AnimationState = 0;
        working = false;
        canWork = true;
        speakBlank = FindObjectOfType<UISpeakBlank>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        playerState = new int[5];
        playerState = SaveListManager.instance.playerState;
    }
	
	// Update is called once per frame
	void Update () {
    }
    public void AnimHammerSound()
    {
        audioSource.clip = hammerSound;
        audioSource.Play();
    }
    public void AnimLeatherSound()
    {
        audioSource.clip = leatherSound;
        audioSource.Play();
    }
}
