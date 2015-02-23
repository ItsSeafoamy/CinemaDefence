/*
*	Copyright (C) 2015 Dylan McCormack, Alexander Prince
*	
*	This program is free software; you can redistribute it and/or modify
*	it under the terms of the GNU General Public License as published by
*	the Free Software Foundation; either version 2 of the License, or
*	any later version.
*		
*	This program is distributed in the hope that it will be useful,
*	but WITHOUT ANY WARRANTY; without even the implied warranty of
*	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*	GNU General Public License for more details.
*			
*	You should have received a copy of the GNU General Public License along
*	with this program; if not, write to the Free Software Foundation, Inc.,
*	51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour {
	
	public int scale; //How big, in pixels, each square on the invisible grid would be
	
	bool isPlacing = true; //If we're currently trying to place a tower. Defaulting to true for now for testing purposes :)
	
	public HoloTower holoTower;
	
	public List<int> allowedLanes;
	public int minY, maxY;
	
	List<Tower> placedTowers = new List<Tower>();
	
	public Vector2[] spawnPoints;
	bool isSpawning = false;
	public float minTime = 1f;
	public float maxTime = 2f;
	public Enemy[] enemies; 
	int enemyNumber;
	
	IEnumerator SpawnObject(int index, float seconds){
		//make sure they're not all spawning on top of each oter
		Debug.Log ("Waiting for " + seconds + " seconds");
		yield return new WaitForSeconds(seconds);
		
		System.Random rand = new System.Random();
		Vector2 point = spawnPoints[rand.Next(spawnPoints.Length)];
		
		Instantiate(enemies[index], new Vector3((point.x + 0.5f) * scale / 100f, (point.y + 0.5f) * scale / 100f), transform.rotation);
		  
		isSpawning = false;
	}
	
	void Update(){
		//check if spawned and if possible to spawn
		if(!isSpawning && enemyNumber < enemies.Length){
			enemyNumber++;
			isSpawning = true; 
			int enemyIndex = Random.Range(0, enemies.Length);
			StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
		}
		
		if (isPlacing){
			Vector3 point3 = Input.mousePosition; //Gets the position of the mouse on the screen
			Vector2 point = new Vector2(point3.x - (Screen.width / 2f), point3.y - (Screen.height / 2f)); //Convert to a 2D point with the origin in the center of the screen
			point /= scale; 
			point.x = Mathf.FloorToInt(point.x) + 0.5f;
			point.y = Mathf.FloorToInt(point.y) + 0.5f;
			
			holoTower.transform.position = new Vector3((point.x * scale) / 100f, (point.y * scale) / 100f); //Move the holo tower to the nearest snap-point to the mouse
			
			if (point.y >= minY && point.y <= maxY && allowedLanes.Contains(Mathf.FloorToInt(point.x))){ //When the mouse button is pressed
				if (Input.GetButtonDown("Fire1")){
					Tower tower = GetTower(holoTower.transform.position);
					
					if (tower == null){
						Tower t = (Tower) Instantiate(holoTower.toSpawn, holoTower.transform.position, holoTower.transform.rotation); //Spawn the tower
						placedTowers.Add(t);
					} else {
						placedTowers.Remove(tower);
						Destroy(tower.gameObject);
					}
				}
				
				holoTower.GetComponent<SpriteRenderer>().sprite = holoTower.valid; //Change the sprite to the "valid" sprite to show the player they can place the tower here
			} else {
				holoTower.GetComponent<SpriteRenderer>().sprite = holoTower.invalid; //Change the sprite to the "invalid" sprite
			}
		} 
	}
	
	/**
	*	Gets the tower at the specified position
	*	Or null, if the space is empty.
	*/
	Tower GetTower(Vector3 position){
		foreach (Tower t in placedTowers){
			if (t.transform.position == position){
				return t;
			}
		}
		
		return null;
	}
}