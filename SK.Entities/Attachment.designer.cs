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
	public partial class AttachmentDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertAttachment(Attachment instance);
    partial void UpdateAttachment(Attachment instance);
    partial void DeleteAttachment(Attachment instance);
    #endregion
		
		public AttachmentDataContext() : 
				base(global::SK.Entities.Properties.Settings.Default.JGDConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public AttachmentDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AttachmentDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AttachmentDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public AttachmentDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Attachment> Attachment
		{
			get
			{
				return this.GetTable<Attachment>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Attachment")]
	public partial class Attachment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _ID;
		
		private string _SourceID;
		
		private string _FilePath;
		
		private string _FileName;
		
		private System.DateTime _CreateAt;
		
		private string _Name;
		
		private int _FileSize;
		
		private System.DateTime _UpdateAt;
		
		private int _Width;
		
		private int _Height;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(string value);
    partial void OnIDChanged();
    partial void OnSourceIDChanging(string value);
    partial void OnSourceIDChanged();
    partial void OnFilePathChanging(string value);
    partial void OnFilePathChanged();
    partial void OnFileNameChanging(string value);
    partial void OnFileNameChanged();
    partial void OnCreateAtChanging(System.DateTime value);
    partial void OnCreateAtChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnFileSizeChanging(int value);
    partial void OnFileSizeChanged();
    partial void OnUpdateAtChanging(System.DateTime value);
    partial void OnUpdateAtChanged();
    partial void OnWidthChanging(int value);
    partial void OnWidthChanged();
    partial void OnHeightChanging(int value);
    partial void OnHeightChanged();
    #endregion
		
		public Attachment()
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SourceID", DbType="VarChar(36) NOT NULL", CanBeNull=false)]
		public string SourceID
		{
			get
			{
				return this._SourceID;
			}
			set
			{
				if ((this._SourceID != value))
				{
					this.OnSourceIDChanging(value);
					this.SendPropertyChanging();
					this._SourceID = value;
					this.SendPropertyChanged("SourceID");
					this.OnSourceIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FilePath", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string FilePath
		{
			get
			{
				return this._FilePath;
			}
			set
			{
				if ((this._FilePath != value))
				{
					this.OnFilePathChanging(value);
					this.SendPropertyChanging();
					this._FilePath = value;
					this.SendPropertyChanged("FilePath");
					this.OnFilePathChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string FileName
		{
			get
			{
				return this._FileName;
			}
			set
			{
				if ((this._FileName != value))
				{
					this.OnFileNameChanging(value);
					this.SendPropertyChanging();
					this._FileName = value;
					this.SendPropertyChanged("FileName");
					this.OnFileNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateAt", DbType="DateTime NOT NULL")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileSize", DbType="Int NOT NULL")]
		public int FileSize
		{
			get
			{
				return this._FileSize;
			}
			set
			{
				if ((this._FileSize != value))
				{
					this.OnFileSizeChanging(value);
					this.SendPropertyChanging();
					this._FileSize = value;
					this.SendPropertyChanged("FileSize");
					this.OnFileSizeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UpdateAt", DbType="DateTime NOT NULL")]
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Width")]
		public int Width
		{
			get
			{
				return this._Width;
			}
			set
			{
				if ((this._Width != value))
				{
					this.OnWidthChanging(value);
					this.SendPropertyChanging();
					this._Width = value;
					this.SendPropertyChanged("Width");
					this.OnWidthChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Height")]
		public int Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				if ((this._Height != value))
				{
					this.OnHeightChanging(value);
					this.SendPropertyChanging();
					this._Height = value;
					this.SendPropertyChanged("Height");
					this.OnHeightChanged();
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
