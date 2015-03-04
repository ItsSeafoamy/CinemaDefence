/*
*	Copyright (C) 2015 Alexander Prince, Dylan McCormack
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

public abstract class Enemy : MonoBehaviour {

	public float health = 5;
	public float movementSpeed = 10;
	
	[System.NonSerialized]
	public List<Tower> watchers = new List<Tower>();
		
	// Update is called once per frame
	void Update (){
		Move();
		if(health <= 0){
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D hit){		
		if (hit.gameObject.tag == "Bullet"){
			Bullet b = hit.gameObject.GetComponent<Bullet>();
			Damage(b);
			
			Destroy(b.gameObject);
			
			if (health <= 0){
				Kill();
			}
		} else {
			Physics2D.IgnoreCollision(hit.collider, GetComponent<Collider2D>()); 
		}
	}
	
	/**
	*	Called every update to move this enemy
	*	Should be overriden for enemies with irregular movement patterns (e.g. The old lady with the rabbit hopping then stopping)
	*/
	protected virtual void Move(){
		transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
	}
	
	/**
	*	Called when this enemy is damaged.
	*	This should be overriden by enemies that have weaknesses, resistances or some effect that occurs when damaged (e.g. Baby speeding up)
	*/
	protected virtual void Damage(Bullet bullet){
		health -= bullet.damage;
	}
	
	/**
	*	Called when this enemy is destroyed.
	*	This should be overriden if this enemy does something when killed.
	*	For a boss, this could end the level,
	*	Or to spawn the kids when the crazy mum is killed.
	*	Unless for some reason the death is to be cancelled, this base method should be called from overriding methods
	*/
	protected virtual void Kill(){
		foreach (Tower t in watchers){
			t.tracked.Remove(this); //Stop towers from tracking this enemy
			t.SelectTarget(); //And update the towers' targets
		}
		
		Destroy(gameObject); //Destroy this enemy
	}
}