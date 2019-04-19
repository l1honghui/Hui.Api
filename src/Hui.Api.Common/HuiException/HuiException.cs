using System;
using System.Runtime.Serialization;

namespace Hui.Api.Common.EmrException
{
    public class HuiException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="T:Hui.HuiException" /> object.
        /// </summary>
        public HuiException()
        {
        }

        /// <summary>
        /// Creates a new <see cref="T:Hui.HuiException" /> object.
        /// </summary>
        public HuiException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }


        /// <summary>
        /// Creates a new <see cref="T:Hui.HuiException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        public HuiException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new <see cref="T:Hui.HuiException" /> object.
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public HuiException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
