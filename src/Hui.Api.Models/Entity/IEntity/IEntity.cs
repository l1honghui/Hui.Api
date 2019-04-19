﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hui.Api.Model.Entity.IEntity
{
    /// <summary>
    /// Defines interface for base entity type. All entities in the system must implement this interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
    public interface IEntity<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TPrimaryKey Id
        {
            get;
            set;
        }

        /// <summary>
        /// Checks if this entity is transient (not persisted to database and it has not an <see cref="P:Hui.Domain.Entities.IEntity`1.Id" />).
        /// </summary>
        /// <returns>True, if this entity is transient</returns>
        bool IsTransient();
    }
}
