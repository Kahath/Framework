﻿/*
 * This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace ServerFramework.Database.Base.Entity
{
	public abstract class EntityBase : IEntity
	{
		#region Properties

		public bool Active					{ get; set; }
		public DateTime DateCreated			{ get; set; }
		public DateTime? DateModified		{ get; set; }
		public DateTime? DateDeactivated	{ get; set; }

		#endregion

		#region Constructors

		public EntityBase()
		{
			Active = true;
		}

		#endregion
	}
}