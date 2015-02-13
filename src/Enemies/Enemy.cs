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

public abstract class Enemy : MonoBehaviour 
{
	public float health = 5;
	public float movementSpeed = 10;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);
		if(health <= 0)
		{
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D hit)
	{
		if (hit.gameObject.tag == "Bullet"){
			Bullet b = hit.gameObject.GetComponent<Bullet>();
			Tower shooter = b.shooter;
			health -= shooter.GetDamage();
			Destroy(b.gameObject);
		}
	}
}
