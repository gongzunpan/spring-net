#region License

/*
 * Copyright 2002-2008 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System;
using Apache.NMS;

namespace Spring.Messaging.Nms.Connections
{
    /// <summary>
    /// MessageProducer decorator that adapts specific settings
    /// to a shared MessageProducer instance underneath.
    /// </summary>
    /// <author>Juergen Hoeller</author>
    /// <author>Mark Pollack (.NET)</author>
    public class CachedMessageProducer : IMessageProducer
    {
        private readonly IMessageProducer target;

        private bool disableMessageID;

        private object originalDisableMessageID = null;

        private bool disableMessageTimestamp;

        private object originalDisableMessageTimestamp = null;

        //Not part of NMS spce
        //private int deliveryMode;

        private bool persistent;

        private byte priority;

        private TimeSpan timeToLive;


        /// <summary>
        /// Initializes a new instance of the <see cref="CachedMessageProducer"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public CachedMessageProducer(IMessageProducer target)
        {
            this.target = target;
        }


        /// <summary>
        /// Gets the target MessageProducer, the procder we are 'wrapping'
        /// </summary>
        /// <value>The target MessageProducer.</value>
        public IMessageProducer Target
        {
            get { return target; }
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(IMessage message)
        {
            target.Send(message);
        }

        /// <summary>
        /// Sends a message to the specified message.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="persistent">if set to <c>true</c> use persistent QOS.</param>
        /// <param name="priority">The message priority.</param>
        /// <param name="timeToLive">The time to live.</param>
        public void Send(IMessage message, bool persistent, byte priority, TimeSpan timeToLive)
        {
           target.Send(message, persistent, priority, timeToLive);
        }

        /// <summary>
        /// Sends a message to the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="message">The message.</param>
        public void Send(IDestination destination, IMessage message)
        {
            target.Send(destination, message);
        }

        /// <summary>
        /// Sends a message the specified destination.
        /// </summary>
        /// <param name="destination">The destination.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="persistent">if set to <c>true</c> use persistent QOS.</param>
        /// <param name="priority">The priority.</param>
        /// <param name="timeToLive">The time to live.</param>
        public void Send(IDestination destination, IMessage message, bool persistent, byte priority, TimeSpan timeToLive)
        {
            target.Send(destination, message, persistent, priority, timeToLive);
        }

        #region Odd Message Creationg Methods on IMessageProducer - not in-line with JMS APIs.
        /// <summary>
        /// Creates the message.
        /// </summary>
        /// <returns>A new message</returns>
        public IMessage CreateMessage()
        {
            return target.CreateMessage();
        }

        /// <summary>
        /// Creates the text message.
        /// </summary>
        /// <returns>A new text message.</returns>
        public ITextMessage CreateTextMessage()
        {
            return target.CreateTextMessage();
        }

        /// <summary>
        /// Creates the text message.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A texst message with the given text.</returns>
        public ITextMessage CreateTextMessage(string text)
        {
            return target.CreateTextMessage(text);
        }

        /// <summary>
        /// Creates the map message.
        /// </summary>
        /// <returns>a new map message.</returns>
        public IMapMessage CreateMapMessage()
        {
            return target.CreateMapMessage();
        }

        /// <summary>
        /// Creates the object message.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A new object message with the given body.</returns>
        public IObjectMessage CreateObjectMessage(object body)
        {
            return target.CreateObjectMessage(body);
        }

        /// <summary>
        /// Creates the bytes message.
        /// </summary>
        /// <returns>A new bytes message.</returns>
        public IBytesMessage CreateBytesMessage()
        {
            return target.CreateBytesMessage();
        }

        /// <summary>
        /// Creates the bytes message.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns>A new bytes message with the given body.</returns>
        public IBytesMessage CreateBytesMessage(byte[] body)
        {
            return target.CreateBytesMessage(body);
        }
        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CachedMessageProducer"/> uses a persistent QOS
        /// </summary>
        /// <value><c>true</c> if persistent; otherwise, <c>false</c>.</value>
        public bool Persistent
        {
            get { return persistent; }
            set { persistent = value; }
        }

        /// <summary>
        /// Gets or sets the time to live value for messages sent with this producer.
        /// </summary>
        /// <value>The time to live.</value>
        public TimeSpan TimeToLive
        {
            get { return timeToLive; }
            set { timeToLive = value; }
        }

        /// <summary>
        /// Gets or sets the priority of messages sent with this producer.
        /// </summary>
        /// <value>The priority.</value>
        public byte Priority
        {
            get { return priority; }
            set { priority = value;}
        }

        /// <summary>
        /// Gets or sets a value indicating whether disable setting of the message ID property.
        /// </summary>
        /// <value><c>true</c> if disable message ID setting; otherwise, <c>false</c>.</value>
        public bool DisableMessageID
        {
            get
            {
                return disableMessageID;
            }
            set
            {
                if (originalDisableMessageID == null)
                {
                    originalDisableMessageID = value;
                }
                disableMessageID = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether disable setting the message timestamp property.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if disable message timestamp; otherwise, <c>false</c>.
        /// </value>
        public bool DisableMessageTimestamp
        {
            get
            {
                return disableMessageTimestamp;
            }
            set
            {
                if (originalDisableMessageTimestamp == null)
                {
                    originalDisableMessageTimestamp = value;
                }
                disableMessageTimestamp = value;
            }
        }

        /// <summary>
        /// Reset properties.
        /// </summary>
        public void Dispose()
        {
            // It's a cached MessageProducer... reset properties only.
            if (originalDisableMessageID != null)
            {
                target.DisableMessageID = (bool) originalDisableMessageID;
                originalDisableMessageID = null;
            }
            if (originalDisableMessageTimestamp != null)
            {
                target.DisableMessageTimestamp = (bool) originalDisableMessageTimestamp;
                originalDisableMessageTimestamp = null;
            }
        }
    }
}