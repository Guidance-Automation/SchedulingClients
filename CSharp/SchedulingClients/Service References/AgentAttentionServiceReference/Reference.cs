﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchedulingClients.AgentAttentionServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AgentAttentionData", Namespace="http://schemas.datacontract.org/2004/07/SchedulingServices")]
    [System.SerializableAttribute()]
    public partial class AgentAttentionData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Tuple<int, SchedulingClients.AgentAttentionServiceReference.AttentionType>[] RequiringAttentionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Tuple<int, SchedulingClients.AgentAttentionServiceReference.AttentionType>[] RequiringAttention {
            get {
                return this.RequiringAttentionField;
            }
            set {
                if ((object.ReferenceEquals(this.RequiringAttentionField, value) != true)) {
                    this.RequiringAttentionField = value;
                    this.RaisePropertyChanged("RequiringAttention");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AttentionType", Namespace="http://schemas.datacontract.org/2004/07/SchedulingServices")]
    public enum AttentionType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Physical = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GUI = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Comms = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AgentAttentionServiceReference.IAgentAttentionService", CallbackContract=typeof(SchedulingClients.AgentAttentionServiceReference.IAgentAttentionServiceCallback))]
    public interface IAgentAttentionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/VersionMajor", ReplyAction="http://tempuri.org/IService/VersionMajorResponse")]
        int VersionMajor();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/VersionMajor", ReplyAction="http://tempuri.org/IService/VersionMajorResponse")]
        System.Threading.Tasks.Task<int> VersionMajorAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/VersionMinor", ReplyAction="http://tempuri.org/IService/VersionMinorResponse")]
        int VersionMinor();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/VersionMinor", ReplyAction="http://tempuri.org/IService/VersionMinorResponse")]
        System.Threading.Tasks.Task<int> VersionMinorAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/VersionPatch", ReplyAction="http://tempuri.org/IService/VersionPatchResponse")]
        int VersionPatch();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/VersionPatch", ReplyAction="http://tempuri.org/IService/VersionPatchResponse")]
        System.Threading.Tasks.Task<int> VersionPatchAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISubscriptionService/SubscriptionHeartbeat", ReplyAction="http://tempuri.org/ISubscriptionService/SubscriptionHeartbeatResponse")]
        void SubscriptionHeartbeat(System.Guid guid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISubscriptionService/SubscriptionHeartbeat", ReplyAction="http://tempuri.org/ISubscriptionService/SubscriptionHeartbeatResponse")]
        System.Threading.Tasks.Task SubscriptionHeartbeatAsync(System.Guid guid);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAgentAttentionServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentAttentionService/OnCallback", ReplyAction="http://tempuri.org/IAgentAttentionService/OnCallbackResponse")]
        void OnCallback(SchedulingClients.AgentAttentionServiceReference.AgentAttentionData callbackObject);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAgentAttentionServiceChannel : SchedulingClients.AgentAttentionServiceReference.IAgentAttentionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AgentAttentionServiceClient : System.ServiceModel.DuplexClientBase<SchedulingClients.AgentAttentionServiceReference.IAgentAttentionService>, SchedulingClients.AgentAttentionServiceReference.IAgentAttentionService {
        
        public AgentAttentionServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public AgentAttentionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public AgentAttentionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AgentAttentionServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AgentAttentionServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public int VersionMajor() {
            return base.Channel.VersionMajor();
        }
        
        public System.Threading.Tasks.Task<int> VersionMajorAsync() {
            return base.Channel.VersionMajorAsync();
        }
        
        public int VersionMinor() {
            return base.Channel.VersionMinor();
        }
        
        public System.Threading.Tasks.Task<int> VersionMinorAsync() {
            return base.Channel.VersionMinorAsync();
        }
        
        public int VersionPatch() {
            return base.Channel.VersionPatch();
        }
        
        public System.Threading.Tasks.Task<int> VersionPatchAsync() {
            return base.Channel.VersionPatchAsync();
        }
        
        public void SubscriptionHeartbeat(System.Guid guid) {
            base.Channel.SubscriptionHeartbeat(guid);
        }
        
        public System.Threading.Tasks.Task SubscriptionHeartbeatAsync(System.Guid guid) {
            return base.Channel.SubscriptionHeartbeatAsync(guid);
        }
    }
}
