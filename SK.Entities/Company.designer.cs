﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18444
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SK.Entities
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="JGD")]
	public partial class CompanyDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertCompany(Company instance);
    partial void UpdateCompany(Company instance);
    partial void DeleteCompany(Company instance);
    #endregion
		
		public CompanyDataContext() : 
				base(global::SK.Entities.Properties.Settings.Default.JGDConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public CompanyDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Company> Company
		{
			get
			{
				return this.GetTable<Company>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Company")]
	public partial class Company : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _ID;
		
		private string _UserID;
		
		private string _CompanyName;
		
		private string _Contact;
		
		private string _Tel;
		
		private string _Address;
		
		private string _Mobile;
		
		private string _Password;
        private string _Pic;

        private bool _IsPass;
		
		private System.DateTime _CreateAt;
		
		private System.DateTime _UpdateAt;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(string value);
    partial void OnIDChanged();
    partial void OnUserIDChanging(string value);
    partial void OnUserIDChanged();
    partial void OnCompanyNameChanging(string value);
    partial void OnCompanyNameChanged();
    partial void OnContactChanging(string value);
    partial void OnContactChanged();
    partial void OnTelChanging(string value);
    partial void OnTelChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnMobileChanging(string value);
    partial void OnMobileChanged();
    partial void OnPasswordChanging(string value);
    partial void OnPasswordChanged();
    partial void OnIsPassChanging(bool value);
    partial void OnIsPassChanged();
    partial void OnCreateAtChanging(System.DateTime value);
    partial void OnCreateAtChanged();
    partial void OnUpdateAtChanging(System.DateTime value);
    partial void OnUpdateAtChanged();
    #endregion
		
		public Company()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", DbType="VarChar(36) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="VarChar(36) NOT NULL", CanBeNull=false)]
		public string UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CompanyName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string CompanyName
		{
			get
			{
				return this._CompanyName;
			}
			set
			{
				if ((this._CompanyName != value))
				{
					this.OnCompanyNameChanging(value);
					this.SendPropertyChanging();
					this._CompanyName = value;
					this.SendPropertyChanged("CompanyName");
					this.OnCompanyNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Contact", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Contact
		{
			get
			{
				return this._Contact;
			}
			set
			{
				if ((this._Contact != value))
				{
					this.OnContactChanging(value);
					this.SendPropertyChanging();
					this._Contact = value;
					this.SendPropertyChanged("Contact");
					this.OnContactChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Tel", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Tel
		{
			get
			{
				return this._Tel;
			}
			set
			{
				if ((this._Tel != value))
				{
					this.OnTelChanging(value);
					this.SendPropertyChanging();
					this._Tel = value;
					this.SendPropertyChanged("Tel");
					this.OnTelChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Mobile", CanBeNull=false)]
		public string Mobile
		{
			get
			{
				return this._Mobile;
			}
			set
			{
				if ((this._Mobile != value))
				{
					this.OnMobileChanging(value);
					this.SendPropertyChanging();
					this._Mobile = value;
					this.SendPropertyChanged("Mobile");
					this.OnMobileChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", CanBeNull=false)]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this.OnPasswordChanging(value);
					this.SendPropertyChanging();
					this._Password = value;
					this.SendPropertyChanged("Password");
					this.OnPasswordChanged();
				}
			}
		}

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Pic", CanBeNull = false)]
        public string Pic
        {
            get
            {
                return this._Pic;
            }
            set
            {
                if ((this._Pic != value))
                {
                    this.OnPasswordChanging(value);
                    this.SendPropertyChanging();
                    this._Pic = value;
                    this.SendPropertyChanged("Pic");
                    this.OnPasswordChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsPass")]
		public bool IsPass
		{
			get
			{
				return this._IsPass;
			}
			set
			{
				if ((this._IsPass != value))
				{
					this.OnIsPassChanging(value);
					this.SendPropertyChanging();
					this._IsPass = value;
					this.SendPropertyChanged("IsPass");
					this.OnIsPassChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateAt")]
		public System.DateTime CreateAt
		{
			get
			{
				return this._CreateAt;
			}
			set
			{
				if ((this._CreateAt != value))
				{
					this.OnCreateAtChanging(value);
					this.SendPropertyChanging();
					this._CreateAt = value;
					this.SendPropertyChanged("CreateAt");
					this.OnCreateAtChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateAt")]
		public System.DateTime UpdateAt
		{
			get
			{
				return this._UpdateAt;
			}
			set
			{
				if ((this._UpdateAt != value))
				{
					this.OnUpdateAtChanging(value);
					this.SendPropertyChanging();
					this._UpdateAt = value;
					this.SendPropertyChanged("UpdateAt");
					this.OnUpdateAtChanged();
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
