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

public class Slowness : Effect {
	
	float speedModifier;
	
	public Slowness(float speedModifier){
		this.speedModifier = speedModifier;
	}
	
	public void OnApplied(Enemy enemy){
		foreach (Effect effect in enemy.effects){
			if (effect is Slowness && effect != this){ //If the enemy already has slowness
				enemy.RemoveEffect(this, false); //Remove this effect. Do not call event to prevent speeding up
				break;
			}
		}
		
		enemy.movementSpeed *= speedModifier; //Slow down the enemy (Speedmodifier should be less than 1).
	}
	
	public void OnUpdate(Enemy enemy){}
	
	public void OnRemoved(Enemy enemy){
		enemy.movementSpeed /= speedModifier; //Reset the movement speed
	}
}
