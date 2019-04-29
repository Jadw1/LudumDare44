﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EventManager {
    public delegate void OnEntityMoved(TileEntity entity, TilePos previous, TilePos current);

    public static event OnEntityMoved OnEntityMovedEvent;

    public static void CallOnEntityMovedEvent(TileEntity entity, TilePos previous, TilePos current) {
        OnEntityMovedEvent?.Invoke(entity, previous, current);
    }
}