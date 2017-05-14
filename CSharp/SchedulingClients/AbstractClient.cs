﻿using NLog;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;

namespace SchedulingClients
{
    public abstract class AbstractClient<T> : IClient
    {
        private readonly NetTcpBinding binding;

        private readonly EndpointAddress endpointAddress;

        private bool isDisposed = false;

        private Exception lastCaughtException = null;

        private Logger logger = LogManager.CreateNullLogger();

        public AbstractClient(Uri netTcpUri)
        {
            this.endpointAddress = new EndpointAddress(netTcpUri);
            this.binding = new NetTcpBinding(SecurityMode.None) { PortSharingEnabled = true };
        }

        ~AbstractClient()
        {
            Dispose(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EndpointAddress EndpointAddress { get { return endpointAddress; } }

        /// <summary>
        /// Last caught exception handled by the client
        /// </summary>
        public Exception LastCaughtException
        {
            get { return lastCaughtException; }

            protected set
            {
                if (lastCaughtException != value)
                {
                    lastCaughtException = value;
                    if (value is EndpointNotFoundException)
                    {
                        Logger.Warn("EndpointNotFoundException - is the server running?");
                    }
                    else
                    {
                        Logger.Error(value);
                    }

                    OnNotifyPropertyChanged();
                }
            }
        }

        public Logger Logger
        {
            get { return logger; }

            set
            {
                if (value == null)
                {
                    value = LogManager.CreateNullLogger();
                }
                logger = value;
                logger.Info("Binding:{0} PortSharing:{1}", binding.Name, binding.PortSharingEnabled);
                logger.Info("Endpoint Address:{0}", endpointAddress);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected ChannelFactory<T> CreateChannelFactory()
        {
            return new ChannelFactory<T>(binding, endpointAddress);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;
        }

        protected ServiceOperationResult HandleClientException(Exception ex)
        {
            LastCaughtException = ex;
            Logger.Error(ex);
            return ServiceOperationResult.FromClientException(ex);
        }

        protected void OnNotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChangedEventHandler handlers = PropertyChanged;
            if (handlers != null)
            {
                foreach (PropertyChangedEventHandler handler in handlers.GetInvocationList())
                {
                    handler.BeginInvoke(this, new PropertyChangedEventArgs(propertyName), null, null);
                }
            }
        }
    }
}