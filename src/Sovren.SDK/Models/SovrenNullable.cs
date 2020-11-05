// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Text.Json.Serialization;

namespace Sovren.Models
{
    /// <summary>
    /// Represents a native type (<see langword="int"/>, <see langword="bool"/>, etc) that can have a value or be <see langword="null"/>
    /// </summary>
    /// <typeparam name="T">int, bool, etc</typeparam>
    [JsonConverter(typeof(SovrenNullableConverter))]
    public class SovrenNullable<T>
    {
        private T _value = default(T);

        /// <summary>
        /// Gets the value if one exists, otherwise throws <see cref="InvalidOperationException"/>.
        /// <br/>Be sure to only use this if <see cref="HasValue"/> is <see langword="true"/>.
        /// <br/><b>NOTE: in the JSON API output, this property will simply be missing if <see cref="HasValue"/> is <see langword="false"/>.</b>
        /// </summary>
        public T Value
        {
            get
            {
                if (!HasValue)
                {
                    throw new InvalidOperationException($"Nullable value is 'null'");
                }

                return _value;
            }
            set
            {
                _value = value;
            }
        }

        /// <summary>
        /// <see langword="true"/> if this object has a value, otherwise <see langword="false"/>
        /// </summary>
        public bool HasValue { get; set; }

        /// <summary>
        /// Creates a <see cref="SovrenNullable{T}"/> from a native type
        /// </summary>
        public static implicit operator SovrenNullable<T>(T value)
        {
            return new SovrenNullable<T>
            {
                Value = value
            };
        }
    }
}
