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

public class Bullet : MonoBehaviour {
	
	[System.NonSerialized]
	public Tower shooter; //Which tower fired this bullet?
	
	public float speed; //The speed of the bullet
	public Vector3 direction; //The direction the bullet is moving in
	public float damage; //How much this bullet does, before enemy resistances and weaknesses are applied
	
	public List<Effect> effects = new List<Effect>();
	
	void Start(){
		StartCoroutine(Destroy());
	}
	
	void Update(){
		transform.Translate(direction * speed * Time.deltaTime); //Move the bullet
	}
	
	public void AddEffect(Effect effect){
		effects.Add(effect);
	}
	
	IEnumerator Destroy(){
		yield return new WaitForSeconds(1f);
		Destroy(gameObject); //Destroy bullets after 1 second to prevent any lag
	}
}