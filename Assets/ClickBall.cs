using UnityEngine;
using System.Collections;

public class ClickBall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("[TRACE] start");
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("[TRACE] update");
		if(Input.GetMouseButtonDown(0)){
			Debug.Log("Pressed left click.");

			StudentInfoGetter infoGetter = gameObject.AddComponent<StudentInfoGetter>();
			infoGetter.Start1();
		}
	}

	
}

public class StudentInfoGetter : MonoBehaviour {
	string url = "http://127.0.0.1:3000/api/student?";
	string queryParams = "";

	public IEnumerator Start1() {
		Debug.Log("Execute");
		//POST method
		//WWWForm form = new WWWForm();
		//form.AddField("id", "574410b08e33397b4a000005");
		//form.AddField("score", "999");
		//WWW download = new WWW( url, form );

		//GET method
		//get student info with id 574410b08e33397b4a000005 
		AddQueryParams("id","574410b08e33397b4a000005");
		Debug.Log("download data from: " + url + queryParams);
		WWW download = new WWW( url + queryParams );

		// Wait until the download is done
		yield return download;

		if(string.IsNullOrEmpty(download.error)) {
			//response text
			Debug.Log(download.text);

			//create object from JSON, see `StudentInfo` class
			StudentInfo studInfo = StudentInfo.CreateFromJSON(download.text);

			// output student info to console
			Debug.Log(studInfo.id_str);
			Debug.Log(studInfo.name);
			Debug.Log(studInfo.gender);
		} else {
			print( "Error downloading: " + download.error );
		}
	}

	/**
	 * GET
	 * construct query string (helper)
	 **/
	public string AddQueryParams(string key, string value){
		queryParams += WWW.EscapeURL(key) + "=" + WWW.EscapeURL(value) + "&";
		return queryParams;
	}

}


public class StudentInfo{
	public string id_str;
	public string name;
	public string gender;
	
	public static StudentInfo CreateFromJSON(string jsonString){
		return JsonUtility.FromJson<StudentInfo>(jsonString);
	}
}
