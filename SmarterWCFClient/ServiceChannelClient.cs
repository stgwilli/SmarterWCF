using System;

namespace SmarterWCFClient
{
    public abstract class ServiceChannelClient<TChannel> where TChannel : class
    {
        protected virtual ServiceClientWrapper<TChannel> CreateInstance()
        {
            return ServiceClientFactory.CreateAndWrap<TChannel>();
        }

        protected virtual void InvokeMethod(Action<TChannel> action)
        {
            ServiceClientFactory.InvokeMethod(action, CreateInstance);
        }

        protected virtual TResult InvokeMethod<TResult>(Func<TChannel, TResult> action)
        {
            return ServiceClientFactory.InvokeMethod(action, CreateInstance);
        }
    }
}
