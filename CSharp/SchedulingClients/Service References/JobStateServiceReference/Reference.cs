﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchedulingClients.JobStateServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="JobStateData", Namespace="http://schemas.datacontract.org/2004/07/Scheduling.Services.Jobs")]
    [System.SerializableAttribute()]
    public partial class JobStateData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int AssignedAgentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int JobIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SchedulingClients.JobStateServiceReference.JobStatus JobStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SchedulingClients.JobStateServiceReference.NodeTaskStateData[] TasksField;
        
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
        public int AssignedAgentId {
            get {
                return this.AssignedAgentIdField;
            }
            set {
                if ((this.AssignedAgentIdField.Equals(value) != true)) {
                    this.AssignedAgentIdField = value;
                    this.RaisePropertyChanged("AssignedAgentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int JobId {
            get {
                return this.JobIdField;
            }
            set {
                if ((this.JobIdField.Equals(value) != true)) {
                    this.JobIdField = value;
                    this.RaisePropertyChanged("JobId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SchedulingClients.JobStateServiceReference.JobStatus JobStatus {
            get {
                return this.JobStatusField;
            }
            set {
                if ((this.JobStatusField.Equals(value) != true)) {
                    this.JobStatusField = value;
                    this.RaisePropertyChanged("JobStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SchedulingClients.JobStateServiceReference.NodeTaskStateData[] Tasks {
            get {
                return this.TasksField;
            }
            set {
                if ((object.ReferenceEquals(this.TasksField, value) != true)) {
                    this.TasksField = value;
                    this.RaisePropertyChanged("Tasks");
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
        private SchedulingClients.JobStateServiceReference.ServiceCode ServiceCodeField;
        
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
        public SchedulingClients.JobStateServiceReference.ServiceCode ServiceCode {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="JobStatus", Namespace="http://schemas.datacontract.org/2004/07/Scheduling.Core")]
    public enum JobStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Assembly = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Assigning = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Waiting = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InProgress = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Completed = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Aborted = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Editing = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Aborting = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InProgressUnderFault = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        FailureImminent = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Failed = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CompletedUnderFault = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        EditingUnderFault = 12,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="NodeTaskStateData", Namespace="http://schemas.datacontract.org/2004/07/Scheduling.Services.Jobs")]
    [System.SerializableAttribute()]
    public partial class NodeTaskStateData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MetaDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NodeIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TaskIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SchedulingClients.JobStateServiceReference.TaskStatus TaskStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TaskTypeField;
        
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
        public string MetaData {
            get {
                return this.MetaDataField;
            }
            set {
                if ((object.ReferenceEquals(this.MetaDataField, value) != true)) {
                    this.MetaDataField = value;
                    this.RaisePropertyChanged("MetaData");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NodeId {
            get {
                return this.NodeIdField;
            }
            set {
                if ((this.NodeIdField.Equals(value) != true)) {
                    this.NodeIdField = value;
                    this.RaisePropertyChanged("NodeId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TaskId {
            get {
                return this.TaskIdField;
            }
            set {
                if ((this.TaskIdField.Equals(value) != true)) {
                    this.TaskIdField = value;
                    this.RaisePropertyChanged("TaskId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SchedulingClients.JobStateServiceReference.TaskStatus TaskStatus {
            get {
                return this.TaskStatusField;
            }
            set {
                if ((this.TaskStatusField.Equals(value) != true)) {
                    this.TaskStatusField = value;
                    this.RaisePropertyChanged("TaskStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TaskType {
            get {
                return this.TaskTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TaskTypeField, value) != true)) {
                    this.TaskTypeField = value;
                    this.RaisePropertyChanged("TaskType");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskStatus", Namespace="http://schemas.datacontract.org/2004/07/Scheduling.Core")]
    public enum TaskStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unstarted = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InProgress = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Completed = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Aborted = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Assembly = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AttemptingAbort = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AwaitingAbort = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PendingFurtherInstruction = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Editing = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InProgressUnderFault = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CompletedUnderFault = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AttemptingEarlyFailure = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AwaitingEarlyFailure = 12,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        AwaitingFailure = 13,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Failed = 14,
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
        DOWNLOADFAILED = 8001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        UPLOADFAILED = 8002,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETFILENAMESFAILED = 8003,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        INVALIDAGENTID = 9001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETSCHEDULERVERSIONFAILED = 10001,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GETPLUGINVERSIONSFAILED = 10002,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskProgressData", Namespace="http://schemas.datacontract.org/2004/07/Scheduling.Services.Jobs")]
    [System.SerializableAttribute()]
    public partial class TaskProgressData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TaskIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private SchedulingClients.JobStateServiceReference.TaskStatus TaskStatusField;
        
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
        public int TaskId {
            get {
                return this.TaskIdField;
            }
            set {
                if ((this.TaskIdField.Equals(value) != true)) {
                    this.TaskIdField = value;
                    this.RaisePropertyChanged("TaskId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public SchedulingClients.JobStateServiceReference.TaskStatus TaskStatus {
            get {
                return this.TaskStatusField;
            }
            set {
                if ((this.TaskStatusField.Equals(value) != true)) {
                    this.TaskStatusField = value;
                    this.RaisePropertyChanged("TaskStatus");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="JobStateServiceReference.IJobStateService", CallbackContract=typeof(SchedulingClients.JobStateServiceReference.IJobStateServiceCallback))]
    public interface IJobStateService {
        
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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobStateService/GetJobState", ReplyAction="http://tempuri.org/IJobStateService/GetJobStateResponse")]
        System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData> GetJobState(int jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobStateService/GetJobState", ReplyAction="http://tempuri.org/IJobStateService/GetJobStateResponse")]
        System.Threading.Tasks.Task<System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData>> GetJobStateAsync(int jobId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobStateService/GetParentJobStateFromTaskId", ReplyAction="http://tempuri.org/IJobStateService/GetParentJobStateFromTaskIdResponse")]
        System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData> GetParentJobStateFromTaskId(int taskId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobStateService/GetParentJobStateFromTaskId", ReplyAction="http://tempuri.org/IJobStateService/GetParentJobStateFromTaskIdResponse")]
        System.Threading.Tasks.Task<System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData>> GetParentJobStateFromTaskIdAsync(int taskId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJobStateServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJobStateService/OnCallback", ReplyAction="http://tempuri.org/IJobStateService/OnCallbackResponse")]
        void OnCallback(SchedulingClients.JobStateServiceReference.TaskProgressData callbackObject);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJobStateServiceChannel : SchedulingClients.JobStateServiceReference.IJobStateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JobStateServiceClient : System.ServiceModel.DuplexClientBase<SchedulingClients.JobStateServiceReference.IJobStateService>, SchedulingClients.JobStateServiceReference.IJobStateService {
        
        public JobStateServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public JobStateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public JobStateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JobStateServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JobStateServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
        
        public System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData> GetJobState(int jobId) {
            return base.Channel.GetJobState(jobId);
        }
        
        public System.Threading.Tasks.Task<System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData>> GetJobStateAsync(int jobId) {
            return base.Channel.GetJobStateAsync(jobId);
        }
        
        public System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData> GetParentJobStateFromTaskId(int taskId) {
            return base.Channel.GetParentJobStateFromTaskId(taskId);
        }
        
        public System.Threading.Tasks.Task<System.Tuple<SchedulingClients.JobStateServiceReference.JobStateData, SchedulingClients.JobStateServiceReference.ServiceCallData>> GetParentJobStateFromTaskIdAsync(int taskId) {
            return base.Channel.GetParentJobStateFromTaskIdAsync(taskId);
        }
    }
}
