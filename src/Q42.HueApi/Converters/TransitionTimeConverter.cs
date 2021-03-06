﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Q42.HueApi
{
  internal class TransitionTimeConverter
    : JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof (TimeSpan?) || objectType == typeof (TimeSpan);
    }

    public override bool CanRead
    {
      get { return true; }
    }

    public override bool CanWrite
    {
      get { return true; }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      int? value = reader.ReadAsInt32();
      if (value == null)
        return null;

      return (TimeSpan?)TimeSpan.FromMilliseconds (value.Value * 100);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      TimeSpan span;

      if (value == null)
      {
          writer.WriteValue (0);
          return;
      }

      if (value is TimeSpan?)
        span = ((TimeSpan?)value).Value;
      else
        span = (TimeSpan)value;

      writer.WriteValue ((int?)(span.TotalSeconds * 10));
    }
  }
}