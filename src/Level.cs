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

	public static Level instance;
	
	public int scale; //How big, in pixels, each square on the invisible grid would be
	
	bool isPlacing = false; //If we're currently trying to place a tower.
	
	HoloTower holoTower; //The "holotower" is the semi-transparent tower that shows you what tower and where you're placing it
	public HoloTower[] holoTowers;
	
	public List<int> allowedLanes; //The lanes we can have towers in
	public int minY, maxY; //The highest and lowest point we can place a tower (So we can't place towers outside the screen)
	
	List<Tower> placedTowers = new List<Tower>(); //A list of all towers currently on the field.
	
	public Vector2[] spawnPoints; //A list of spawnpoints where enemies spawn. 
	bool isSpawning = false;
	public float minTime = 1f;
	public float maxTime = 2f;
	
	public Wave[] waves; //The waves of enemies
	int wave = 0; //Which wave we are currently on
	int enemyNumber;
	bool waiting = true; //If we are currently waiting to advance to the next wave.
	bool waitingForNextLevel = false;
	
	public int childPrice, studentPrice, adultPrice;
	public float popularity;
	float nextPurchase = 3f;
	public int victoryBonus;
	
	[System.NonSerialized]
	public float happiness = 100f; //Your life
	
	IEnumerator SpawnObject(int index, float seconds){
		//make sure they're not all spawning on top of each oter
		yield return new WaitForSeconds(seconds);
		
		if (waves[wave][index] != null){
			System.Random rand = new System.Random();
			Vector2 point = spawnPoints[rand.Next(spawnPoints.Length)];
			
			Instantiate(waves[wave][index], new Vector3((point.x + 0.5f) * scale / 100f, (point.y + 0.5f) * scale / 100f), transform.rotation);
		}
		
		isSpawning = false;
	}
	
	void Start(){
		instance = this;
	}
	
	void Update(){
		if (!waiting){
			if (nextPurchase <= 0){
				switch (Random.Range(0, 4)){
				case 0:
					int sales = Random.Range(1,2);
					int gained = adultPrice * sales;
					Game.money += gained;
					Debug.Log(sales + " adult ticket" + (sales == 2 ? "s" : "") + " sold for " + gained + "G! (Total: " + Game.money + "G)");
					break;
				case 1:
					int sales1 = Random.Range(1, 4);
					int gained1 = adultPrice * sales1;
					Game.money += gained1;
					Debug.Log(sales1 + " adult ticket" + (sales1 == 2 ? "s" : "") + " sold for " + gained1 + "G! (Total: " + Game.money + "G)");
					break;
				case 2:
					int children = Random.Range(1, 3);
					int adult = Random.Range(1, 2);
					int gained2 = (adultPrice * adult) + (childPrice * children);
					Game.money += gained2;
					Debug.Log(adult + " adult ticket" + (adult == 2 ? "s" : "") + " and " + children + " child ticket" + (children == 2 ? "s" : "") + " sold for " + gained2 + "G! (Total: " + Game.money + "G)");
					break;
				case 3:
					int students = Random.Range(1,2);
					int gained3 = studentPrice * students;
					Game.money += gained3;
					Debug.Log(students + " student ticket" + (students == 2 ? "s" : "") + " sold for " + gained3 + "G! (Total: " + Game.money + "G)");
					break;
				case 4:
					int students2 = Random.Range(1, 6);
					int gained4 = adultPrice * students2;
					Game.money += gained4;
					Debug.Log(students2 + " student ticket" + (students2 == 2 ? "s" : "") + " sold for " + gained4 + "G! (Total: " + Game.money + "G)");
					break;
				case 5:
					int childs = Random.Range(3, 10);
					int gained5 = childPrice * childs;
					Game.money += gained5;
					Debug.Log(childs + " student ticket" + (childs == 2 ? "s" : "") + " sold for " + childs + "G! (Total: " + Game.money + "G)");
					break;
				}
				
				nextPurchase = 10 - (popularity * Random.Range(0.5f, 1));
			} else {
				nextPurchase -= Time.deltaTime;
			}
		}
		
		//check if spawned and if possible to spawn
		if(!isSpawning && !waiting && !waitingForNextLevel){
			if (enemyNumber < waves[wave].enemies.Length){
				isSpawning = true; 
				//int enemyIndex = Random.Range(0, waves[wave].enemies.Length);
				StartCoroutine(SpawnObject(enemyNumber, Random.Range(minTime, maxTime)));
				enemyNumber++;
			} else {
				if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0){ //No more enemies, wave has been defeated
					if (wave < waves.Length - 1){
						wave++;
						enemyNumber = 0;
						waiting = true;
					} else {
						Game.money += victoryBonus;
						Debug.Log("Congratulations! Here's " + victoryBonus + "G for your hard work");
						waitingForNextLevel = true;
						
						if (holoTower != null){
							Destroy(holoTower.gameObject);
						}
					}
				}
			}
		}
		
		if (waiting && Input.GetKeyDown(KeyCode.Return)){
			waiting = false;
		} 
		
		if (waitingForNextLevel && Input.GetKeyDown(KeyCode.Return)){
			//TODO: Bring on the next level!
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			if (holoTower != null){ //If there is already a holoTower, destroy it
				Destroy(holoTower.gameObject);
			}
			
			holoTower = (HoloTower) Instantiate(holoTowers[0]); //Create the new holotower
			isPlacing = true; //We are now placing a tower
		} else if (Input.GetKeyDown(KeyCode.Alpha2)){
			if (holoTower != null){
				Destroy(holoTower.gameObject);
			}
			
			holoTower = (HoloTower) Instantiate(holoTowers[1]);
			isPlacing = true;
		} else if (Input.GetKeyDown(KeyCode.Alpha3)){
			if (holoTower != null){
				Destroy(holoTower.gameObject);
			}
			
			holoTower = (HoloTower) Instantiate(holoTowers[2]);
			isPlacing = true;
		} else if (Input.GetKeyDown(KeyCode.Escape)){
			if (holoTower != null){
				Destroy(holoTower.gameObject);
			}
			
			isPlacing = false; //No longer placing towers
		}
		
		if (isPlacing && !waitingForNextLevel){
			Vector3 point3 = Input.mousePosition; //Gets the position of the mouse on the screen
			Vector2 point = new Vector2(point3.x - (Screen.width / 2f), point3.y - (Screen.height / 2f)); //Convert to a 2D point with the origin in the center of the screen
			point /= scale; 
			point.x = Mathf.FloorToInt(point.x) + 0.5f;
			point.y = Mathf.FloorToInt(point.y) + 0.5f;
			
			holoTower.transform.position = new Vector3((point.x * scale) / 100f, (point.y * scale) / 100f); //Move the holo tower to the nearest snap-point to the mouse
			
			if (point.y >= minY && point.y <= maxY && allowedLanes.Contains(Mathf.FloorToInt(point.x)) && holoTower.toSpawn.buy <= Game.money){ 
				if (Input.GetButtonDown("Fire1")){ //When the mouse button is pressed
					Tower tower = GetTower(holoTower.transform.position);
					
					if (tower == null){
						Tower t = (Tower) Instantiate(holoTower.toSpawn, holoTower.transform.position, holoTower.transform.rotation); //Spawn the tower
						placedTowers.Add(t);
						Game.money -= t.buy;
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