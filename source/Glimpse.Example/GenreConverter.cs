﻿using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Glimpse.Net.Extensibility;
using MvcMusicStore.Models;

namespace MvcMusicStore
{
    [GlimpseConverter]
    public class GenreConverter:IGlimpseConverter
    {
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var source = obj as IList<Genre>;
            if (source == null) return null;

            var result = new Dictionary<string, object>();

            var count = 0;
            foreach (var genre in source)
            {
                result.Add(count++.ToString(), new Dictionary<string, string>
                       {
                           {"Name", genre.Name},
                           {"Id", genre.GenreId.ToString()},
                           {"Description", genre.Description},
                       });
            }

            return result;
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                yield return typeof(List<Genre>);
                yield break;
            }
        }
    }
}