﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testMVC4LinQ
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="TestDB")]
	public partial class MyDBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUsuariostest(Usuariostest instance);
    partial void UpdateUsuariostest(Usuariostest instance);
    partial void DeleteUsuariostest(Usuariostest instance);
    #endregion
		
		public MyDBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public MyDBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MyDBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MyDBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public MyDBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Usuariostest> Usuariostests
		{
			get
			{
				return this.GetTable<Usuariostest>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="tst.Usuariostest")]
	public partial class Usuariostest : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _usuario;
		
		private string _nombre;
		
		private string _apellido1;
		
		private string _apellido2;
		
		private bool _habilitado;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnusuarioChanging(string value);
    partial void OnusuarioChanged();
    partial void OnnombreChanging(string value);
    partial void OnnombreChanged();
    partial void Onapellido1Changing(string value);
    partial void Onapellido1Changed();
    partial void Onapellido2Changing(string value);
    partial void Onapellido2Changed();
    partial void OnhabilitadoChanging(bool value);
    partial void OnhabilitadoChanged();
    #endregion
		
		public Usuariostest()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_usuario", DbType="VarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string usuario
		{
			get
			{
				return this._usuario;
			}
			set
			{
				if ((this._usuario != value))
				{
					this.OnusuarioChanging(value);
					this.SendPropertyChanging();
					this._usuario = value;
					this.SendPropertyChanged("usuario");
					this.OnusuarioChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_nombre", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string nombre
		{
			get
			{
				return this._nombre;
			}
			set
			{
				if ((this._nombre != value))
				{
					this.OnnombreChanging(value);
					this.SendPropertyChanging();
					this._nombre = value;
					this.SendPropertyChanged("nombre");
					this.OnnombreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_apellido1", DbType="VarChar(50)")]
		public string apellido1
		{
			get
			{
				return this._apellido1;
			}
			set
			{
				if ((this._apellido1 != value))
				{
					this.Onapellido1Changing(value);
					this.SendPropertyChanging();
					this._apellido1 = value;
					this.SendPropertyChanged("apellido1");
					this.Onapellido1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_apellido2", DbType="VarChar(50)")]
		public string apellido2
		{
			get
			{
				return this._apellido2;
			}
			set
			{
				if ((this._apellido2 != value))
				{
					this.Onapellido2Changing(value);
					this.SendPropertyChanging();
					this._apellido2 = value;
					this.SendPropertyChanged("apellido2");
					this.Onapellido2Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_habilitado", DbType="Bit NOT NULL")]
		public bool habilitado
		{
			get
			{
				return this._habilitado;
			}
			set
			{
				if ((this._habilitado != value))
				{
					this.OnhabilitadoChanging(value);
					this.SendPropertyChanging();
					this._habilitado = value;
					this.SendPropertyChanged("habilitado");
					this.OnhabilitadoChanged();
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
