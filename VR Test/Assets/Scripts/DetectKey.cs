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
	private socketScript socketScript;

    void Start()
    {
        rig = GameObject.Find("LMHeadMountedRig");
        s_script = rig.GetComponent<socketScript>();
		dispTxt = dispBox.GetComponent<TextMesh> ();
		socketScript = new socketScript();
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Button" && Time.realtimeSinceStartup - prevTime > 0.3 && dispTxt.text.Length <= 14)
        {
            string letter = other.name;
            Debug.Log(letter);

            //GameObject rig = GameObject.Find("LMHeadMountedRig");
            //socketScript s_script = rig.GetComponent<socketScript>();
            s_script.message += letter;
			dispTxt.text += letter;
        }
		else if (other.tag == "BkspcButton" && Time.realtimeSinceStartup - prevTime > 0.3)
		{
			dispTxt.text = dispTxt.text.Substring(0, dispTxt.text.Length-1);
			Debug.Log ("bkspc");
		}
		else if (other.tag == "EnterButton" && Time.realtimeSinceStartup - prevTime > 0.3)
		{
			socketScript.SendToServer(socketScript.getJson("hongyu wang", dispTxt.text));
			dispTxt.text = "";
			Debug.Log ("rtn");
		}
    }

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Button") {
			prevTime = Time.realtimeSinceStartup;
		}
	}
}
