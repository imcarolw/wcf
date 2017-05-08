// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WcfService
{
    [ServiceContract]
    [XmlSerializerFormat(Use = OperationFormatUse.Encoded)]
    public interface IWcfSoapService
    {
        [OperationContract(Action = "http://tempuri.org/IWcfService/CombineStringXmlSerializerFormatSoap")]
        [XmlSerializerFormat(Use = OperationFormatUse.Encoded)]
        string CombineStringXmlSerializerFormatSoap(string message1, string message2);

        [OperationContract(Action = "http://tempuri.org/IWcfService/EchoComositeTypeXmlSerializerFormatSoap")]
        [XmlSerializerFormat(Use = OperationFormatUse.Encoded)]
        SoapComplexType EchoComositeTypeXmlSerializerFormatSoap(SoapComplexType c);

        [OperationContract(Action = "http://tempuri.org/IWcfService/ProcessCustomerData")]
        [XmlSerializerFormat(Style = OperationFormatStyle.Rpc, SupportFaults = true, Use = OperationFormatUse.Encoded)]
        [ServiceKnownType(typeof(AdditionalData))]
        [return: MessageParameter(Name = "ProcessCustomerDataReturn")]
        [return: System.Xml.Serialization.SoapElement(DataType = "string")]
        string ProcessCustomerData([MessageParameter(Name = "CustomerData")]CustomerObject customerData);
    }

    [ServiceContract(Namespace = "http://tempuri.org/calc")]
    public interface IRpcEncSingleNs1
    {
        [OperationContract]
        int Sum(IntParams par);

        [OperationContract]
        float Divide(FloatParams par);

        [OperationContract]
        string Concatenate(IntParams par);

        [OperationContract]
        void DoSomething(IntParams par);

        [OperationContract]
        DateTime GetCurrentDateTime();

        [OperationContract]
        byte[] CreateSet(ByteParams par);
    }

    [ServiceContract]
    public interface IRpcEncSingleNs2
    {
        [OperationContract]
        void SayHello(string name);
    }

    public class IntParams
    {
        public int p1;
        public int p2;
    }

    public class FloatParams
    {
        public float p1;
        public float p2;
    }

    public class ByteParams
    {
        public byte p1;
        public byte p2;
    }
}
