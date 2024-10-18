using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.API.Extensions.Enums
{
    /// <summary>Indicates that an enum value has a Message.</summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class MessageAttribute : System.Attribute
    {
        /// <summary>The Message for the enum value.</summary>
        public string Message { get; set; }

        /// <summary>Constructs a new MessageAttribute.</summary>
        public MessageAttribute() { }

        /// <summary>Constructs a new MessageAttribute.</summary>
        /// <param name="description">The initial value of the Message property.</param>
        public MessageAttribute(string Message)
        {
            this.Message = Message;
        }

        /// <summary>Returns the Message property.</summary>
        /// <returns>The Message property.</returns>
        public override string ToString()
        {
            return this.Message;
        }
    }
}
