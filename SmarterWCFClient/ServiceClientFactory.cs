using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SmarterWCFClient
{
    public static class ServiceClientFactory
    {
        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel>()
                       where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>();
        }

        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel>(Binding binding, EndpointAddress remoteAddress) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(binding, remoteAddress);
        }

        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel>(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, binding, remoteAddress);
        }

        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel>(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, endpointConfigurationName, remoteAddress);
        }

        public static ServiceClientWrapper<TChannel> CreateAndWrap<TChannel>(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) where TChannel : class
        {
            return new ServiceClientWrapper<TChannel>(callbackInstance, endpointConfigurationName, remoteAddress);
        }

        public static void InvokeMethod<TChannel>(Action<TChannel> invocation)
                              where TChannel : class
        {
            if (invocation == null)
                throw new ArgumentNullException("invocation");

            using (var proxy = CreateAndWrap<TChannel>())
            {
                invocation(proxy.Client);
            };
        }

        public static void InvokeMethod<TChannel>(Action<TChannel> invocation, Func<ServiceClientWrapper<TChannel>> initializer) where TChannel : class
        {
            if (invocation == null)
                throw new ArgumentNullException("invocation");

            Func<ServiceClientWrapper<TChannel>> init = initializer ?? (() => CreateAndWrap<TChannel>());

            using (var proxy = init())
            {
                invocation(proxy.Client);
            };
        }

        public static TResult InvokeMethod<TChannel, TResult>(Func<TChannel, TResult> invocation) where TChannel : class
        {
            if (invocation == null)
                throw new ArgumentNullException("invocation");

            using (var proxy = CreateAndWrap<TChannel>())
            {
                return invocation(proxy.Client);
            };
        }

        public static TResult InvokeMethod<TChannel, TResult>(Func<TChannel, TResult> invocation, Func<ServiceClientWrapper<TChannel>> initializer) where TChannel : class
        {
            if (invocation == null)
                throw new ArgumentNullException("invocation");

            Func<ServiceClientWrapper<TChannel>> init = initializer ?? (() => CreateAndWrap<TChannel>());

            using (var proxy = init())
            {
                return invocation(proxy.Client);
            };
        }
    }
}