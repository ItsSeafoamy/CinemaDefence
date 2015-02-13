/*
*	Copyright (C) 2015 Alexander Prince
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

public class spawnScript : MonoBehaviour 
{
	bool isSpawning = false;
	public float minTime = 1f;
	public float maxTime = 2f;
	public GameObject[] enemies; 
	int enemyNumber;
	
	IEnumerator SpawnObject(int index, float seconds)
	{
		//make sure they're not all spawning on top of each oter
		Debug.Log ("Waiting for " + seconds + " seconds");
		yield return new WaitForSeconds(seconds);
		Instantiate(enemies[index], transform.position, transform.rotation);    
		isSpawning = false;
	}
	
	void Update () 
	{
		//check if spawned and if possible to spawn
		if(! isSpawning && enemyNumber < enemies.Length)
		{
			enemyNumber++;
			isSpawning = true; 
			int enemyIndex = Random.Range(0, enemies.Length);
			StartCoroutine(SpawnObject(enemyIndex, Random.Range(minTime, maxTime)));
		
		}
	}
}
