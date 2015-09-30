﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.5420
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UNIRecruitmentSoft
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="UNIRecruitment")]
	public partial class UniDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCandidateDetail(CandidateDetail instance);
    partial void UpdateCandidateDetail(CandidateDetail instance);
    partial void DeleteCandidateDetail(CandidateDetail instance);
    #endregion
		
		public UniDBDataContext() : 
				base(global::UNIRecruitmentSoft.Properties.Settings.Default.UNIRecruitmentConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public UniDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UniDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UniDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public UniDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<CandidateDetail> CandidateDetails
		{
			get
			{
				return this.GetTable<CandidateDetail>();
			}
		}
		
		[Function(Name="dbo.ReCalculateAge")]
		public int ReCalculateAge()
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
			return ((int)(result.ReturnValue));
		}
	}
	
	[Table(Name="dbo.CandidateDetail")]
	public partial class CandidateDetail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private long _SrNo;
		
		private string _Link;
		
		private System.DateTime _ProcessedDate;
		
		private string _Name;
		
		private string _Gender;
		
		private System.DateTime _DateOfBirth;
		
		private int _Age;
		
		private string _CurrentLocation;
		
		private string _PreferedLocation;
		
		private string _MobileNo;
		
		private string _LandLineNo;
		
		private string _Email;
		
		private string _Qualification;
		
		private string _AdditionalQualification;
		
		private int _Experience;
		
		private string _ExpSlap;
		
		private string _JobTitle;
		
		private string _CurrentIndustry;
		
		private string _PreferredIndustry;
		
		private string _NoticePeriod;
		
		private string _CurrentCTC;
		
		private string _ExpectedCTC;
		
		private int _NoOfCalls;
		
		private string _Remarks;
		
		private string _CVPath;
		
		private bool _Registered;
		
		private bool _Placed;
		
		private string _MarritalStatus;
		
		private string _ExecutiveName;
		
		private string _CallLatter1;
		
		private string _CallLatter2;
		
		private string _CallLatter3;
		
		private string _CallLatter4;
		
		private string _CallLatter5;
		
		private bool _IsDeleted;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSrNoChanging(long value);
    partial void OnSrNoChanged();
    partial void OnLinkChanging(string value);
    partial void OnLinkChanged();
    partial void OnProcessedDateChanging(System.DateTime value);
    partial void OnProcessedDateChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnGenderChanging(string value);
    partial void OnGenderChanged();
    partial void OnDateOfBirthChanging(System.DateTime value);
    partial void OnDateOfBirthChanged();
    partial void OnAgeChanging(int value);
    partial void OnAgeChanged();
    partial void OnCurrentLocationChanging(string value);
    partial void OnCurrentLocationChanged();
    partial void OnPreferedLocationChanging(string value);
    partial void OnPreferedLocationChanged();
    partial void OnMobileNoChanging(string value);
    partial void OnMobileNoChanged();
    partial void OnLandLineNoChanging(string value);
    partial void OnLandLineNoChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnQualificationChanging(string value);
    partial void OnQualificationChanged();
    partial void OnAdditionalQualificationChanging(string value);
    partial void OnAdditionalQualificationChanged();
    partial void OnExperienceChanging(int value);
    partial void OnExperienceChanged();
    partial void OnExpSlapChanging(string value);
    partial void OnExpSlapChanged();
    partial void OnJobTitleChanging(string value);
    partial void OnJobTitleChanged();
    partial void OnCurrentIndustryChanging(string value);
    partial void OnCurrentIndustryChanged();
    partial void OnPreferredIndustryChanging(string value);
    partial void OnPreferredIndustryChanged();
    partial void OnNoticePeriodChanging(string value);
    partial void OnNoticePeriodChanged();
    partial void OnCurrentCTCChanging(string value);
    partial void OnCurrentCTCChanged();
    partial void OnExpectedCTCChanging(string value);
    partial void OnExpectedCTCChanged();
    partial void OnNoOfCallsChanging(int value);
    partial void OnNoOfCallsChanged();
    partial void OnRemarksChanging(string value);
    partial void OnRemarksChanged();
    partial void OnCVPathChanging(string value);
    partial void OnCVPathChanged();
    partial void OnRegisteredChanging(bool value);
    partial void OnRegisteredChanged();
    partial void OnPlacedChanging(bool value);
    partial void OnPlacedChanged();
    partial void OnMarritalStatusChanging(string value);
    partial void OnMarritalStatusChanged();
    partial void OnExecutiveNameChanging(string value);
    partial void OnExecutiveNameChanged();
    partial void OnCallLatter1Changing(string value);
    partial void OnCallLatter1Changed();
    partial void OnCallLatter2Changing(string value);
    partial void OnCallLatter2Changed();
    partial void OnCallLatter3Changing(string value);
    partial void OnCallLatter3Changed();
    partial void OnCallLatter4Changing(string value);
    partial void OnCallLatter4Changed();
    partial void OnCallLatter5Changing(string value);
    partial void OnCallLatter5Changed();
    partial void OnIsDeletedChanging(bool value);
    partial void OnIsDeletedChanged();
    #endregion
		
		public CandidateDetail()
		{
			OnCreated();
		}
		
		[Column(Storage="_SrNo", DbType="BigInt NOT NULL", IsPrimaryKey=true)]
		public long SrNo
		{
			get
			{
				return this._SrNo;
			}
			set
			{
				if ((this._SrNo != value))
				{
					this.OnSrNoChanging(value);
					this.SendPropertyChanging();
					this._SrNo = value;
					this.SendPropertyChanged("SrNo");
					this.OnSrNoChanged();
				}
			}
		}
		
		[Column(Storage="_Link", DbType="NVarChar(50)")]
		public string Link
		{
			get
			{
				return this._Link;
			}
			set
			{
				if ((this._Link != value))
				{
					this.OnLinkChanging(value);
					this.SendPropertyChanging();
					this._Link = value;
					this.SendPropertyChanged("Link");
					this.OnLinkChanged();
				}
			}
		}
		
		[Column(Storage="_ProcessedDate", DbType="Date NOT NULL")]
		public System.DateTime ProcessedDate
		{
			get
			{
				return this._ProcessedDate;
			}
			set
			{
				if ((this._ProcessedDate != value))
				{
					this.OnProcessedDateChanging(value);
					this.SendPropertyChanging();
					this._ProcessedDate = value;
					this.SendPropertyChanged("ProcessedDate");
					this.OnProcessedDateChanged();
				}
			}
		}
		
		[Column(Storage="_Name", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[Column(Storage="_Gender", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this.OnGenderChanging(value);
					this.SendPropertyChanging();
					this._Gender = value;
					this.SendPropertyChanged("Gender");
					this.OnGenderChanged();
				}
			}
		}
		
		[Column(Storage="_DateOfBirth", DbType="Date NOT NULL")]
		public System.DateTime DateOfBirth
		{
			get
			{
				return this._DateOfBirth;
			}
			set
			{
				if ((this._DateOfBirth != value))
				{
					this.OnDateOfBirthChanging(value);
					this.SendPropertyChanging();
					this._DateOfBirth = value;
					this.SendPropertyChanged("DateOfBirth");
					this.OnDateOfBirthChanged();
				}
			}
		}
		
		[Column(Storage="_Age", DbType="Int NOT NULL")]
		public int Age
		{
			get
			{
				return this._Age;
			}
			set
			{
				if ((this._Age != value))
				{
					this.OnAgeChanging(value);
					this.SendPropertyChanging();
					this._Age = value;
					this.SendPropertyChanged("Age");
					this.OnAgeChanged();
				}
			}
		}
		
		[Column(Storage="_CurrentLocation", DbType="NVarChar(100)")]
		public string CurrentLocation
		{
			get
			{
				return this._CurrentLocation;
			}
			set
			{
				if ((this._CurrentLocation != value))
				{
					this.OnCurrentLocationChanging(value);
					this.SendPropertyChanging();
					this._CurrentLocation = value;
					this.SendPropertyChanged("CurrentLocation");
					this.OnCurrentLocationChanged();
				}
			}
		}
		
		[Column(Storage="_PreferedLocation", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string PreferedLocation
		{
			get
			{
				return this._PreferedLocation;
			}
			set
			{
				if ((this._PreferedLocation != value))
				{
					this.OnPreferedLocationChanging(value);
					this.SendPropertyChanging();
					this._PreferedLocation = value;
					this.SendPropertyChanged("PreferedLocation");
					this.OnPreferedLocationChanged();
				}
			}
		}
		
		[Column(Storage="_MobileNo", DbType="NVarChar(11)")]
		public string MobileNo
		{
			get
			{
				return this._MobileNo;
			}
			set
			{
				if ((this._MobileNo != value))
				{
					this.OnMobileNoChanging(value);
					this.SendPropertyChanging();
					this._MobileNo = value;
					this.SendPropertyChanged("MobileNo");
					this.OnMobileNoChanged();
				}
			}
		}
		
		[Column(Storage="_LandLineNo", DbType="NVarChar(12)")]
		public string LandLineNo
		{
			get
			{
				return this._LandLineNo;
			}
			set
			{
				if ((this._LandLineNo != value))
				{
					this.OnLandLineNoChanging(value);
					this.SendPropertyChanging();
					this._LandLineNo = value;
					this.SendPropertyChanged("LandLineNo");
					this.OnLandLineNoChanged();
				}
			}
		}
		
		[Column(Storage="_Email", DbType="NVarChar(100)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[Column(Storage="_Qualification", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string Qualification
		{
			get
			{
				return this._Qualification;
			}
			set
			{
				if ((this._Qualification != value))
				{
					this.OnQualificationChanging(value);
					this.SendPropertyChanging();
					this._Qualification = value;
					this.SendPropertyChanged("Qualification");
					this.OnQualificationChanged();
				}
			}
		}
		
		[Column(Storage="_AdditionalQualification", DbType="NVarChar(100)")]
		public string AdditionalQualification
		{
			get
			{
				return this._AdditionalQualification;
			}
			set
			{
				if ((this._AdditionalQualification != value))
				{
					this.OnAdditionalQualificationChanging(value);
					this.SendPropertyChanging();
					this._AdditionalQualification = value;
					this.SendPropertyChanged("AdditionalQualification");
					this.OnAdditionalQualificationChanged();
				}
			}
		}
		
		[Column(Storage="_Experience", DbType="Int NOT NULL")]
		public int Experience
		{
			get
			{
				return this._Experience;
			}
			set
			{
				if ((this._Experience != value))
				{
					this.OnExperienceChanging(value);
					this.SendPropertyChanging();
					this._Experience = value;
					this.SendPropertyChanged("Experience");
					this.OnExperienceChanged();
				}
			}
		}
		
		[Column(Storage="_ExpSlap", DbType="NVarChar(50)")]
		public string ExpSlap
		{
			get
			{
				return this._ExpSlap;
			}
			set
			{
				if ((this._ExpSlap != value))
				{
					this.OnExpSlapChanging(value);
					this.SendPropertyChanging();
					this._ExpSlap = value;
					this.SendPropertyChanged("ExpSlap");
					this.OnExpSlapChanged();
				}
			}
		}
		
		[Column(Storage="_JobTitle", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string JobTitle
		{
			get
			{
				return this._JobTitle;
			}
			set
			{
				if ((this._JobTitle != value))
				{
					this.OnJobTitleChanging(value);
					this.SendPropertyChanging();
					this._JobTitle = value;
					this.SendPropertyChanged("JobTitle");
					this.OnJobTitleChanged();
				}
			}
		}
		
		[Column(Storage="_CurrentIndustry", DbType="NVarChar(200)")]
		public string CurrentIndustry
		{
			get
			{
				return this._CurrentIndustry;
			}
			set
			{
				if ((this._CurrentIndustry != value))
				{
					this.OnCurrentIndustryChanging(value);
					this.SendPropertyChanging();
					this._CurrentIndustry = value;
					this.SendPropertyChanged("CurrentIndustry");
					this.OnCurrentIndustryChanged();
				}
			}
		}
		
		[Column(Storage="_PreferredIndustry", DbType="NVarChar(200)")]
		public string PreferredIndustry
		{
			get
			{
				return this._PreferredIndustry;
			}
			set
			{
				if ((this._PreferredIndustry != value))
				{
					this.OnPreferredIndustryChanging(value);
					this.SendPropertyChanging();
					this._PreferredIndustry = value;
					this.SendPropertyChanged("PreferredIndustry");
					this.OnPreferredIndustryChanged();
				}
			}
		}
		
		[Column(Storage="_NoticePeriod", DbType="NVarChar(50)")]
		public string NoticePeriod
		{
			get
			{
				return this._NoticePeriod;
			}
			set
			{
				if ((this._NoticePeriod != value))
				{
					this.OnNoticePeriodChanging(value);
					this.SendPropertyChanging();
					this._NoticePeriod = value;
					this.SendPropertyChanged("NoticePeriod");
					this.OnNoticePeriodChanged();
				}
			}
		}
		
		[Column(Storage="_CurrentCTC", DbType="NVarChar(50)")]
		public string CurrentCTC
		{
			get
			{
				return this._CurrentCTC;
			}
			set
			{
				if ((this._CurrentCTC != value))
				{
					this.OnCurrentCTCChanging(value);
					this.SendPropertyChanging();
					this._CurrentCTC = value;
					this.SendPropertyChanged("CurrentCTC");
					this.OnCurrentCTCChanged();
				}
			}
		}
		
		[Column(Storage="_ExpectedCTC", DbType="NVarChar(50)")]
		public string ExpectedCTC
		{
			get
			{
				return this._ExpectedCTC;
			}
			set
			{
				if ((this._ExpectedCTC != value))
				{
					this.OnExpectedCTCChanging(value);
					this.SendPropertyChanging();
					this._ExpectedCTC = value;
					this.SendPropertyChanged("ExpectedCTC");
					this.OnExpectedCTCChanged();
				}
			}
		}
		
		[Column(Storage="_NoOfCalls", DbType="Int NOT NULL")]
		public int NoOfCalls
		{
			get
			{
				return this._NoOfCalls;
			}
			set
			{
				if ((this._NoOfCalls != value))
				{
					this.OnNoOfCallsChanging(value);
					this.SendPropertyChanging();
					this._NoOfCalls = value;
					this.SendPropertyChanged("NoOfCalls");
					this.OnNoOfCallsChanged();
				}
			}
		}
		
		[Column(Storage="_Remarks", DbType="NVarChar(200)")]
		public string Remarks
		{
			get
			{
				return this._Remarks;
			}
			set
			{
				if ((this._Remarks != value))
				{
					this.OnRemarksChanging(value);
					this.SendPropertyChanging();
					this._Remarks = value;
					this.SendPropertyChanged("Remarks");
					this.OnRemarksChanged();
				}
			}
		}
		
		[Column(Storage="_CVPath", DbType="NVarChar(MAX)")]
		public string CVPath
		{
			get
			{
				return this._CVPath;
			}
			set
			{
				if ((this._CVPath != value))
				{
					this.OnCVPathChanging(value);
					this.SendPropertyChanging();
					this._CVPath = value;
					this.SendPropertyChanged("CVPath");
					this.OnCVPathChanged();
				}
			}
		}
		
		[Column(Storage="_Registered", DbType="Bit NOT NULL")]
		public bool Registered
		{
			get
			{
				return this._Registered;
			}
			set
			{
				if ((this._Registered != value))
				{
					this.OnRegisteredChanging(value);
					this.SendPropertyChanging();
					this._Registered = value;
					this.SendPropertyChanged("Registered");
					this.OnRegisteredChanged();
				}
			}
		}
		
		[Column(Storage="_Placed", DbType="Bit NOT NULL")]
		public bool Placed
		{
			get
			{
				return this._Placed;
			}
			set
			{
				if ((this._Placed != value))
				{
					this.OnPlacedChanging(value);
					this.SendPropertyChanging();
					this._Placed = value;
					this.SendPropertyChanged("Placed");
					this.OnPlacedChanged();
				}
			}
		}
		
		[Column(Storage="_MarritalStatus", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string MarritalStatus
		{
			get
			{
				return this._MarritalStatus;
			}
			set
			{
				if ((this._MarritalStatus != value))
				{
					this.OnMarritalStatusChanging(value);
					this.SendPropertyChanging();
					this._MarritalStatus = value;
					this.SendPropertyChanged("MarritalStatus");
					this.OnMarritalStatusChanged();
				}
			}
		}
		
		[Column(Storage="_ExecutiveName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string ExecutiveName
		{
			get
			{
				return this._ExecutiveName;
			}
			set
			{
				if ((this._ExecutiveName != value))
				{
					this.OnExecutiveNameChanging(value);
					this.SendPropertyChanging();
					this._ExecutiveName = value;
					this.SendPropertyChanged("ExecutiveName");
					this.OnExecutiveNameChanged();
				}
			}
		}
		
		[Column(Storage="_CallLatter1", DbType="NVarChar(MAX)")]
		public string CallLatter1
		{
			get
			{
				return this._CallLatter1;
			}
			set
			{
				if ((this._CallLatter1 != value))
				{
					this.OnCallLatter1Changing(value);
					this.SendPropertyChanging();
					this._CallLatter1 = value;
					this.SendPropertyChanged("CallLatter1");
					this.OnCallLatter1Changed();
				}
			}
		}
		
		[Column(Storage="_CallLatter2", DbType="NVarChar(MAX)")]
		public string CallLatter2
		{
			get
			{
				return this._CallLatter2;
			}
			set
			{
				if ((this._CallLatter2 != value))
				{
					this.OnCallLatter2Changing(value);
					this.SendPropertyChanging();
					this._CallLatter2 = value;
					this.SendPropertyChanged("CallLatter2");
					this.OnCallLatter2Changed();
				}
			}
		}
		
		[Column(Storage="_CallLatter3", DbType="NVarChar(MAX)")]
		public string CallLatter3
		{
			get
			{
				return this._CallLatter3;
			}
			set
			{
				if ((this._CallLatter3 != value))
				{
					this.OnCallLatter3Changing(value);
					this.SendPropertyChanging();
					this._CallLatter3 = value;
					this.SendPropertyChanged("CallLatter3");
					this.OnCallLatter3Changed();
				}
			}
		}
		
		[Column(Storage="_CallLatter4", DbType="NVarChar(MAX)")]
		public string CallLatter4
		{
			get
			{
				return this._CallLatter4;
			}
			set
			{
				if ((this._CallLatter4 != value))
				{
					this.OnCallLatter4Changing(value);
					this.SendPropertyChanging();
					this._CallLatter4 = value;
					this.SendPropertyChanged("CallLatter4");
					this.OnCallLatter4Changed();
				}
			}
		}
		
		[Column(Storage="_CallLatter5", DbType="NVarChar(MAX)")]
		public string CallLatter5
		{
			get
			{
				return this._CallLatter5;
			}
			set
			{
				if ((this._CallLatter5 != value))
				{
					this.OnCallLatter5Changing(value);
					this.SendPropertyChanging();
					this._CallLatter5 = value;
					this.SendPropertyChanged("CallLatter5");
					this.OnCallLatter5Changed();
				}
			}
		}
		
		[Column(Storage="_IsDeleted", DbType="Bit NOT NULL")]
		public bool IsDeleted
		{
			get
			{
				return this._IsDeleted;
			}
			set
			{
				if ((this._IsDeleted != value))
				{
					this.OnIsDeletedChanging(value);
					this.SendPropertyChanging();
					this._IsDeleted = value;
					this.SendPropertyChanged("IsDeleted");
					this.OnIsDeletedChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591