/**
 * Autogenerated by Thrift Compiler (0.20.0)
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


// Thrift code generated for net8
#nullable enable                 // requires C# 8.0
#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE0290  // use primary CTOR
#pragma warning disable IDE1006  // parts of the code use IDL spelling
#pragma warning disable CA1822   // empty DeepCopy() methods still non-static

namespace ThriftCom;

public partial class ThriftTrip : TBase
{
  private long _id;
  private string? _landmark;
  private string? _transportCompany;
  private string? _departureTime;
  private double _price;
  private int _slots;

  public long Id
  {
    get
    {
      return _id;
    }
    set
    {
      __isset.@id = true;
      this._id = value;
    }
  }

  public string? Landmark
  {
    get
    {
      return _landmark;
    }
    set
    {
      __isset.@landmark = true;
      this._landmark = value;
    }
  }

  public string? TransportCompany
  {
    get
    {
      return _transportCompany;
    }
    set
    {
      __isset.transportCompany = true;
      this._transportCompany = value;
    }
  }

  public string? DepartureTime
  {
    get
    {
      return _departureTime;
    }
    set
    {
      __isset.departureTime = true;
      this._departureTime = value;
    }
  }

  public double Price
  {
    get
    {
      return _price;
    }
    set
    {
      __isset.@price = true;
      this._price = value;
    }
  }

  public int Slots
  {
    get
    {
      return _slots;
    }
    set
    {
      __isset.@slots = true;
      this._slots = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool @id;
    public bool @landmark;
    public bool transportCompany;
    public bool departureTime;
    public bool @price;
    public bool @slots;
  }

  public ThriftTrip()
  {
  }

  public ThriftTrip DeepCopy()
  {
    var tmp5 = new ThriftTrip()
    {
    };
    if(__isset.@id)
    {
      tmp5.Id = this.Id;
    }
    tmp5.__isset.@id = this.__isset.@id;
    if((Landmark != null) && __isset.@landmark)
    {
      tmp5.Landmark = this.Landmark!;
    }
    tmp5.__isset.@landmark = this.__isset.@landmark;
    if((TransportCompany != null) && __isset.transportCompany)
    {
      tmp5.TransportCompany = this.TransportCompany!;
    }
    tmp5.__isset.transportCompany = this.__isset.transportCompany;
    if((DepartureTime != null) && __isset.departureTime)
    {
      tmp5.DepartureTime = this.DepartureTime!;
    }
    tmp5.__isset.departureTime = this.__isset.departureTime;
    if(__isset.@price)
    {
      tmp5.Price = this.Price;
    }
    tmp5.__isset.@price = this.__isset.@price;
    if(__isset.@slots)
    {
      tmp5.Slots = this.Slots;
    }
    tmp5.__isset.@slots = this.__isset.@slots;
    return tmp5;
  }

  public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      TField field;
      await iprot.ReadStructBeginAsync(cancellationToken);
      while (true)
      {
        field = await iprot.ReadFieldBeginAsync(cancellationToken);
        if (field.Type == TType.Stop)
        {
          break;
        }

        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.I64)
            {
              Id = await iprot.ReadI64Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              Landmark = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 3:
            if (field.Type == TType.String)
            {
              TransportCompany = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 4:
            if (field.Type == TType.String)
            {
              DepartureTime = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 5:
            if (field.Type == TType.Double)
            {
              Price = await iprot.ReadDoubleAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 6:
            if (field.Type == TType.I32)
            {
              Slots = await iprot.ReadI32Async(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          default: 
            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            break;
        }

        await iprot.ReadFieldEndAsync(cancellationToken);
      }

      await iprot.ReadStructEndAsync(cancellationToken);
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
  {
    oprot.IncrementRecursionDepth();
    try
    {
      var tmp6 = new TStruct("ThriftTrip");
      await oprot.WriteStructBeginAsync(tmp6, cancellationToken);
      #pragma warning disable IDE0017  // simplified init
      var tmp7 = new TField();
      if(__isset.@id)
      {
        tmp7.Name = "id";
        tmp7.Type = TType.I64;
        tmp7.ID = 1;
        await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
        await oprot.WriteI64Async(Id, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((Landmark != null) && __isset.@landmark)
      {
        tmp7.Name = "landmark";
        tmp7.Type = TType.String;
        tmp7.ID = 2;
        await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
        await oprot.WriteStringAsync(Landmark, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((TransportCompany != null) && __isset.transportCompany)
      {
        tmp7.Name = "transportCompany";
        tmp7.Type = TType.String;
        tmp7.ID = 3;
        await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
        await oprot.WriteStringAsync(TransportCompany, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if((DepartureTime != null) && __isset.departureTime)
      {
        tmp7.Name = "departureTime";
        tmp7.Type = TType.String;
        tmp7.ID = 4;
        await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
        await oprot.WriteStringAsync(DepartureTime, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.@price)
      {
        tmp7.Name = "price";
        tmp7.Type = TType.Double;
        tmp7.ID = 5;
        await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
        await oprot.WriteDoubleAsync(Price, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      if(__isset.@slots)
      {
        tmp7.Name = "slots";
        tmp7.Type = TType.I32;
        tmp7.ID = 6;
        await oprot.WriteFieldBeginAsync(tmp7, cancellationToken);
        await oprot.WriteI32Async(Slots, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      #pragma warning restore IDE0017  // simplified init
      await oprot.WriteFieldStopAsync(cancellationToken);
      await oprot.WriteStructEndAsync(cancellationToken);
    }
    finally
    {
      oprot.DecrementRecursionDepth();
    }
  }

  public override bool Equals(object? that)
  {
    if (that is not ThriftTrip other) return false;
    if (ReferenceEquals(this, other)) return true;
    return ((__isset.@id == other.__isset.@id) && ((!__isset.@id) || (global::System.Object.Equals(Id, other.Id))))
      && ((__isset.@landmark == other.__isset.@landmark) && ((!__isset.@landmark) || (global::System.Object.Equals(Landmark, other.Landmark))))
      && ((__isset.transportCompany == other.__isset.transportCompany) && ((!__isset.transportCompany) || (global::System.Object.Equals(TransportCompany, other.TransportCompany))))
      && ((__isset.departureTime == other.__isset.departureTime) && ((!__isset.departureTime) || (global::System.Object.Equals(DepartureTime, other.DepartureTime))))
      && ((__isset.@price == other.__isset.@price) && ((!__isset.@price) || (global::System.Object.Equals(Price, other.Price))))
      && ((__isset.@slots == other.__isset.@slots) && ((!__isset.@slots) || (global::System.Object.Equals(Slots, other.Slots))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      if(__isset.@id)
      {
        hashcode = (hashcode * 397) + Id.GetHashCode();
      }
      if((Landmark != null) && __isset.@landmark)
      {
        hashcode = (hashcode * 397) + Landmark.GetHashCode();
      }
      if((TransportCompany != null) && __isset.transportCompany)
      {
        hashcode = (hashcode * 397) + TransportCompany.GetHashCode();
      }
      if((DepartureTime != null) && __isset.departureTime)
      {
        hashcode = (hashcode * 397) + DepartureTime.GetHashCode();
      }
      if(__isset.@price)
      {
        hashcode = (hashcode * 397) + Price.GetHashCode();
      }
      if(__isset.@slots)
      {
        hashcode = (hashcode * 397) + Slots.GetHashCode();
      }
    }
    return hashcode;
  }

  public override string ToString()
  {
    var tmp8 = new StringBuilder("ThriftTrip(");
    int tmp9 = 0;
    if(__isset.@id)
    {
      if(0 < tmp9++) { tmp8.Append(", "); }
      tmp8.Append("Id: ");
      Id.ToString(tmp8);
    }
    if((Landmark != null) && __isset.@landmark)
    {
      if(0 < tmp9++) { tmp8.Append(", "); }
      tmp8.Append("Landmark: ");
      Landmark.ToString(tmp8);
    }
    if((TransportCompany != null) && __isset.transportCompany)
    {
      if(0 < tmp9++) { tmp8.Append(", "); }
      tmp8.Append("TransportCompany: ");
      TransportCompany.ToString(tmp8);
    }
    if((DepartureTime != null) && __isset.departureTime)
    {
      if(0 < tmp9++) { tmp8.Append(", "); }
      tmp8.Append("DepartureTime: ");
      DepartureTime.ToString(tmp8);
    }
    if(__isset.@price)
    {
      if(0 < tmp9++) { tmp8.Append(", "); }
      tmp8.Append("Price: ");
      Price.ToString(tmp8);
    }
    if(__isset.@slots)
    {
      if(0 < tmp9++) { tmp8.Append(", "); }
      tmp8.Append("Slots: ");
      Slots.ToString(tmp8);
    }
    tmp8.Append(')');
    return tmp8.ToString();
  }
}

