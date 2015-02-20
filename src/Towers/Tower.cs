/*
*	Copyright (C) 2015 Dylan McCormack
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

public abstract class Tower : MonoBehaviour {
	
	/*
	*	An array of costs to upgrade a tower or for the initial purchase.
	*	The first value (0) would be the cost to purchase the tower,
	*	The next value (1) would be the cost to upgrade the tower to Lvl. 2
	*	And the last value (2) would be the cost to upgrade the tower to its max value
	*/
	public static int[] upgradeCost {get; protected set;}
	
	public static int currentLevel {get; set;} //The current level of the tower.
	
	public Bullet bullet; //The bullet prefab this tower shoots

	public float[] baseDamage; //How much damage this tower does under normal conditions
	public float fireRate; //The delay between each shot, in seconds
	protected float cooldown; //How long before we can fire again
	
	protected int targetMode; //Controls what enemies this target should prioritise over
	protected const int FIRST_SPOTTED = 0; //Prioritises based on the order the enemies entered the trigger zone
	protected const int FURTHEST = 1; //Prioritises based on how far ahead the enemies are
	protected const int LAST = 2; //Reversed order of FURTHEST
	protected const int STRONGEST = 3; //Targets based on the amount of health the enemies have
	protected const int WEAKEST = 4; //Targets the enemies with less health first
	
	[System.NonSerialized]
	public List<Enemy> tracked = new List<Enemy>(); //List of all enemies in the trigger zone.
	protected Enemy target; //Which enemy we're currently targetting
	
	public abstract void Fire();
	public abstract float GetDamage();
	
	void Update(){
		if (cooldown < 0 && target != null){
			cooldown = fireRate;
			Fire();
		} 
		
		cooldown -= Time.deltaTime;
	}
	
	/**
	*	Fired when another collider enters this tower's trigger zone, should be an enemy
	*/
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("Triggered");
		if (other.tag == "Enemy"){ //[Safety] Check if the collider was an enemy
			Enemy e = other.GetComponent<Enemy>();
			tracked.Add(e); //Add the enemy to the list of tracked enemies
			e.watchers.Add(this); //Add this tower to the enemy's list of watchers
			SelectTarget();
		}
	}
	
	/**
	*	Fired when another collider leaves this tower's trigger zone, should be an enemy
	*/
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Enemy"){
			Enemy e = other.GetComponent<Enemy>();
			tracked.Remove(e); //Remove the enemy from the list of tracked enemies
			e.watchers.Remove(this); //Remove this tower to the enemy's list of watchers
			SelectTarget();
		}
	}
	
	/**
	*	Updates who we should be targetting. Should be fired whenever the list of tracked enemies changes
	*/
	public void SelectTarget(){
		switch (targetMode){
		case FIRST_SPOTTED:
			if (tracked.Count > 0) //Make sure enemies are being tracked
				target = tracked[0];
			else 
				target = null;
			break;
		case FURTHEST:
			target = null;
			
			foreach (Enemy e in tracked){
				if (target == null){
					target = e;
				} else if (e.transform.position.y < target.transform.position.y){
					target = e;
				}
			}
			break;
		case LAST:
			target = null;
			
			foreach (Enemy e in tracked){
				if (target == null){
					target = e;
				} else if (e.transform.position.y > target.transform.position.y){
					target = e;
				}
			}
			break;
		case STRONGEST:
			target = null;
			
			foreach (Enemy e in tracked){
				if (target == null){
					target = e;
				} else if (e.health > target.health){
					target = e;
				}
			}
			break;
		case WEAKEST:
			target = null;
			
			foreach (Enemy e in tracked){
				if (target == null){
					target = e;
				} else if (e.health < target.health){
					target = e;
				}
			}
			break;
		}
	}
}