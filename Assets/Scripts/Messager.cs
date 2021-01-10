using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Messager : MonoBehaviour
{
    public bool DisablePausing;
    private AudioSource _audioSource;
    private Image _frame;
    private Image _portrait;
    private bool _messageActive; //Wether or not a message is currently playing
                         
    private Text[] _text;

    private string[][] subtitleLine = new string[1][];

    void Start()
    {
        _frame = GetComponent<Image>();
        _portrait = GetComponentsInChildren<Image>()[1];
        _audioSource = GetComponent<AudioSource>();
        _text = this.GetComponentsInChildren<Text>();
     
    }

    void FixedUpdate()
    {
        if ((_messageActive && !_audioSource.isPlaying) || _messageActive && (Input.GetKeyDown("x") && !DisablePausing))
        {
            StopMessage();
        }
    }

    /// <summary>
    /// Show portrait and play message audio
    /// </summary>
    public void StopMessage()
    {
        _messageActive = false;
        _frame.enabled = false;
        _portrait.enabled = false;
        _audioSource.Stop();
    }
    public void PlayMessage(Sprite portrait, AudioClip audio)
    {
        _messageActive = true;
        _frame.enabled = true;
        _portrait.enabled = true;
        _portrait.sprite = portrait;//Resources.Load<Sprite>(portraitFileName);
        _audioSource.clip = audio;//Resources.Load<AudioClip>(audioFileName);
        _audioSource.Play();

        StartCoroutine(SubtitleSequence(_audioSource.clip.name));
    }

    public bool IsMessageActive()
    {
        return _messageActive;
    }



    IEnumerator SubtitleSequence(string speech = "")
    {
        subtitleLine = getSpeechSubtitles(speech);
        for (int i = 0; i < subtitleLine.Length; i++)
        {
            if (IsMessageActive()) 
            {
                _text[0].text = subtitleLine[i][0];
                yield return new WaitForSeconds(float.Parse(subtitleLine[i][1]));
                _text[0].text = null;
            }
        }
    }


    public string[][] getSpeechSubtitles(string speech)
    {
        // UnityEngine.Debug.Log(speech);
        switch (speech)
        {
            case "Attention":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Seems like you're really getting his attention, Hank", "2" };
                break;
            case "ComeOnMan":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Come on man", "1" };
                subtitleLine[1] = new string[2] { "Show those alliens who's boss", "2" };
                break;
            case "Foolish":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "You'll never take me alive", "1,3" };
                subtitleLine[1] = new string[2] { "foolish human", "1" };
                break;
            case "Insect":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "What is this human doing here?", "2" };
                subtitleLine[1] = new string[2] { "..take care of this insect for me boys!", "2,5" };
                break;
            case "Pay":
                subtitleLine = new string[6][];
                subtitleLine[0] = new string[2] { "You will pay for this human!", "2" };
                subtitleLine[1] = new string[2] { "i'm redirecting all of my shield energy", "2" };
                subtitleLine[2] = new string[2] { "to my robot penis", "1,8" };
                subtitleLine[3] = new string[2] { "You cannot stand against the #", "2" };
                subtitleLine[4] = new string[2] { "# charged robot #", "1,5" };
                subtitleLine[5] = new string[2] { "foolish insect! #", "1" };
                break;
            case "Portal":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Quick, he's opening a portal..", "2" };
                subtitleLine[1] = new string[2] { "..get in there and finish him off Hank!", "2,5" };
                break;
            case "Sorry":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Oh god no", "0,8" };
                subtitleLine[1] = new string[2] { "I thought those were tentacles", "1,6" };
                subtitleLine[2] = new string[2] { "Oh god, i am so sorry!", "1.5" };
                break;
            case "owie":
                subtitleLine[0] = new string[2] { "[cries in pain]", "1,3" };
                subtitleLine[1] = new string[2] { "You insane man!", "1" };
                subtitleLine[2] = new string[2] { "You shot my dicks off", "2" };
                subtitleLine[3] = new string[2] { "What a horrible person must you be!", "2,2" };
                break;

            //Below here is briefing1Dialog Folder

            case "Floopy1":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Good morning Hank...", "1,5" };
                subtitleLine[1] = new string[2] { "please wake up, we are being contacted", "2,5" };
                break;
            case "Floopy2":
                subtitleLine = new string[4][];
                subtitleLine[0] = new string[2] { "My name is floopy", "1,5" };
                subtitleLine[1] = new string[2] { "I am your ship's AI system", "2" };
                subtitleLine[2] = new string[2] { "We are being contacted", "1,5" };
                subtitleLine[3] = new string[2] { "Do you wish to answer?", "1,5" };
                break;
            case "Hank1":
                subtitleLine = new string[4][];
                subtitleLine[0] = new string[2] { "Quiet, i'm sleeping..", "1,5" };
                subtitleLine[1] = new string[2] { "Wait a second..", "1" };
                subtitleLine[2] = new string[2] { "Who was that?", "1" };
                subtitleLine[3] = new string[2] { "I thought i was alone.", "1,5" };
                break;
            case "Hank2":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Oké", "0,4" };
                break;
            case "Hank3":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "You just said it was mine, didn't you?", "2" };
                subtitleLine[1] = new string[2] { "I won this shit fair and square!", "2" };
                break;
            case "Hank4":
                subtitleLine[0] = new string[2] { "#", "1" };
                break;
            case "Hank5":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "President Biden", "1,5" };
                break;
            case "Hank6":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Sorry i can't # help you", "1,5" };
                subtitleLine[1] = new string[2] { "I am just an accountant..", "1" };
                break;
            case "Hank7":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "O God, i am going to die!", "2" };
                break;

            case "Joe1":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Sorry to interrupt, gentlement", "2" };
                break;
            case "Joe2":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "#", "1" };
                break;
            case "Joe3":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "#", "1" };
                break;
            case "Show1":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "#", "1" };
                break;
            case "Show2":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "You haven't even signed the papers..", "2" };
                subtitleLine[1] = new string[2] { "I could have you arrested!", "1,5" };
                subtitleLine[2] = new string[2] { "Hell, i could sue you for millions just for what you did to my roof!", "3,5" };
                break;
            case "Show3":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "I will get you sooner or later Hank!!", "2" };
                subtitleLine[1] = new string[2] { "I will get you!!!", "1,5" };
                subtitleLine[2] = new string[2] { "you son of a #!", "1,5" };
                break;
            default:
                break;
        }
        return subtitleLine;
    }
}