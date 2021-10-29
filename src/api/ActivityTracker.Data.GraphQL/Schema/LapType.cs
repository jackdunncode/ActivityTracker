﻿using ActivityTracker.Application.Models;
using GraphQL.Types;

namespace ActivityTracker.Data.Graph.Schema
{
    public sealed class LapType : ObjectGraphType<Lap>
    {
        public LapType()
        {
            Field(lap => lap.Id);
            Field(lap => lap.StartDateTimeUtc);
            Field(lap => lap.StartDateTimeUtc);
        }
    }
}