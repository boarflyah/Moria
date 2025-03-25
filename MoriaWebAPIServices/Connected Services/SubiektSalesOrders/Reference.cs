﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//
//     Zmiany w tym pliku mogą spowodować niewłaściwe zachowanie i zostaną utracone
//     w przypadku ponownego wygenerowania kodu.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SubiektSalesOrders
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SubiektSalesOrders.ISalesOrderContract")]
    public interface ISalesOrderContract
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersDetailed", ReplyAction="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersDetailedResponse")]
        MoriaDTObjects.Models.MoriaSalesOrder[] GetClosedSalesOrdersDetailed(System.DateTime dateFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersDetailed", ReplyAction="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersDetailedResponse")]
        System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetClosedSalesOrdersDetailedAsync(System.DateTime dateFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersSimplified", ReplyAction="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersSimplifiedResponse")]
        MoriaDTObjects.Models.MoriaSalesOrder[] GetClosedSalesOrdersSimplified(System.DateTime dateFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersSimplified", ReplyAction="http://tempuri.org/ISalesOrderContract/GetClosedSalesOrdersSimplifiedResponse")]
        System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetClosedSalesOrdersSimplifiedAsync(System.DateTime dateFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetSalesOrdersSimplified", ReplyAction="http://tempuri.org/ISalesOrderContract/GetSalesOrdersSimplifiedResponse")]
        MoriaDTObjects.Models.MoriaSalesOrder[] GetSalesOrdersSimplified(System.DateTime dateFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetSalesOrdersSimplified", ReplyAction="http://tempuri.org/ISalesOrderContract/GetSalesOrdersSimplifiedResponse")]
        System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetSalesOrdersSimplifiedAsync(System.DateTime dateFrom);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetDetailedSalesOrders", ReplyAction="http://tempuri.org/ISalesOrderContract/GetDetailedSalesOrdersResponse")]
        MoriaDTObjects.Models.MoriaSalesOrder[] GetDetailedSalesOrders(int[] ids);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetDetailedSalesOrders", ReplyAction="http://tempuri.org/ISalesOrderContract/GetDetailedSalesOrdersResponse")]
        System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetDetailedSalesOrdersAsync(int[] ids);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetSalesOrder", ReplyAction="http://tempuri.org/ISalesOrderContract/GetSalesOrderResponse")]
        MoriaDTObjects.Models.MoriaSalesOrder GetSalesOrder(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISalesOrderContract/GetSalesOrder", ReplyAction="http://tempuri.org/ISalesOrderContract/GetSalesOrderResponse")]
        System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder> GetSalesOrderAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    public interface ISalesOrderContractChannel : SubiektSalesOrders.ISalesOrderContract, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.2.0-preview1.23462.5")]
    public partial class SalesOrderContractClient : System.ServiceModel.ClientBase<SubiektSalesOrders.ISalesOrderContract>, SubiektSalesOrders.ISalesOrderContract
    {
        
        /// <summary>
        /// Wdróż tę metodę częściową, aby skonfigurować punkt końcowy usługi.
        /// </summary>
        /// <param name="serviceEndpoint">Punkt końcowy do skonfigurowania</param>
        /// <param name="clientCredentials">Poświadczenia klienta</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SalesOrderContractClient() : 
                base(SalesOrderContractClient.GetDefaultBinding(), SalesOrderContractClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ISalesOrderContract.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesOrderContractClient(EndpointConfiguration endpointConfiguration) : 
                base(SalesOrderContractClient.GetBindingForEndpoint(endpointConfiguration), SalesOrderContractClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesOrderContractClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SalesOrderContractClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesOrderContractClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SalesOrderContractClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SalesOrderContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public MoriaDTObjects.Models.MoriaSalesOrder[] GetClosedSalesOrdersDetailed(System.DateTime dateFrom)
        {
            return base.Channel.GetClosedSalesOrdersDetailed(dateFrom);
        }
        
        public System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetClosedSalesOrdersDetailedAsync(System.DateTime dateFrom)
        {
            return base.Channel.GetClosedSalesOrdersDetailedAsync(dateFrom);
        }
        
        public MoriaDTObjects.Models.MoriaSalesOrder[] GetClosedSalesOrdersSimplified(System.DateTime dateFrom)
        {
            return base.Channel.GetClosedSalesOrdersSimplified(dateFrom);
        }
        
        public System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetClosedSalesOrdersSimplifiedAsync(System.DateTime dateFrom)
        {
            return base.Channel.GetClosedSalesOrdersSimplifiedAsync(dateFrom);
        }
        
        public MoriaDTObjects.Models.MoriaSalesOrder[] GetSalesOrdersSimplified(System.DateTime dateFrom)
        {
            return base.Channel.GetSalesOrdersSimplified(dateFrom);
        }
        
        public System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetSalesOrdersSimplifiedAsync(System.DateTime dateFrom)
        {
            return base.Channel.GetSalesOrdersSimplifiedAsync(dateFrom);
        }
        
        public MoriaDTObjects.Models.MoriaSalesOrder[] GetDetailedSalesOrders(int[] ids)
        {
            return base.Channel.GetDetailedSalesOrders(ids);
        }
        
        public System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder[]> GetDetailedSalesOrdersAsync(int[] ids)
        {
            return base.Channel.GetDetailedSalesOrdersAsync(ids);
        }
        
        public MoriaDTObjects.Models.MoriaSalesOrder GetSalesOrder(int id)
        {
            return base.Channel.GetSalesOrder(id);
        }
        
        public System.Threading.Tasks.Task<MoriaDTObjects.Models.MoriaSalesOrder> GetSalesOrderAsync(int id)
        {
            return base.Channel.GetSalesOrderAsync(id);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISalesOrderContract))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Nie można znaleźć punktu końcowego o nazwie „{0}”.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISalesOrderContract))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8080/MyService");
            }
            throw new System.InvalidOperationException(string.Format("Nie można znaleźć punktu końcowego o nazwie „{0}”.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return SalesOrderContractClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ISalesOrderContract);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return SalesOrderContractClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ISalesOrderContract);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ISalesOrderContract,
        }
    }
}
