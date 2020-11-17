// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models
{
    /// <summary>
    /// Represents a native type (<see langword="int"/>, <see langword="bool"/>, etc) that can have a value or be <see langword="null"/>
    /// </summary>
    /// <typeparam name="T">int, bool, etc</typeparam>
    public class SovrenPrimitive<T> where T : struct
    {
        /// <summary>
        /// The value for this object
        /// </summary>
        public T Value { get; set; }
    }
}