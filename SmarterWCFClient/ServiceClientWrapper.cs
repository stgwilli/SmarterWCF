using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace SmarterWCFClient
{
    public class ServiceClientWrapper<TChannel> : ClientBase<TChannel>, IDisposable where TChannel : class
    {
        #region ctors
        public ServiceClientWrapper() { }

        public ServiceClientWrapper(string endpointConfigurationName)
            : base(endpointConfigurationName) { }

        public ServiceClientWrapper(InstanceContext callbackInstance)
            : base(callbackInstance) { }

        public ServiceClientWrapper(ServiceEndpoint endpoint)
            : base(endpoint) { }

        public ServiceClientWrapper(InstanceContext callbackInstance, ServiceEndpoint endpoint)
            : base(callbackInstance, endpoint) { }

        public ServiceClientWrapper(InstanceContext callbackInstance, string endpointConfigurationName)
            : base(callbackInstance, endpointConfigurationName) { }

        public ServiceClientWrapper(string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(endpointConfigurationName, remoteAddress) { }

        public ServiceClientWrapper(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress) { }

        public ServiceClientWrapper(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress) { }

        public ServiceClientWrapper(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress)
            : base(callbackInstance, binding, remoteAddress) { }

        public ServiceClientWrapper(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress)
            : base(callbackInstance, endpointConfigurationName, remoteAddress) { }

        public ServiceClientWrapper(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress)
            : base(callbackInstance, endpointConfigurationName, remoteAddress) { }

        #endregion

        public TChannel Client
        {
            get { return Channel; }
        }

        public new void Close()
        {
            ((IDisposable)this).Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (State != CommunicationState.Closed)
                    base.Close();
            }
            catch (CommunicationException)
            {
                base.Abort();
            }
            catch (TimeoutException)
            {
                base.Abort();
            }
            catch
            {
                base.Abort();
                throw;
            };
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}