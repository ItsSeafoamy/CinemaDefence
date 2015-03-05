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

public interface Effect {

	void OnApplied(Enemy enemy); //Called when this effect is applied to an enemy. Called AFTER the effect is applied
	void OnUpdate(Enemy enemy); //Called every update for each enemy this effect is applied to. Called BEFORE any other update on an enemy
	void OnRemoved(Enemy enemy); //Called when this effect is removed from an enemy. Called AFTER the effect is removed
}