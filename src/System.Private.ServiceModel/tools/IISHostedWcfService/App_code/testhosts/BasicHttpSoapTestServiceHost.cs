// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;

namespace WcfService
{
    public class BasicHttpSoapTestServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceHost = new BasicHttpSoapTestServiceHost(serviceType, baseAddresses);
            return serviceHost;
        }
    }

    public class BasicHttpSoapTestServiceHost : TestServiceHostBase<IWcfSoapService>
    {
        protected override string Address { get { return "Basic"; } }

        protected override Binding GetBinding()
        {
            return new BasicHttpBinding();
        }

        public BasicHttpSoapTestServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }
    }

    public class BasicHttpRpcEncSingleNsServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceHost = new BasicHttpRpcEncSingleNsServiceHost(serviceType, baseAddresses);
            return serviceHost;
        }
    }

    public class BasicHttpRpcEncSingleNsServiceHost : TestServiceHostBase<IRpcEncSingleNs1>
    {
        protected override string Address { get { return "Basic"; } }

        protected override Binding GetBinding()
        {
            return new BasicHttpBinding();
        }

        public BasicHttpRpcEncSingleNsServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }
    }

    public class BasicHttpRpcEncMultiNsServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceHost = new BasicHttpRpcEncMultiNsServiceHost(serviceType, baseAddresses);
            return serviceHost;
        }
    }

    public class BasicHttpRpcEncMultiNsServiceHost : TestServicesHostBase<IRpcEncSingleNs1,IRpcEncSingleNs2>
    {
        protected override string Address { get { return "Basic"; } }

        protected override Binding GetBinding()
        {
            return new BasicHttpBinding();
        }

        public BasicHttpRpcEncMultiNsServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }
    }    
}
