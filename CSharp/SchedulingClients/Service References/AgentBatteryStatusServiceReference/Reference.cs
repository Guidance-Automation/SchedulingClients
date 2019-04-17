﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchedulingClients.AgentBatteryStatusServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AgentBatteryStatusData", Namespace="http://schemas.datacontract.org/2004/07/Scheduling.Services.Agents")]
    [System.SerializableAttribute()]
    public partial class AgentBatteryStatusData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double BatteryChargePercentageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
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
        public double BatteryChargePercentage {
            get {
                return this.BatteryChargePercentageField;
            }
            set {
                if ((this.BatteryChargePercentageField.Equals(value) != true)) {
                    this.BatteryChargePercentageField = value;
                    this.RaisePropertyChanged("BatteryChargePercentage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceCallData", Namespace="http://schemas.datacontract.org/2004/07/Services")]
    [System.SerializableAttribute()]
    public partial class ServiceCallData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SchedulingClients.AgentBatteryStatusServiceReference.ServiceCode ServiceCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SourceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StackTraceField;
        
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
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SchedulingClients.AgentBatteryStatusServiceReference.ServiceCode ServiceCode {
            get {
                return this.ServiceCodeField;
            }
            set {
                if ((this.ServiceCodeField.Equals(value) != true)) {
                    this.ServiceCodeField = value;
                    this.RaisePropertyChanged("ServiceCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Source {
            get {
                return this.SourceField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceField, value) != true)) {
                    this.SourceField = value;
                    this.RaisePropertyChanged("Source");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StackTrace {
            get {
                return this.StackTraceField;
            }
            set {
                if ((object.ReferenceEquals(this.StackTraceField, value) != true)) {
                    this.StackTraceField = value;
                    this.RaisePropertyChanged("StackTrace");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceCode", Namespace="http://schemas.datacontract.org/2004/07/Services")]
    public enum ServiceCode : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NOERROR = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SERVICENOTCONFIGURED = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CLIENTEXCEPTION = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SERVICEUNAVAILABLE = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SERVICENOTIMPLEMENTED = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        COMMITJOBFAILED = 1001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATEJOBFAILED = 1002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATEUNORDEREDLISTTASKFAILED = 1003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATEPIPELINEDTASKFAILED = 1004,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATEORDEREDLISTTASKFAILED = 1005,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATESERVICINGTASKFAILED = 1006,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        NOTACCEPTINGNEWJOBS = 1007,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DIRECTIVENOTALLOWED = 1008,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDNODETASKID = 1009,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATESLEEPINGTASKFAILED = 1010,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATEMOVINGTASKFAILED = 1011,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FINALISETASKFAILED = 1012,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BEGINEDITINGJOBFAILED = 1013,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FINISHEDITINGJOBFAILED = 1014,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BEGINEDITINGTASKFAILED = 1015,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FINISHEDITINGTASKFAILED = 1016,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATEAWAITINGTASKFAILED = 1017,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ABORTALLJOBSFAILED = 2001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ABORTALLJOBSFORAGENTFAILED = 2002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ABORTJOBFAILED = 2003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETACTIVEJOBSFORAGENTFAILED = 2004,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ABORTTASKFAILED = 2005,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RESOLVEFAULTEDJOBFAILED = 2006,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RESOLVEFAULTEDTASKFAILED = 2007,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETJOBSTATEFAILED = 3001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDJOBID = 3002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDTASKID = 3003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETALLMOVEDATAFAILED = 4001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETALLNODEDATAFAILED = 4002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETMAPPINGKEYCARDSIGNATUREFAILED = 4003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETTRAJECTORYFAILED = 4004,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDMOVEID = 4005,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        REGISTERBLOCKINGMANDATEFAILED = 4006,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CLEARBLOCKINGMANDATEFAILED = 4007,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETOUTSTANDINGSERVICEREQUESTSFAILED = 5001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SETSERVICECOMPLETEFAILED = 5002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETALLAGENTDATAFAILED = 6001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETALLAGENTSINLIFETIMESTATEFAILED = 6002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SETAGENTLIFETIMESTATEFAILED = 6003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        COMMITINSTRUCTIONFAILED = 7001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        REQUESTFREEZEFAILED = 7002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        REQUESTUNFREEZEFAILED = 7003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INCORRECTNUMBEROFBYTES = 7004,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        COMMITEXTENDEDWAYPOINTSFAILED = 7005,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETKINGPINDESCRIPTIONFAILED = 7008,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CREATEVIRTUALVEHICLEFAILED = 7009,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        REMOVEVEHICLEFAILED = 7010,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SETPOSEFAILED = 7011,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        RESETKINGPINFAILED = 7012,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ENABLEALLVEHICLESFAILED = 7013,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DISABLEALLVEHICLESFAILED = 7014,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDIPADDRESS = 7015,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        ENABLEVEHICLEFAILED = 7016,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DISABLEVEHICLEFAILED = 7017,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SETFLEETSTATEFAILED = 7018,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SETKINGPINSTATEFAILED = 7019,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DOWNLOADFAILED = 8001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UPLOADFAILED = 8002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETFILENAMESFAILED = 8003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDAGENTID = 9001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDSTATECASTVARIABLEALIAS = 9002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        STATECASTVARIABLEDATATYPEMISMATCH = 9003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETSCHEDULERVERSIONFAILED = 10001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETPLUGINVERSIONSFAILED = 10002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETOUTSTANDINGAGENTREQUESTSFAILED = 11001,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AgentBatteryStatusServiceReference.IAgentBatteryStatusService")]
    public interface IAgentBatteryStatusService {
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentBatteryStatusService/GetAllAgentBatteryStatusData", ReplyAction="http://tempuri.org/IAgentBatteryStatusService/GetAllAgentBatteryStatusDataRespons" +
            "e")]
        System.Tuple<SchedulingClients.AgentBatteryStatusServiceReference.AgentBatteryStatusData[], SchedulingClients.AgentBatteryStatusServiceReference.ServiceCallData> GetAllAgentBatteryStatusData();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentBatteryStatusService/GetAllAgentBatteryStatusData", ReplyAction="http://tempuri.org/IAgentBatteryStatusService/GetAllAgentBatteryStatusDataRespons" +
            "e")]
        System.Threading.Tasks.Task<System.Tuple<SchedulingClients.AgentBatteryStatusServiceReference.AgentBatteryStatusData[], SchedulingClients.AgentBatteryStatusServiceReference.ServiceCallData>> GetAllAgentBatteryStatusDataAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAgentBatteryStatusServiceChannel : SchedulingClients.AgentBatteryStatusServiceReference.IAgentBatteryStatusService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AgentBatteryStatusServiceClient : System.ServiceModel.ClientBase<SchedulingClients.AgentBatteryStatusServiceReference.IAgentBatteryStatusService>, SchedulingClients.AgentBatteryStatusServiceReference.IAgentBatteryStatusService {
        
        public AgentBatteryStatusServiceClient() {
        }
        
        public AgentBatteryStatusServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AgentBatteryStatusServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AgentBatteryStatusServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AgentBatteryStatusServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
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
        
        public System.Tuple<SchedulingClients.AgentBatteryStatusServiceReference.AgentBatteryStatusData[], SchedulingClients.AgentBatteryStatusServiceReference.ServiceCallData> GetAllAgentBatteryStatusData() {
            return base.Channel.GetAllAgentBatteryStatusData();
        }
        
        public System.Threading.Tasks.Task<System.Tuple<SchedulingClients.AgentBatteryStatusServiceReference.AgentBatteryStatusData[], SchedulingClients.AgentBatteryStatusServiceReference.ServiceCallData>> GetAllAgentBatteryStatusDataAsync() {
            return base.Channel.GetAllAgentBatteryStatusDataAsync();
        }
    }
}
