using UnityEngine;
using System.Collections;

public class TowerPlacer : MonoBehaviour {
	
	public int scale; //How big, in pixels, each square on the invisible grid would be
	
	bool isPlacing = true; //If we're currently trying to place a tower. Defaulting to true for now for testing purposes :)
	
	public HoloTower holoTower;
	
	void Update(){
		if (isPlacing){
			Vector3 point3 = Input.mousePosition; //Gets the position of the mouse on the screen
			Vector2 point = new Vector2(point3.x - (Screen.width / 2f), point3.y - (Screen.height / 2f)); //Convert to a 2D point with the origin in the center of the screen
			point /= scale; 
			point.x = Mathf.FloorToInt(point.x) + 0.5f;
			point.y = Mathf.FloorToInt(point.y) + 0.5f;
			
			holoTower.transform.position = new Vector3((point.x * scale) / 100f, (point.y * scale) / 100f); //Move the holo tower to the nearest snap-point to the mouse
			
			if (Input.GetButtonDown("Fire1")){ //When the mouse button is pressed
				Instantiate(holoTower.toSpawn, holoTower.transform.position, holoTower.transform.rotation); //Spawn the tower
				
				//isPlacing = false; //We're no longer placing a tower
				//Destroy(holoTower); //Destroy the holo tower
			}
		} 
	}
}