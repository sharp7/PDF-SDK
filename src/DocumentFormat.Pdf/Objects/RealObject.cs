﻿using System;

namespace DocumentFormat.Pdf.Objects
{
    /// <summary>
    /// Represents a Pdf Real Object
    /// </summary>
    public class RealObject : NumericObject
    {
        private float value;

        /// <summary>
        /// Instanciates a new RealObject
        /// </summary>
        /// <param name="value">The object's value</param>
        public RealObject(float value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the object's value converted to integer
        /// </summary>
        public override int IntergerValue => Convert.ToInt32(value);

        /// <summary>
        /// Gets the object's value
        /// </summary>
        public override float RealValue => value;
    }
}