using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class DialougeManager : MonoBehaviour {

	public bool frequencyFound;
	public static DialougeManager instance;
	public AudioClip audioclip;
	public FirstPersonController astrid;
	public HartoTuningController harto;
	//public List<AudioClip> dialogue;
	public Dictionary<string, AudioClip> dialogue;

	private AudioSource audioSource;
	private AudioSource hartoAudioSource;
	public AudioSource getAudioSource() { return audioSource; }
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}

		if (astrid == null)
		{
			astrid = GameObject.Find ("Astrid").GetComponent<FirstPersonController>();
		}

		// if (harto == null) {
		// 	harto = GameObject.Find ("TemporaryHARTO").GetComponent<HartoTuningController>();
		// }

		dialogue = new Dictionary<string, AudioClip> ();

		audioSource = GetComponent<AudioSource> ();
		//hartoAudioSource = harto.GetComponent<AudioSource> ();

		

	}

	void playVoiceOver(string filename, float volume) {

		if (filename.Contains ("HARTO")) {
			if (!hartoAudioSource.isPlaying) {
				hartoAudioSource.PlayOneShot (dialogue [filename], volume);
			}
		}
		else {
			if (!audioSource.isPlaying) {
				audioSource.PlayOneShot (dialogue [filename], volume);
			}
		}
	}

	public void loadVOFile (string character, string dialogueType,string filename ,string scene)
	{
		audioclip = Resources.Load<AudioClip> ("Audio/VO/" + character + "/" + dialogueType + "/" + filename);
		dialogue.Add (filename, audioclip);
	}

	void loadScenesDialogue()
	{
		loadVOFile ("Astrid", "Gibberish", "Astrid VO1_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO1repeated_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO2_Curious_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO2_Happy_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO2_Sad_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO3_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO4_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO5_Gib", "Protoype");
		loadVOFile ("Astrid", "Gibberish", "Astrid VO6_Gib", "Protoype");

		loadVOFile ("Astrid", "HARTO", "Astrid VO1_HARTO", "Protoype");
		loadVOFile ("Astrid", "HARTO", "Astrid VO2_Curious_HARTO", "Protoype");
		loadVOFile ("Astrid", "HARTO", "Astrid VO2_Happy_HARTO", "Protoype");
		loadVOFile ("Astrid", "HARTO", "Astrid VO2_Sad_HARTO", "Protoype");
		loadVOFile ("Astrid", "HARTO", "Astrid VO3_HARTO", "Protoype");
		loadVOFile ("Astrid", "HARTO", "Astrid VO4_HARTO", "Protoype");
		loadVOFile ("Astrid", "HARTO", "Astrid VO5_HARTO", "Protoype");

		loadVOFile ("Mali", "Gibberish", "Mali VO1_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO2_Curious_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO2_Happy_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO2_Sad_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO3_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO4_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO5_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO6_Gib", "Protoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO7_Gib", "Protsoype");
		loadVOFile ("Mali", "Gibberish", "Mali VO8_Gib", "Protoype");

		loadVOFile ("Mali", "HARTO", "Mali VO1_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO2_Curious_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO2_Happy_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO2_Sad_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO3_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO4_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO5_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO6_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO7_HARTO", "Protoype");
		loadVOFile ("Mali", "HARTO", "Mali VO8_HARTO", "Protoype");

	}

	public IEnumerator initDialogue(string npcName) {
		if (npcName == "Mali") {
			//	Loads Dialogue voice over files
			//	Add parameters to loadScenesDialouge method for more robustness
			loadScenesDialogue ();
			//	Begins the dialogue
			yield return StartCoroutine(startPrototypeDialogue());
		}
	}

	IEnumerator waitForRightFrequency()
 	{
     	while (harto.currentfrequency != GameManager.gm.mali.myFrequency)
        	 yield return null;

		 harto.setHARTOActiveTo (true, false, false);
	 }

	 IEnumerator waitForEmotionalInput()
 	{
     	while (!harto.isHappy && !harto.isCurious && !harto.isSad)
        	 yield return null;

		 harto.setHARTOActiveTo (true, false, false);
	 }

	IEnumerator startPrototypeDialogue() {
		playVoiceOver ("Astrid VO1_Gib", 0.65f);
		//	Tell Game Manager to tell Mali to do her gesture animation
		//	If gesture animation is complete tell Game Manager to tell
		//	Astrid they can now Activate and tune HARTO

		GameManager.gm.mali.startGestureAnimation();	
		if (GameManager.gm.mali.gestureAnimationDone){
				//harto.setHARTOActiveTo(true, true,false);
		}
		//	Wait for user input
		yield return StartCoroutine(waitForRightFrequency());
			
		playVoiceOver ("Astrid VO1repeated_Gib", 0.2f);
		yield return new WaitForSeconds(2.0f);
		playVoiceOver ("Astrid VO1_HARTO", 1.3f);
				
		yield return new WaitForSeconds(2.0f);
		
		//	Mali's dialogue
		playVoiceOver ("Mali VO1_Gib", 0.2f);
		yield return new WaitForSeconds(2.0f);
		playVoiceOver ("Mali VO1_HARTO", 1.3f);

		yield return new WaitForSeconds(8.0f);

		//harto.setHARTOActiveTo(true, false, true);
		
		//	Wait for input
		yield return StartCoroutine(waitForEmotionalInput());
		
		// if(harto.isHappy){
		// 	playVoiceOver("Astrid VO2_Happy_Gib", 0.5f);
		// 	yield return new WaitForSeconds(2.0f);
		// 	playVoiceOver("Astrid VO2_Happy_HARTO", 1.3f);

		// 	yield return new WaitForSeconds(5.0f);

		// 	playVoiceOver("Mali VO2_Happy_HARTO", 1.3f);
		// 	playVoiceOver("Mali VO2_Happy_Gib", 0.5f);
		// }
		// else if (harto.isCurious) {
		// 	playVoiceOver("Astrid VO2_Curious_HARTO", 1.3f);
		// 	playVoiceOver("Astrid VO2_Curious_Gib", 0.5f);

		// 	yield return new WaitForSeconds(2.0f);

		// 	playVoiceOver("Mali VO2_Curious_HARTO", 1.3f);
		// 	playVoiceOver("Mali VO2_Curious_Gib", 0.5f);
		// }
		// else if (harto.isSad) {
		// 	playVoiceOver("Astrid VO2_Sad_HARTO", 1.3f);
		// 	playVoiceOver("Astrid VO2_Sad_Gib", 0.5f);

		// 	yield return new WaitForSeconds(4.0f);

		// 	playVoiceOver("Mali VO2_Sad_HARTO", 1.3f);
		// 	playVoiceOver("Mali VO2_Sad_Gib", 0.5f);
		// }

		//	sets all emotions to false
		//harto.defaultHARTOEmotions();

		yield return new WaitForSeconds(10.0f);

		playVoiceOver("Mali VO3_HARTO", 1.3f);
		playVoiceOver("Mali VO3_Gib", 0.5f);

		yield return new WaitForSeconds(30.0f);
		
		playVoiceOver("Astrid VO3_HARTO", 1.3f);
		playVoiceOver("Astrid VO3_Gib", 0.5f);

		yield return new WaitForSeconds(3.0f);

		playVoiceOver("Mali VO4_HARTO", 1.3f);
		playVoiceOver("Mali VO4_Gib", 0.5f);

		yield return new WaitForSeconds(32.0f);
		
		playVoiceOver("Astrid VO4_HARTO", 1.3f);
		playVoiceOver("Astrid VO4_Gib", 0.5f);

		yield return new WaitForSeconds(6.0f);

		playVoiceOver("Mali VO5_HARTO", 1.3f);
		playVoiceOver("Mali VO5_Gib", 0.5f);

		yield return new WaitForSeconds(5.0f);
		
		playVoiceOver("Astrid VO5_HARTO", 1.3f);
		playVoiceOver("Astrid VO5_Gib", 0.5f);

		yield return new WaitForSeconds(8.0f);
		
		playVoiceOver("Astrid VO6_Gib", 0.5f);

		yield return new WaitForSeconds(4.0f);

		playVoiceOver("Mali VO7_HARTO", 1.3f);
		playVoiceOver("Mali VO7_Gib", 0.5f);

		astrid.setTalking(false);
	}
	

	
	// Update is called once per frame
	void Update () {

		
	}
}
