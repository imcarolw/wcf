// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Infrastructure.Common;
using Xunit;
using System;

public static partial class XmlSerializerFormatTests
{
    [WcfFact]
    [OuterLoop]
    public static void CombineString_XmlSerializerFormat_Soap()
    {
        RunWcfSoapServiceTest((serviceProxy) =>
        {
            // *** EXECUTE *** \\
            string message1 = "hello";
            string message2 = "world";
            var response = serviceProxy.CombineStringXmlSerializerFormatSoap(message1, message2);

            // *** VALIDATE *** \\
            Assert.Equal(message1 + message2, response);
        });
    }

    [WcfFact]
    [OuterLoop]
    [Issue(1884)]
    public static void EchoComositeType_XmlSerializerFormat_Soap()
    {
        RunWcfSoapServiceTest((serviceProxy) =>
        {
            // *** EXECUTE *** \\
            var value = new SoapComplexType() { BoolValue = true, StringValue = "hello" };
            SoapComplexType response = serviceProxy.EchoComositeTypeXmlSerializerFormatSoap(value);

            // *** VALIDATE *** \\
            Assert.NotNull(response);
            Assert.Equal(value.BoolValue, response.BoolValue);
            Assert.Equal(value.StringValue, response.StringValue);
        });
    }

    [WcfFact]
    [OuterLoop]
    public static void ProcessCustomerData_XmlSerializerFormat_Soap()
    {
        RunWcfSoapServiceTest((serviceProxy) =>
        {
            // *** EXECUTE *** \\
            CustomerObject value = new CustomerObject() { Name = "MyName", Data = new AdditionalData() { Field = "Foo" } };
            string response = serviceProxy.ProcessCustomerData(value);

            // *** VALIDATE *** \\
            Assert.Equal("MyNameFoo", response);
        });
    }

    //[WcfFact]
    //[OuterLoop]
    //public static void SingleContractNamespace_XmlSerializerFormat_RpcEncoded()
    //{
    //    bool testFailed = false;

    //    foreach (AppHostType appHostType in new AppHostType[] { AppHostType.SelfHost, AppHostType.WebHost })
    //    {
    //        TestParameters.AppHostType = appHostType;
    //        Log.Info("Variation : {0}", appHostType);

    //        try
    //        {
    //            VariationHelper.RunVariation(typeof(RpcEncSingleNsService));
    //            Log.Info("Variation passed");
    //        }
    //        catch (Exception exception)
    //        {
    //            testFailed = true;
    //            Log.Error("Variation {0} failed with error :{1}", appHostType, exception);
    //        }
    //    }

    //    if (testFailed)
    //    {
    //        throw new Exception("At least one of the variations failed. Look into the log for more details");
    //    }
    //}

    //[WcfFact]
    //[OuterLoop]
    //public void MultipleBindingNamespaces_XmlSerializerFormat_RpcEncoded()
    //{
    //    bool testFailed = false;

    //    foreach (AppHostType appHostType in new AppHostType[] { AppHostType.SelfHost, AppHostType.WebHost })
    //    {
    //        TestParameters.AppHostType = appHostType;
    //        Log.Info("Variation : {0}", appHostType);

    //        try
    //        {
    //            VariationHelper.RunVariation(typeof(RpcEncSingleNsService), true);
    //            Log.Info("Variation passed");
    //        }
    //        catch (Exception exception)
    //        {
    //            testFailed = true;
    //            Log.Error("Variation {0} failed with error :{1}", appHostType, exception);
    //        }
    //    }

    //    if (testFailed)
    //    {
    //        throw new Exception("At least one of the variations failed. Look into the log for more details");
    //    }
    //}

    //[WcfFact]
    //[OuterLoop]
    //public void MultipleNamespaces_XmlSerializerFormat_RpcEncoded()
    //{
    //    bool testFailed = false;

    //    foreach (AppHostType appHostType in new AppHostType[] { AppHostType.SelfHost, AppHostType.WebHost })
    //    {
    //        TestParameters.AppHostType = appHostType;
    //        Log.Info("Variation : {0}", appHostType);

    //        try
    //        {
    //            VariationHelper.RunNegativeVariation(typeof(RpcEncMultiNsService));
    //            Log.Info("Variation passed");
    //        }
    //        catch (Exception exception)
    //        {
    //            testFailed = true;
    //            Log.Error("Variation {0} failed with error :{1}", appHostType, exception);
    //        }
    //    }

    //    if (testFailed)
    //    {
    //        throw new Exception("At least one of the variations failed. Look into the log for more details");
    //    }
    //}

    private static void RunWcfSoapServiceTest(Action<IWcfSoapService> testMethod)
    {
        BasicHttpBinding binding;
        EndpointAddress endpointAddress;
        ChannelFactory<IWcfSoapService> factory;
        IWcfSoapService serviceProxy = null;

        try
        {
            // *** SETUP *** \\
            binding = new BasicHttpBinding();
            endpointAddress = new EndpointAddress(Endpoints.HttpBaseAddress_Basic_Soap);
            factory = new ChannelFactory<IWcfSoapService>(binding, endpointAddress);
            serviceProxy = factory.CreateChannel();
            testMethod(serviceProxy);

            // *** CLEANUP *** \\
            factory.Close();
            ((ICommunicationObject)serviceProxy).Close();
        }
        finally
        {
            // *** ENSURE CLEANUP *** \\
            ScenarioTestHelpers.CloseCommunicationObjects((ICommunicationObject)serviceProxy);
        }
    }

    //private static void RunRpcEncMetadataGenerationTest(Type serviceType, bool addExtraBindingNs)
    //{
    //    //Log.Info("Running variation using address: {0}", baseAddress.ToString());
        
    //    string wsdl = string.Format("{0}?wsdl", address);
    //    string singleWsdl = string.Format("{0}?singleWsdl", address);

    //    CodeGenerationArguments wsdlArguments = new CodeGenerationArguments()
    //    {
    //        SourceFiles = wsdl,
    //        SelectedTool = SelectedTool.SvcUtilClient,
    //        GenConfig = true
    //    };
    //    CodeGenerationArguments singleWsdlArguments = new CodeGenerationArguments()
    //    {
    //        SourceFiles = singleWsdl,
    //        SelectedTool = SelectedTool.SvcUtilClient,
    //        GenConfig = true
    //    };

    //    foreach (SerializerMode mode in Enum.GetValues(typeof(SerializerMode)))
    //    {
    //        Log.Info("Running variation using serializer: {0}", mode.ToString());

    //        wsdlArguments.SerializerMode = mode;
    //        singleWsdlArguments.SerializerMode = mode;

    //        ToolInvoker.RunSvcUtil(wsdlArguments);
    //        ToolInvoker.RunSvcUtil(singleWsdlArguments);

    //        string wsdlGeneratedFile = string.Format("{0}{1}", wsdlArguments.SvcUtilOutputFile, wsdlArguments.Language.ToString().ToLower());
    //        string singleWsdlGeneratedFile = string.Format("{0}{1}", singleWsdlArguments.SvcUtilOutputFile, singleWsdlArguments.Language.ToString().ToLower());

    //        Validator.CompareFile(wsdlGeneratedFile, singleWsdlGeneratedFile, null, false);
    //    }
    //}

}
