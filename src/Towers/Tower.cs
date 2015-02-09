﻿/*
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

	public static float[] baseDamage {get; protected set;} //How much damage this tower does under normal conditions
	
	protected int targetMode; //Controls what enemies this target should prioritise over
	protected const int FIRST_SPOTTED = 0; //Prioritises based on the order the enemies entered the trigger zone
	protected const int FURTHEST = 1; //Prioritises based on how far ahead the enemies are
	protected const int LAST = 2; //Reversed order of FURTHEST
	protected const int STRONGEST = 3; //Targets based on the amount of health the enemies have
	protected const int WEAKEST = 4; //Targets the enemies with less health first
	
	protected List<Enemy> tracked = new List<Enemy>(); //List of all enemies in the trigger zone. TODO: Make enemy class
	protected Enemy target; //Which enemy we're currently targetting
	
	/**
	*	Fired when another collider enters this tower's trigger zone, should be an enemy
	*/
	void OnTriggerEnter(Collider other){
		if (other.tag == "Enemy"){ //[Safety] Check if the collider was an enemy
			Enemy e = other.GetComponent<Enemy>();
			tracked.Add(e); //Add the enemy to the list of tracked enemies
			SelectTarget();
		}
	}
	
	/**
	*	Fired when another collider leaves this tower's trigger zone, should be an enemy
	*/
	void OnTriggerLeave(Collider other){
		if (other.tag == "Enemy"){
			Enemy e = other.GetComponent<Enemy>();
			tracked.Remove(e); //Remove the enemy from the list of tracked enemies
			SelectTarget();
		}
	}
	
	/**
	*	Updates who we should be targetting. Should be fired whenever the list of tracked enemies changes
	*/
	void SelectTarget(){
		switch (targetMode){
		case FIRST_SPOTTED:
			target = tracked[0];
			break;
		case FURTHEST: //TODO: Have to make the enemy stuff before these are possible
			break;
		case LAST:
			break;
		case STRONGEST:
			break;
		case WEAKEST:
			break;
		}
	}
}