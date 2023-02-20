using UnityEngine;
using System.Collections;

public class FPS : MonoBehaviour {

	float deltaTime = 0.0f;
	
	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;					//dodaję różnicę między deltaTime bieżącej klatki, a deltaTime poprzedniej klatki jest pomnożona przez 0,1
	}
	
	void OnGUI()															//OnGUI jest wywoływane w każdej klatce w celu renderowania GUI na ekranie
	{
		int w = Screen.width, h = Screen.height;							//zmienna w, która jest ustawiona na szerokość ekranu
																			//zmienna h, która jest ustawiona na wysokość ekranu
		
		GUIStyle style = new GUIStyle();									//nowa zmienna GUIStyle
		
		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperLeft;								//Zmienna prostokąta, która jest ustawiona w lewym górnym rogu
		style.fontSize = h * 2 / 100;										// i miała rozmiar (w,h*2/100) //rozmiar czcionki h*2/100
		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);		//kolor czcionki ustawiony na niebieski
		float msec = deltaTime * 1000.0f;									//zmienne "msec" i "fps", które sąobliczane przez deltaTime*1000.0 i 1.0/deltaTime
		float fps = 1.0f / deltaTime;
		string text = string.Format(" {1:0.} FPS", msec, fps);				//zmienna "text" wartosć fps jest przekazywana do metody string.Format
		GUI.Label(rect, text, style);										//funckja GUI.Label ze zmiennymi rect, text i style, które przekazywane są jako argumenty
																			//Kod renderuje FPS gry na ekranie
	}
}
