// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Serialization;

namespace WcfService
{
    public class WcfSoapService : IWcfSoapService
    {
        public string CombineStringXmlSerializerFormatSoap(string message1, string message2)
        {
            return message1 + message2;
        }

        public SoapComplexType EchoComositeTypeXmlSerializerFormatSoap(SoapComplexType complexObject)
        {
            return complexObject;
        }

        [return: MessageParameter(Name = "ProcessCustomerDataReturn"), SoapElement(DataType = "string")]
        public string ProcessCustomerData([MessageParameter(Name = "CustomerData")] CustomerObject customerData)
        {
            return customerData.Name + ((AdditionalData)customerData.Data).Field;
        }
    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class RpcEncSingleNsService : IRpcEncSingleNs1
    {
        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public int Sum(IntParams par)
        {
            return par.p1 + par.p2;
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public float Divide(FloatParams par)
        {
            return (float)(par.p1 / par.p2);
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public string Concatenate(IntParams par)
        {
            return string.Format("{0}{1}", par.p1, par.p2);
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public void DoSomething(IntParams par)
        {
            //Log.Info("Inside DoSomething method...params: {0} {1}", par.p1, par.p2);
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public byte[] CreateSet(ByteParams par)
        {
            return new byte[] { par.p1, par.p2 };
        }
    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class RpcEncMultiNsService : IRpcEncSingleNs1, IRpcEncSingleNs2
    {
        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public int Sum(IntParams par)
        {
            return par.p1 + par.p2;
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public float Divide(FloatParams par)
        {
            return (float)(par.p1 / par.p2);
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public string Concatenate(IntParams par)
        {
            return string.Format("{0}{1}", par.p1, par.p2);
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public void DoSomething(IntParams par)
        {
            //Log.Info("Inside DoSomething method...params: {0} {1}", par.p1, par.p2);
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public byte[] CreateSet(ByteParams par)
        {
            return new byte[] { par.p1, par.p2 };
        }

        [OperationBehavior]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, Use = OperationFormatUse.Encoded)]
        public void SayHello(string name)
        {
            //Log.Info("Hello {0}", name);
        }
    }

}
