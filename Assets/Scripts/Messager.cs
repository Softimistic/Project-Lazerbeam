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
        _text[0].color = Color.yellow;
    }


    public string[][] getSpeechSubtitles(string speech)
    {
      //  UnityEngine.Debug.Log(speech);
        switch (speech)
        {
            case "Attention": //#
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
            case "Portal":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Quick, he's opening a portal..", "2" };
                subtitleLine[1] = new string[2] { "..get in there and finish him off Hank!", "2,5" };
                break;
            case "Sorry":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Oh god", "0,6" };
                subtitleLine[1] = new string[2] { "I thought those were tentacles", "1,8" }; 
                subtitleLine[2] = new string[2] { "Joe what the hell?!", "1.3" };
                break;
            case "YouShotMyDicks":
                _text[0].color = Color.red;
                subtitleLine = new string[4][];
                subtitleLine[0] = new string[2] { "[cries in pain]", "1,3" };
                subtitleLine[1] = new string[2] { "You insane man!", "1,5" };  
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
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Well come and get me then", "2" }; //#Redo
                subtitleLine[1] = new string[2] { "YOU don’t even know where I am", "2,5" };
                subtitleLine[2] = new string[2] { "I don’t even know where I am...", "2" };
                break;
            case "Hank5":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "President Biden?!", "1,5" };
                break;
            case "Hank6":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Sorry i can't help you", "1,5" }; //#redo
                subtitleLine[1] = new string[2] { "I'm just an accountant..", "1" };
                break;
            case "Hank7":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "O God, i am going to die!", "2" };
                break;

            case "Joe1":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Sorry to interrupt, gentlemen", "2" };
                break;
            case "Joe2":
                subtitleLine = new string[6][];
                subtitleLine[0] = new string[2] { "That's right", "1,5" };
                subtitleLine[1] = new string[2] { "The United States of America needs you Hank!", "3" };
                subtitleLine[2] = new string[2] { "We have detected a fleed of extra-terrestrial beings..", "3" };
                subtitleLine[3] = new string[2] { "..that are on their way to earth!", "2" };
                subtitleLine[4] = new string[2] { "Our forces cannot make it in time", "2" };
                subtitleLine[5] = new string[2] { "You are the only one that is close enough to stop them Hank!", "5" };
                break;
            case "Joe3":
                subtitleLine = new string[10][];
                subtitleLine[0] = new string[2] { "Listen Fat!", "1" };
                subtitleLine[1] = new string[2] { "You either do as you are told..", "1" };
                subtitleLine[2] = new string[2] { "or i will have you arrested for stealing that ship!", "1" };
                subtitleLine[3] = new string[2] { "Somewhere in that alien fleed, there should be an alien commander..", "1" };
                subtitleLine[4] = new string[2] { "who is carrying a wormhole generator", "1" };
                subtitleLine[5] = new string[2] { "If he activates that generator he will be able to bring the rest of his alien fleed here!", "1" };
                subtitleLine[6] = new string[2] { "There will be millions swarming us", "1" };
                subtitleLine[7] = new string[2] { "My forces have no chance against numbers like that", "1" };
                subtitleLine[8] = new string[2] { "So I need you to get in there..", "1" };
                subtitleLine[9] = new string[2] { "..and take out that commander before he can activate his wormhole generator", "1" };
                break;
            case "Show1":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Turn that ship around right now thief!", "1" }; //#redo
                subtitleLine[1] = new string[2] { "That is my ship!", "1" };
                break;
            case "Show2":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "You haven't even signed the papers..", "2" };
                subtitleLine[1] = new string[2] { "I could have you arrested!", "1,5" };
                subtitleLine[2] = new string[2] { "Hell, i could sue you for millions just for what you did to my roof!", "4,5" }; //#redo
                break;
            case "Show3":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "I will get you sooner or later Hank!!", "2" };
                subtitleLine[1] = new string[2] { "I will get you!!!", "1,5" };
                subtitleLine[2] = new string[2] { "you son of a @#@#@$!", "1,5" };
                break; 
            //New Joe
            case "DisableForcefield":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Find a way to disable that forcefield Hank!", "2,7" };
                break;
            case "Minefield":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Be careful Hank", "1,2" };
                subtitleLine[1] = new string[2] { "You’re about to enter a minefield!", "1,8" };
                break;
            case "KillCyclops":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Kill that Cyclops, Hank!", "2" };
                break;
            case "CyclopsDone":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Great Job Hank!", "1,5" };
                subtitleLine[1] = new string[2] { "That Cyclops won't be bothering us no more", "3,5" };
                subtitleLine[2] = new string[2] { "I have lost track of the Alien commander however", "3" };
                break;
            case "FoundCommander":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Well, guess we found the alien commander.", "3" };
                subtitleLine[1] = new string[2] { "Enter that planet’s atmosphere and hunt that son of a bitch down", "4,3" };
                subtitleLine[2] = new string[2] { "Please..", "1" };
                break;
            case "GreatJobHank":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Great job Hank", "1,5" };
                subtitleLine[1] = new string[2] { "The world, and most importantly, the United States of America are in your debt", "5,5" };
                subtitleLine[2] = new string[2] { "However, I have one final task for you", "3" };
                break;
            case "GoCrash":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "I need you to crash your ship and die", "2,2" };        
                break;
            case "EndThisHank":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Oops", "0,6" };
                subtitleLine[1] = new string[2] { "Shoot him in the head and end this Hank", "2,7" };
                subtitleLine[2] = new string[2] { "I've had enough of this guy", "1,8" };
                break;
            case "IDoItForYou":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Then I’ll do it for you", "1,3" };
                break;
            case "SorryHank":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "I’m sorry Hank", "1,2" };
                break;
            case "KillHim":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "We don't have much time Hank", "2" };
                subtitleLine[1] = new string[2] { "Kill him before he can bring his fleet through that wormhole", "4,5" };
                subtitleLine[2] = new string[2] { "Target his tentacles, they are extremely sensitive!", "3,6" };
                break;
            case "YouKnowTooMuch":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "I’m sorry Hank, but you simply know too much", "3" };
                subtitleLine[1] = new string[2] { "The public can never know about this attempted invasion Hank", "3" };
                subtitleLine[2] = new string[2] { "..the consequences would be disastrous", "3" };
                break;
            //New floopy
            case "Contacted":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "We are being contacted by that planet in front of us Hank", "3" };
                break;
            case "Count1":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "1", "0,5" };
                break;
            case "Count2":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "2", "0,5" };
                break;
            case "Count3":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "3", "0,5" };
                break;
            case "Goobye":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Goodbye Hank", "2" };
                break;
            case "SelfDestruct":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Selft destruct countdown activated", "2" };
                break;
            case "SomethingBig":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "I am detecting something big, and it's comming our way", "3" };
                break;
            //new Hank
            case "ButHow":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "But how?", "1" };
                break;
            case "Commander":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "That must be him, that must be the commander!", "3" };
                break;
            case "Geez":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Geez, he's still not dead?", "2" };
                break;
            case "HelpMe":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Oh god", "1" };
                subtitleLine[1] = new string[2] { "Joe! Help me, what do i do?!", "2" };
                break;
            case "ISavedHumanity":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "I did it", "1" };
                subtitleLine[1] = new string[2] { "I saved humanity", "1,5" };
                break;
            case "ItsOver":            
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "It's over, i've got you!", "2" };
                break;
            case "NotDoingThat":
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "what?, NO!", "1" };
                subtitleLine[1] = new string[2] { "I'M NOT GOING TO DO THAT!", "2" };
                break;
            case "No":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "NO!", "1" };
                break;
            case "ReadyForAnything":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "I'm ready for anything, mr.President!", "2,5" };
                break;
            case "YouToo":
                _text[0].color = Color.yellow; //No idea why this one stays read
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "y-you too..", "1" };
                break;
            case "YouWillPayJoe":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "YOU WILL PAY FOR THIS JOE!!", "2" };
                break;
            //New AllienCommander
            case "Trap":
                _text[0].color = Color.red;
                subtitleLine = new string[4][];
                subtitleLine[0] = new string[2] { "Ha! Not so fast human!", "2" };
                subtitleLine[1] = new string[2] { "You've fallen right into my trap!", "2" };
                subtitleLine[2] = new string[2] { "May I introduce you to the most feared member of my fleet..", "4" };
                subtitleLine[3] = new string[2] { "THE CYCLOPS!", "1,2" };
                break;
            case "Sayonara":
                _text[0].color = Color.red;
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Keep him busy Cyclops. Meanwhile I’m out of here. ", "3" };
                subtitleLine[1] = new string[2] { "Sayonara humans!", "1,7" };
                break;
            case "NothingToSee":
                _text[0].color = Color.red;
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Nothing to see here, move along.", "3" };
                break;
            case "StopBothering":
                _text[0].color = Color.red;
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Stop bothering me human!", "1,8" };
                break;
            case "Pay":
                _text[0].color = Color.red;
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "YOU WILL PAY FOR THIS!!", "1,4" };
                break;
            case "FarFromOver":
                _text[0].color = Color.red;
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "It’s far from over. I’m activating my wormhole generator!", "4" };
                subtitleLine[1] = new string[2] { "Soon you will face the might of my infinite fleet!", "3" };
                break;
            case "PrepareToDie":
                _text[0].color = Color.red;
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Enough Talking, Prepare to die!", "3" };
                break;
            case "Penis": //#probably is not being used
                _text[0].color = Color.red;
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Ow my penises!", "1,5" };
                subtitleLine[1] = new string[2] { "You shot off my penises!", "2" };
                break;
            //Space colonist
            case "ColonistHelp":
                subtitleLine = new string[3][];
                subtitleLine[0] = new string[2] { "Aaaaah help us we’re being attacked by a..", "2,5" };
                subtitleLine[1] = new string[2] { "..giant green tentacle monster with a satan face! ", "2,5" };
                subtitleLine[2] = new string[2] { "God would somebody please help us! ", "2" };
                break;
            case "CyclopsNoise1":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "[Cylops noises]", "1,5" };              
                break;
            case "CyclopsNoise2":
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "[Angrier Cyclops noises]", "2" };
                break;
            //NewSpacelevelInterrups
            case "PressSpacebar": //Ai 
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Enemies detected", "1,2" };
                subtitleLine[1] = new string[2] { "press [SPACEBAR] to use your ship's cannon", "2" };
                break;
            case "PressCtrlOrShift": //Ai
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Meteorite cluster detected, try not to fly into them!", "3" };
                subtitleLine[1] = new string[2] { "You can press [CTRL] to slow down or [SHIFT] to use boost.", "3" };
                break;
            case "DodgeRocketLauncher": //Joe
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Hank, you should fly more to the left to", "2,7" };
                subtitleLine[1] = new string[2] { "dodge the rocket launchers in front of you", "2" };
                break;
            case "GoToPlanet": //Joe #not needed
                subtitleLine = new string[1][];
                subtitleLine[0] = new string[2] { "Hank, i need you to fly to that planet in front of you", "3,5" };
                break;
            case "GrabItem": //Joe
                subtitleLine = new string[2][];
                subtitleLine[0] = new string[2] { "Hank, that enemy is holding an item.", "3" };
                subtitleLine[1] = new string[2] { "Try to grab it after you eliminate him!", "2,5" };
                break;

            default:
                subtitleLine = new string[0][];
                break;
           
        }
        return subtitleLine;
    }
}