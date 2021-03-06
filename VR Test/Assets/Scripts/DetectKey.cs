using UnityEngine;
using System.Collections;

public class DetectKey : MonoBehaviour
{
    private float prevTime;
    private string letter;

    private GameObject rig;
    private socketScript s_script;

	public GameObject dispBox;
	private TextMesh dispTxt;

	public GameObject screentxt;
	private TextController txtcntrl;

	public GameObject Display_text;
	public GameObject Username_display;
	private UsernameDisplay displayscript;
	private TextMesh userdispTxt;

	private TextMesh targetDispMesh;


    void Start()
    {
        rig = GameObject.Find("LMHeadMountedRig");
        s_script = rig.GetComponent<socketScript>();
		dispTxt = dispBox.GetComponent<TextMesh> ();
		userdispTxt = Display_text.GetComponent<TextMesh> ();
		txtcntrl = screentxt.GetComponent<TextController> ();
		displayscript = Username_display.GetComponent<UsernameDisplay> ();
    }

	void Update()
	{
		if (displayscript.writeUsername) {
			targetDispMesh = userdispTxt;
		} else {
			targetDispMesh = dispTxt;
		}
	}
			

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Button" && Time.realtimeSinceStartup - prevTime > 0.3 && dispTxt.text.Length <= 11)
        {
            string letter = other.name;
            Debug.Log(letter);

            //GameObject rig = GameObject.Find("LMHeadMountedRig");
            //socketScript s_script = rig.GetComponent<socketScript>();
            s_script.message += letter;
			targetDispMesh.text += letter;
        }
		else if (other.tag == "BkspcButton" && Time.realtimeSinceStartup - prevTime > 0.1)
		{
			targetDispMesh.text = targetDispMesh.text.Remove(targetDispMesh.text.Length-1);
			s_script.message = s_script.message.Remove (s_script.message.Length - 1);
			Debug.Log ("bkspc");
		}
		else if (other.tag == "EnterButton" && Time.realtimeSinceStartup - prevTime > 0.3)
		{
			s_script.SendToServer(s_script.getJson(userdispTxt.text, dispTxt.text));
			Debug.Log (userdispTxt.text);
			txtcntrl.updateScreen_me (dispTxt.text);
			s_script.message = "";
			dispTxt.text = "";
			Debug.Log ("rtn");
		}
    }

	void OnTriggerExit(Collider other)
	{
		//if (other.tag == "Button" || other.tag == "BkspcButton" || other.tag == "EnterButton") {
			prevTime = Time.realtimeSinceStartup;
		//}
	}
}
