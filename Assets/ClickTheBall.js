#pragma strict

public var url: String = "http://www.w3schools.com/json/myTutorials.txt";
public var postUrl: String = "http://www.w3schools.com/ajax/demo_post.asp";

function Start () {}

function Update () {
	if(Input.GetMouseButtonDown(0)){
		send();	
	}
}

function send(){
	// Create a form object for sending high score data to the server
	var form = new WWWForm();

	var data = new GameResult(123,456);
	Debug.Log(data.toJson());
	
	form.AddField( "data", data.toJson() );
	
	// Create a download object
	var download = new WWW(postUrl ,form );

	// Wait until the download is done
	yield download;

	if(download.error) {
		print( "Error downloading: " + download.error );
		return;
	} else {
		// show the www content
		Debug.Log(download.text);
	}
}

public class GameResult{
	var userid:int;
	var score:int;
	public function GameResult(userid:int, score:int){
		this.userid = userid;
		this.score = score;
	}
	public function toJson(){
		return JsonUtility.ToJson(this);
	}
}





