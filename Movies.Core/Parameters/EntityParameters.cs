﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Parameters;
public class EntityParameters
{
    const int maxPageSize = 100;
    public int PageNumber { get; set; } = 1;
    private int _pageSize { get; set; } = 10;
    public string? SearchQuery { get; set; }
    public int PageSize 
    {
        get => _pageSize;
        set => _pageSize = value > maxPageSize ? maxPageSize : value;
    }
}

public class MovieParameters : EntityParameters
{
    public string? Title { get; set; }
    public MovieParameters()
    {
        PageSize = 5;
    }
}
public class ActorParameters : EntityParameters
{
    public string? Name { get; set; }
}


