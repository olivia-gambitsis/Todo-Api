﻿namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? EstimatedTimeToComplete { get; set; }

    public bool? IsComplete { get; set; }

    public int? OrderIndex { get; set; }

}

