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
*	
*/
using UnityEngine;
using System.Collections;

public class GrannyRabbit : Enemy {

	public float waitTime; //How long granny rabbit waits between hops
	float cooldown; //How long til the next hop
	
	public float hopDistance; //How far we hop in each jump
	public float airTime; //How long we stay in the air
	float phase;
	
	Vector3 lastPos; //The position before we hopped
	
	bool hopping; //Are we hopping?
	
	protected override void Move(){
		if (hopping){
			phase += Time.deltaTime / airTime;
			
			if (phase > 1) phase = 1;
			
			float modifier = (Mathf.Sin(2*(Mathf.PI)*(phase-0.25f)) + 1) / 2.0f;
			transform.localScale = Vector3.one * (modifier + 1); //Change the size to give the illusion of us moving towards and away from the screen
			
			if (phase > 0.5f){
				modifier = -modifier + 2;
			}
			
			modifier = modifier / 2.0f * hopDistance;
			
			transform.position = new Vector3(lastPos.x, lastPos.y - modifier, lastPos.z);
			
			if (phase >= 1){
				phase = 0;
				hopping = false;
				GetComponent<Collider2D>().enabled = true; //Enable the collider so we can be attacked again
			}
		} else {
			if (cooldown <= 0){
				hopping = true; //Start hoppimg
				cooldown = waitTime;
				lastPos = transform.position;
				GetComponent<Collider2D>().enabled = false; //Disable the collider, making granny rabbit invulnerable mid-hop
				
				foreach (Tower t in watchers){
					t.tracked.Remove(this); //Stop towers tracking granny rabbit whilst she's invulnerable
					t.SelectTarget();
				}
			} else {
				cooldown -= Time.deltaTime;
			}
		}
	}
}