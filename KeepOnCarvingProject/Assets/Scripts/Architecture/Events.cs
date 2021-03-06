﻿public class KeepOnCarvingEvent { }

public class SkaterCrashEvent : KeepOnCarvingEvent { }

public class NewHighScoreEvent : KeepOnCarvingEvent
{
    public int Score { get; set; }

    public NewHighScoreEvent(int score)
    {
        this.Score = score;
    }
}

public class RetryEvent : KeepOnCarvingEvent { }

public class GameStartEvent : KeepOnCarvingEvent { }