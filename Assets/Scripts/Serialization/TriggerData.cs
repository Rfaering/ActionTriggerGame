﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BehaviorData
{
    public bool Applied;
    public bool Available;
    public string Name;
}

[Serializable()]
public class ActionData : BehaviorData { }

[Serializable()]
public class TriggerData : BehaviorData { }
