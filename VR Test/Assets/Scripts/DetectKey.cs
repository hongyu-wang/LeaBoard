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

    void Start()
    {
        rig = GameObject.Find("LMHeadMountedRig");
        s_script = rig.GetComponent<socketScript>();
		dispTxt = dispBox.GetComponent<TextMesh> ();
    }

    void OnTriggerEnter(Collider other)
    {
		if (other.tag == "Button" && Time.realtimeSinceStartup - prevTime > 0.1 && dispTxt.text.Length <= 11)
        {
            string letter = other.name;
            Debug.Log(letter);

            //GameObject rig = GameObject.Find("LMHeadMountedRig");
            //socketScript s_script = rig.GetComponent<socketScript>();
            s_script.message += letter;
			dispTxt.text += letter;
        }
		else if (other.tag == "BkspcButton" && Time.realtimeSinceStartup - prevTime > 0.1)
		{
			dispTxt.text = dispTxt.text.Remove(dispTxt.text.Length-1);
			s_script.message = s_script.message.Remove (s_script.message.Length - 1);
			Debug.Log ("bkspc");
		}
		else if (other.tag == "EnterButton" && Time.realtimeSinceStartup - prevTime > 0.1)
		{
			s_script.SendToServer(s_script.getJson("hongyu wang", dispTxt.text));
			s_script.message = "";
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
