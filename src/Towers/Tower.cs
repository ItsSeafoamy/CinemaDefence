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

public abstract class Tower : MonoBehaviour {
	
	/*
	*	An array of costs to upgrade a tower or for the initial purchase.
	*	The first value (0) would be the cost to purchase the tower,
	*	The next value (1) would be the cost to upgrade the tower to Lvl. 2
	*	And the last value (2) would be the cost to upgrade the tower to its max value
	*/
	public static int[] upgradeCost {get; protected set;}
	
	public static int currentLevel {get; set;} //The current level of the tower.
}